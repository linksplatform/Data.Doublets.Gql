#![feature(never_type)]
#![feature(result_flattening)]
#![feature(box_syntax)]
#![feature(try_trait_v2)]

mod model;
mod store;

use crate::model::{
    LinkType, Links, LinksInsertInput, LinksMutationResponse, LinksOnConflict, LinksOptionExt,
    MutationRoot, QueryRoot,
};
use actix_web::{guard, web, App, HttpResponse, HttpServer, Responder};
use async_graphql::{
    http::{playground_source, GraphQLPlaygroundConfig},
    EmptyMutation, EmptySubscription,
};
use async_graphql_actix_web::{GraphQLRequest, GraphQLResponse};
use async_std::sync::RwLock;
use doublets::mem::FileMapped;
use doublets::{split, Doublets, Link};
use std::{error::Error, fs::File, io, path::Path};

type RawStore = Box<dyn Doublets<LinkType>>;
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
fn map_db_file<T: Default, P: AsRef<Path>>(path: P) -> io::Result<FileMapped<T>> {
    FileMapped::from_path(path)
}

#[tokio::main]
// todo: implement Into<io::Error> for LinksError
async fn main() -> Result<(), Box<dyn Error>> {
    let store =
        split::Store::<LinkType, _, _>::new(map_db_file("db.links")?, map_db_file("index.links")?)?;
    let store: Box<dyn Doublets<_>> = box store::Store::new(store);
    let schema = Schema::build(QueryRoot, MutationRoot, EmptySubscription)
        .data(Store::new(store))
        .finish();

    println!("Playground: http://localhost:8000");

    HttpServer::new(move || {
        App::new()
            .app_data(web::Data::new(schema.clone()))
            .service(web::resource("/").guard(guard::Post()).to(index))
            .service(web::resource("/").guard(guard::Get()).to(index_playground))
    })
    .bind("127.0.0.1:8000")?
    .run()
    .await
    .map_err(|e| e.into())
}

#[cfg(feature = "mimalloc")]
#[global_allocator]
static MIMALLOC: mimalloc::MiMalloc = mimalloc::MiMalloc;

#[cfg(feature = "jemalloc")]
static JEMALLOC: jemalloc::Jemalloc = jemalloc::Jemalloc;
