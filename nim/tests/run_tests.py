#!/usr/bin/env python3
"""Compile Nim packages and test real native/C# backends on disposable data.
Rust integration is optional; packages do not depend on the Rust
implementation. Only loopback HTTP is used. Requires Python3, Nim2+, C
compiler, the native DLL, a built original C# GraphQL server, and a suitable
.NET runtime.
"""
import argparse
import contextlib
import http.client
import http.server
import json
import os
import pathlib
import signal
import socket
import subprocess
import tempfile
import threading
import time

BASE = pathlib.Path(__file__).resolve().parents[1]


class Fixtures(http.server.BaseHTTPRequestHandler):
    def log_message(self, *_):
        pass

    def do_POST(self):
        request = json.loads(
            self.rfile.read(int(self.headers.get('Content-Length', '0')))
        )
        bodies = {
            '/http-error': (503, b'{"error":"unavailable"}'),
            '/invalid-json': (200, b'not json'),
            '/graphql-error': (
                200,
                b'{"data":{"links":[]},'
                b'"errors":[{"message":"example failure"}]}',
            ),
            '/missing-data': (200, b'{}'),
            '/wrong-id': (
                200,
                b'{"data":{"links":[{"id":2,"from_id":1,"to_id":1}]}}',
            ),
            '/negative-address': (
                200,
                b'{"data":{"links":[{"id":1,"from_id":-1,"to_id":1}]}}',
            ),
            '/wrong-shape': (200, b'{"data":{"links":{}}}'),
            '/slow-body': (200, b'{"data":{"links":[]}}'),
            '/echo': (
                200,
                json.dumps({
                    'data': {
                        'header': self.headers.get('X-Test'),
                        'variables': request.get('variables'),
                    }
                }).encode(),
            ),
        }
        status, body = bodies.get(self.path, (404, b'{}'))
        self.send_response(status)
        self.send_header('Content-Type', 'application/json')
        self.send_header('Content-Length', str(len(body)))
        self.end_headers()
        if self.path == '/slow-body':
            time.sleep(.25)
        try:
            self.wfile.write(body)
        except (BrokenPipeError, ConnectionResetError):
            pass


