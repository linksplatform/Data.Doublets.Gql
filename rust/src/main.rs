use actix_web::{web, App, HttpServer};
use doublets_gql_server::{build_schema, configure, Database};
use std::io;

#[actix_web::main]
async fn main() -> io::Result<()> {
    let mut args = std::env::args().skip(1);
    let directory = args.next().unwrap_or_else(|| "doublets-data".into());
    let address = args.next().unwrap_or_else(|| "127.0.0.1:8000".into());
    if args.next().is_some() {
        return Err(io::Error::new(
            io::ErrorKind::InvalidInput,
            "Usage: doublets_gql_server [DATABASE_DIRECTORY] [LISTEN_ADDRESS]",
        ));
    }
    let schema = build_schema(Database::open(directory)?);
    println!("GraphQL: http://{address}/v1/graphql");
    println!("Playground: http://{address}/ui/playground");
    HttpServer::new(move || {
        App::new()
            .app_data(web::Data::new(schema.clone()))
            .configure(configure)
    })
    .bind(address)?
    .run()
    .await
}
