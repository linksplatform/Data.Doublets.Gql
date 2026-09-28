mod filters;
mod schema;
mod storage_format;
mod store;

use actix_web::{web, HttpResponse};
use async_graphql::http::{playground_source, GraphQLPlaygroundConfig};
use async_graphql_actix_web::{GraphQLRequest, GraphQLResponse};
pub use schema::{build_schema, ApiSchema};
pub use store::Database;

async fn graphql(schema: web::Data<ApiSchema>, request: GraphQLRequest) -> GraphQLResponse {
    schema.execute(request.into_inner()).await.into()
}
async fn playground() -> HttpResponse {
    HttpResponse::Ok()
        .content_type("text/html; charset=utf-8")
        .body(playground_source(GraphQLPlaygroundConfig::new(
            "/v1/graphql",
        )))
}
/// Both the original Rust route and the documented C# route use the same schema.
pub fn configure(config: &mut web::ServiceConfig) {
    config
        .service(web::resource("/v1/graphql").route(web::post().to(graphql)))
        .service(
            web::resource("/")
                .route(web::post().to(graphql))
                .route(web::get().to(playground)),
        )
        .service(web::resource("/ui/playground").route(web::get().to(playground)));
}
