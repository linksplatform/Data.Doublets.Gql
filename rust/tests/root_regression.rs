use doublets_gql_server::{build_schema, Database};
use serde_json::{json, Value};

async fn query(schema: &doublets_gql_server::ApiSchema, text: &str) -> Value {
    let result = schema.execute(text).await;
    assert!(result.errors.is_empty(), "{:?}", result.errors);
    result.data.into_json().unwrap()
}

#[actix_web::test]
async fn signed_64_bit_addresses_round_trip_without_truncation() {
    let directory = tempfile::tempdir().unwrap();
    let schema = build_schema(Database::open(directory.path()).unwrap());
    let inserted = query(
        &schema,
        "mutation { insert_links_one(object: {from_id: 9223372036854775807, to_id: 0}) { id from_id to_id } }",
    )
    .await;
    assert_eq!(inserted["insert_links_one"]["from_id"], json!(i64::MAX));
    assert_eq!(
        query(
            &schema,
            "{ links(where: {from_id: {_eq: 9223372036854775807}}) { from_id to_id } }"
        )
        .await,
        json!({"links": [{"from_id": i64::MAX, "to_id": 0}]}),
    );
    let overflow = schema
        .execute("mutation { insert_links_one(object: {from_id: 9223372036854775808}) { id } }")
        .await;
    assert!(!overflow.errors.is_empty());
    assert_eq!(
        query(&schema, "{ links { id } }").await["links"]
            .as_array()
            .unwrap()
            .len(),
        1
    );
}

#[actix_web::test]
async fn invalid_second_batch_input_leaves_database_unchanged() {
    let directory = tempfile::tempdir().unwrap();
    let schema = build_schema(Database::open(directory.path()).unwrap());
    let response = schema
        .execute("mutation { insert_links(objects: [{from_id: 1, to_id: 1}, {from_id: -1, to_id: 0}]) { affected_rows } }")
        .await;
    assert!(!response.errors.is_empty());
    assert_eq!(
        query(&schema, "{ links { id } }").await,
        json!({"links": []})
    );
    assert_eq!(
        query(
            &schema,
            "mutation { insert_links_one(object: {from_id: 1, to_id: 1}) { id } }"
        )
        .await,
        json!({"insert_links_one": {"id": 1}}),
    );
}

#[actix_web::test]
async fn empty_logical_groups_and_negative_comparison_are_consistent() {
    let directory = tempfile::tempdir().unwrap();
    let schema = build_schema(Database::open(directory.path()).unwrap());
    query(
        &schema,
        "mutation { insert_links_one(object: {from_id: 1, to_id: 1}) { id } }",
    )
    .await;
    assert_eq!(
        query(&schema, "{ all: links(where: {_and: []}) { id } none: links(where: {_or: []}) { id } negative: links(where: {id: {_lt: 0}}) { id } }").await,
        json!({"all": [{"id": 1}], "none": [], "negative": []}),
    );
}

#[actix_web::test]
async fn concurrent_duplicate_inserts_share_one_persistent_link() {
    let directory = tempfile::tempdir().unwrap();
    let schema = build_schema(Database::open(directory.path()).unwrap());
    let mut tasks = Vec::new();
    for _ in 0..8 {
        let schema = schema.clone();
        tasks.push(actix_web::rt::spawn(async move {
            query(
                &schema,
                "mutation { insert_links_one(object: {from_id: 1, to_id: 1}) { id } }",
            )
            .await
        }));
    }
    for task in tasks {
        assert_eq!(task.await.unwrap(), json!({"insert_links_one": {"id": 1}}));
    }
    assert_eq!(
        query(&schema, "{ links { id } }").await,
        json!({"links": [{"id": 1}]})
    );
    assert!(
        Database::open(directory.path()).is_err(),
        "a second writer must not acquire the mapped files"
    );
    drop(schema);
    let reopened = build_schema(Database::open(directory.path()).unwrap());
    assert_eq!(
        query(&reopened, "{ links { id from_id to_id } }").await,
        json!({"links": [{"id": 1, "from_id": 1, "to_id": 1}]})
    );
}
