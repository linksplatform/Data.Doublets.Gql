## Use only a disposable/local database for this writing example.
## Native: example native /tmp/example.links /path/to/libPlatform.Doublets.so
## GraphQL: example graphql http://localhost:5000/v1/graphql
import std/[os, options]
import platform_data_doublets_client

proc main() =
  let args = commandLineParams()
  if args.len < 2:
    quit("Usage: client native DATABASE LIBRARY | client graphql ENDPOINT", 1)
  let client = case args[0]
    of "native":
      if args.len != 3: quit("Provide a database and native library path", 1)
      openNativeClient(args[1], args[2])
    of "graphql": openGraphqlClient(args[1])
    else: quit("Backend must be native or graphql", 1)
  defer: client.close()
  let link = client.getOrCreate(1, 1)
  let other = client.getOrCreate(2, 2)
  echo "Created or found: ", link
  echo "Read: ", client.get(link.id).get
  echo "Updated: ", client.update(link.id, 1, 2).get
  echo "Selected for deletion: ", client.delete(link.id)
  discard client.delete(other.id)

main()
