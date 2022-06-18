#![feature(never_type)]
extern crate core;

mod model;

use core::panicking::panic;
use std::fs::File;
use std::io::ErrorKind;
use std::sync::RwLock;
use doublets::mem::FileMappedMem;
use doublets::{mem, splited};
use actix_web::{guard, web, web::Data, App, HttpResponse, HttpServer, Result, Responder};
use async_graphql::{
    http::{playground_source, GraphQLPlaygroundConfig},
    EmptyMutation, EmptySubscription
};
use async_graphql_actix_web::{GraphQLRequest, GraphQLResponse};
use model::{QueryRoot, MutationRoot};
type Store = RwLock<splited::Store<u64, FileMappedMem, FileMappedMem>>;
type Schema = async_graphql::Schema<QueryRoot, MutationRoot, EmptySubscription>;

async fn index(schema: web::Data<Schema>, req: GraphQLRequest) -> GraphQLResponse {
    schema.execute(req.into_inner()).await.into()
}

async fn index_playground() -> Result<HttpResponse> {
    let source = playground_source(GraphQLPlaygroundConfig::new("/").subscription_endpoint("/"));
    Ok(HttpResponse::Ok()
        .content_type("text/html; charset=utf-8")
        .body(source))
}

const LINKS_FILE_PATH: &str = "db.links";
const INDEX_LINKS_FILE_PATH: &str = "index_db.links";

#[actix_web::main]
async fn main() -> std::io::Result<()> {

    let mut memory_file = File::open(LINKS_FILE_PATH).unwrap_or_else(|error| {
        if error.kind() == ErrorKind::NotFound {
            File::create(LINKS_FILE_PATH).unwrap()
        } else {
            panic!(error)
        }
    });
    let links_file_mapped_mem = FileMappedMem::new(memoryFile).unwrap();
    let mut memory_file = File::open(INDEX_LINKS_FILE_PATH).unwrap_or_else(|error| {
        if error.kind() == ErrorKind::NotFound {
            File::create(INDEX_LINKS_FILE_PATH).unwrap()
        } else {
            panic!(error)
        }
    });
    let index_links_file_mapped_mem = FileMappedMem::new(index_memory_file).unwrap();
    let store = match splited::Store::new(links_file_mapped_mem, index_links_file_mapped_mem) {
        Ok(store) => store,
        Err(e) => panic!("Problem opening links store: {:?}", e)
    };
    let schema = async_graphql::Schema::build(QueryRoot, MutationRoot, EmptySubscription)
        .data(Store::new(store))
        .finish();

    println!("Playground: http://localhost:8000");

    HttpServer::new(move || {
        App::new()
            .app_data(Data::new(schema.clone()))
            .service(web::resource("/").guard(guard::Post()).to(index))
            .service(web::resource("/").guard(guard::Get()).to(index_playground))
    })
        .bind("127.0.0.1:8000")?
        .run()
        .await
}
