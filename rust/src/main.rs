#![feature(never_type)]
#![feature(result_flattening)]

mod model;

use crate::model::{MutationRoot, QueryRoot};
use actix_web::{guard, web, App, HttpResponse, HttpServer, Responder};
use async_graphql::{
    http::{playground_source, GraphQLPlaygroundConfig},
    EmptyMutation, EmptySubscription,
};
use async_graphql_actix_web::{GraphQLRequest, GraphQLResponse};
use async_std::sync::RwLock;
use doublets::mem::FileMappedMem;
use doublets::splited;
use std::{error::Error, fs::File, io, path::Path};

// todo: wait for fix type infer
type RawStore = splited::Store<u64, FileMappedMem, FileMappedMem>;
type Store = RwLock<RawStore>;
type Schema = async_graphql::Schema<QueryRoot, MutationRoot, EmptySubscription>;

async fn index(schema: web::Data<Schema>, req: GraphQLRequest) -> GraphQLResponse {
    schema.execute(req.into_inner()).await.into()
}

async fn index_playground() -> actix_web::Result<HttpResponse> {
    let source = playground_source(GraphQLPlaygroundConfig::new("/").subscription_endpoint("/"));
    Ok(HttpResponse::Ok()
        .content_type("text/html; charset=utf-8")
        .body(source))
}

// todo: may be add support async-std files to platform-mem
fn map_db_file<P: AsRef<Path>>(path: P) -> io::Result<FileMappedMem> {
    File::options()
        .create(true)
        .read(true)
        .write(true)
        .open(path)
        .map(FileMappedMem::new)
        .flatten()
}

#[actix_web::main]
// todo: implement Into<io::Error> for LinksError
async fn main() -> Result<(), Box<dyn Error>> {
    let store = RawStore::new(map_db_file("db.links")?, map_db_file("index.links")?)?;
    let schema = Schema::build(QueryRoot, MutationRoot, EmptySubscription)
        .data(Store::new(store))
        .finish();

    println!("Playground: http://localhost:8000");

    HttpServer::new(move || {
        App::new()
            .app_data(web::Data::new(schema.clone()))
            .service(web::resource("/v1/graphql").guard(guard::Post()).to(index))
            .service(
                web::resource("/ui")
                    .guard(guard::Get())
                    .to(index_playground),
            )
    })
    .bind("localhost:8000")?
    .run()
    .await
    .map_err(|e| e.into())
}