@contextlib.contextmanager
def server(kind, binary, args, logs):
    with tempfile.TemporaryDirectory(
        prefix='doublets-nim-server-'
    ) as database:
        with socket.socket() as reservation:
            reservation.bind(('127.0.0.1', 0))
            port = reservation.getsockname()[1]
        origin = f'http://127.0.0.1:{port}'
        env = os.environ.copy()
        if kind == 'csharp':
            command = [
                args.dotnet,
                str(binary),
                str(pathlib.Path(database) / 'db.links'),
            ]
            env.update({
                'ASPNETCORE_URLS': origin,
                'ASPNETCORE_ENVIRONMENT': 'Production',
            })
            if args.dotnet_roll_forward:
                env['DOTNET_ROLL_FORWARD'] = args.dotnet_roll_forward
        else:
            command = [str(binary), database, f'127.0.0.1:{port}']
        with (logs / f'{kind}-server.log').open('w') as log:
            process = subprocess.Popen(
                command, cwd=database, env=env, stdout=log,
                stderr=subprocess.STDOUT,
            )
            try:
                for _ in range(150):
                    if process.poll() is not None:
                        raise RuntimeError(
                            f'{kind} server exited; see {log.name}'
                        )
                    try:
                        connection = http.client.HTTPConnection(
                            '127.0.0.1', port, timeout=.3
                        )
                        try:
                            connection.request(
                                'POST', '/v1/graphql',
                                body=b'{"query":"{links{id}}"}',
                                headers={'Content-Type': 'application/json'},
                            )
                            response = connection.getresponse()
                            if response.status != 200:
                                time.sleep(.05)
                                continue
                            data = json.load(response)
                        finally:
                            connection.close()
                        if (
                            data.get('errors')
                            or data.get('data', {}).get('links') != []
                        ):
                            raise RuntimeError(
                                f'{kind} fresh database query failed: {data}'
                            )
                        break
                    except (OSError, http.client.HTTPException):
                        time.sleep(.05)
                else:
                    raise RuntimeError(f'{kind} server startup timed out')
                yield origin + '/v1/graphql'
            finally:
                if process.poll() is None:
                    process.send_signal(signal.SIGTERM)
                try:
                    process.wait(timeout=8)
                except subprocess.TimeoutExpired:
                    process.kill()
                    process.wait()


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--nim', default='nim')
    parser.add_argument('--nim-lib')
    parser.add_argument('--native-library', type=pathlib.Path, required=True)
    parser.add_argument('--csharp-server', type=pathlib.Path, required=True)
    parser.add_argument('--dotnet', default='dotnet')
    parser.add_argument(
        '--dotnet-roll-forward', choices=['LatestPatch', 'Minor', 'Major']
    )
    parser.add_argument('--rust-server', type=pathlib.Path)
    parser.add_argument('--release', action='store_true')
    parser.add_argument('--ssl', action='store_true')
    args = parser.parse_args()
    args.native_library = args.native_library.resolve()
    args.csharp_server = args.csharp_server.resolve()
    if args.rust_server:
        args.rust_server = args.rust_server.resolve()
    build = BASE / 'build'
    logs = build / 'test-results'
    logs.mkdir(parents=True, exist_ok=True)
    results = []

    def run(name, command, env=None):
        with (logs / (name + '.log')).open('w') as log:
            completed = subprocess.run(
                command, cwd=BASE, env=env, stdout=log,
                stderr=subprocess.STDOUT,
            )
        results.append({
            'name': name,
            'command': command,
            'exit_code': completed.returncode,
        })
        (logs / 'results.json').write_text(
            json.dumps(results, indent=2) + '\n'
        )
        if completed.returncode:
            raise RuntimeError(f'{name} failed; see {logs / (name + ".log")}')
        print('PASS:', name, flush=True)

    packages = [
        'platform_data_doublets_native',
        'platform_data_doublets_gql_client',
        'platform_data_doublets_client',
    ]
    test_files = [
        ('native', packages[0], 'test_native.nim'),
        ('graphql', packages[1], 'test_graphql.nim'),
        ('client', packages[2], 'test_client.nim'),
        ('csharp-limitations', packages[1], 'test_csharp_limitations.nim'),
    ]
    for short, package, filename in test_files:
        command = [args.nim, 'c']
        if args.nim_lib:
            command.append(
                '--lib:' + str(pathlib.Path(args.nim_lib).resolve())
            )
        if args.release:
            command.append('-d:release')
        if args.ssl:
            command.append('-d:ssl')
        command.extend(
            '--path:' + str(BASE / name / 'src') for name in packages
        )
        command.extend([
            '--nimcache:' + str(build / ('cache-' + short)),
            '--out:' + str(build / ('test-' + short)),
            str(BASE / package / 'tests' / filename),
        ])
        run('compile-' + short, command)
    env = os.environ.copy()
    env['DOUBLETS_FFI_LIBRARY'] = str(args.native_library)
    env['DOUBLETS_TEST_BACKEND'] = 'native'
    run('native', [str(build / 'test-native')], env)
    run('unified-native', [str(build / 'test-client')], env)
    fixtures = http.server.ThreadingHTTPServer(('127.0.0.1', 0), Fixtures)
    thread = threading.Thread(target=fixtures.serve_forever, daemon=True)
    thread.start()
    env['DOUBLETS_FIXTURE_URL'] = (
        f'http://127.0.0.1:{fixtures.server_address[1]}'
    )
    try:
        backends = [('csharp', args.csharp_server)]
        if args.rust_server:
            backends.append(('rust', args.rust_server))
        for kind, binary in backends:
            with server(kind, binary, args, logs) as endpoint:
                env['DOUBLETS_GQL_URL'] = endpoint
                env['DOUBLETS_TEST_BACKEND'] = 'graphql'
                env['DOUBLETS_TEST_SERVER_KIND'] = kind
                run('graphql-' + kind, [str(build / 'test-graphql')], env)
                run('unified-' + kind, [str(build / 'test-client')], env)
                if kind == 'csharp':
                    run(
                        'csharp-limitations',
                        [str(build / 'test-csharp-limitations')], env,
                    )
    finally:
        fixtures.shutdown()
        fixtures.server_close()
        thread.join(timeout=2)
    print('All checks passed. Logs:', logs)


if __name__ == '__main__':
    main()
