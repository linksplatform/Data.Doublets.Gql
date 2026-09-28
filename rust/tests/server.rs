use actix_web::{test, web, App};
use doublets_gql_server::{build_schema, configure, ApiSchema, Database};
use serde_json::{json, Value};
use tempfile::TempDir;

fn fixture() -> (TempDir, ApiSchema) {
    let dir = tempfile::tempdir().unwrap();
    let schema = build_schema(Database::open(dir.path()).unwrap());
    (dir, schema)
}
async fn query(schema: &ApiSchema, source: &str) -> Value {
    let response = schema.execute(source).await;
    assert!(
        response.errors.is_empty(),
        "GraphQL failed: {:?}",
        response.errors
    );
    response.data.into_json().unwrap()
}
async fn seed(schema: &ApiSchema) {
    query(schema, "mutation { insert_links(objects: [{from_id:1,to_id:1},{from_id:2,to_id:2},{from_id:1,to_id:2},{from_id:1,to_id:0},{from_id:2,to_id:0}]) { affected_rows } }").await;
}

#[actix_web::test]
async fn documented_crud_and_primary_keys() {
    let (_dir, schema) = fixture();
    assert_eq!(
        query(
            &schema,
            "mutation { insert_links_one(object: {from_id:0,to_id:0}) {id from_id to_id} }"
        )
        .await,
        json!({"insert_links_one":{"id":1,"from_id":0,"to_id":0}})
    );
    assert_eq!(query(&schema, "mutation { update_links(_set:{from_id:1,to_id:1},where:{id:{_eq:1}}) {affected_rows returning{id from_id to_id}} }").await,
        json!({"update_links":{"affected_rows":1,"returning":[{"id":1,"from_id":1,"to_id":1}]}}));
    assert_eq!(
        query(&schema, "{links_by_pk(id:1){id from{id} to{id}}}").await,
        json!({"links_by_pk":{"id":1,"from":{"id":1},"to":{"id":1}}})
    );
    assert_eq!(
        query(
            &schema,
            "mutation {update_links_by_pk(pk_columns:{id:1},_inc:{to_id:1}){id to_id}}"
        )
        .await,
        json!({"update_links_by_pk":{"id":1,"to_id":2}})
    );
    assert_eq!(
        query(&schema, "mutation {delete_links_by_pk(id:1){id}}").await,
        json!({"delete_links_by_pk":{"id":1}})
    );
    assert_eq!(
        query(&schema, "{links_by_pk(id:1){id} links{id}}").await,
        json!({"links_by_pk":null,"links":[]})
    );
    assert_eq!(
        query(&schema, "mutation {delete_links_by_pk(id:99){id}}").await,
        json!({"delete_links_by_pk":null})
    );
    assert_eq!(
        query(
            &schema,
            "mutation {update_links_by_pk(pk_columns:{id:99},_set:{to_id:0}){id}}"
        )
        .await,
        json!({"update_links_by_pk":null})
    );
}

#[actix_web::test]
async fn batch_crud_returns_affected_rows_and_deduplicates_insert() {
    let (_dir, schema) = fixture();
    seed(&schema).await;
    let duplicate = query(
        &schema,
        "mutation {insert_links_one(object:{from_id:1,to_id:2}){id}}",
    )
    .await;
    assert_eq!(duplicate, json!({"insert_links_one":{"id":3}}));
    let updated = query(&schema, "mutation {update_links(where:{id:{_in:[4,5]}},_set:{to_id:9}){affected_rows returning{id to_id}}}").await;
    assert_eq!(
        updated,
        json!({"update_links":{"affected_rows":2,"returning":[{"id":4,"to_id":9},{"id":5,"to_id":9}]}})
    );
    let deleted = query(
        &schema,
        "mutation {delete_links(where:{to_id:{_eq:9}}){affected_rows returning{id}}}",
    )
    .await;
    assert_eq!(
        deleted,
        json!({"delete_links":{"affected_rows":2,"returning":[{"id":4},{"id":5}]}})
    );
}

#[actix_web::test]
async fn ordering_distinct_offset_limit_and_nested_arguments() {
    let (_dir, schema) = fixture();
    seed(&schema).await;
    assert_eq!(
        query(
            &schema,
            "{links(order_by:{id:desc},distinct_on:[from_id],offset:1,limit:1){id from_id}}"
        )
        .await,
        json!({"links":[{"id":4,"from_id":1}]})
    );
    assert_eq!(
        query(
            &schema,
            "{links(order_by:[{from_id:desc},{to_id:asc}],limit:3){id}}"
        )
        .await,
        json!({"links":[{"id":5},{"id":2},{"id":4}]})
    );
    assert_eq!(query(&schema, "{links_by_pk(id:1){out(where:{to_id:{_gte:1}},order_by:{id:desc},offset:1,limit:1){id} in{id}}}").await,
        json!({"links_by_pk":{"out":[{"id":1}],"in":[{"id":1}]}}));
    assert_eq!(
        query(&schema, "{links(limit:0){id}}").await,
        json!({"links":[]})
    );
}

#[actix_web::test]
async fn logical_filters_are_conjunctive_and_relationship_filters_are_existential() {
    let (_dir, schema) = fixture();
    seed(&schema).await;
    assert_eq!(
        query(
            &schema,
            "{links(where:{id:{_eq:3},_or:[{from_id:{_eq:2}}]}){id}}"
        )
        .await,
        json!({"links":[]})
    );
    assert_eq!(query(&schema, "{links(where:{_and:[{from_id:{_eq:1}},{_not:{to_id:{_eq:0}}}],_or:[{id:{_eq:3}}]}){id}}").await, json!({"links":[{"id":3}]}));
    assert_eq!(
        query(&schema, "{links(where:{_or:[]}){id}}").await,
        json!({"links":[]})
    );
    assert_eq!(
        query(
            &schema,
            "{links(where:{to:{id:{_eq:2}},from:{id:{_eq:1}}}){id}}"
        )
        .await,
        json!({"links":[{"id":3}]})
    );
    assert_eq!(
        query(&schema, "{links(where:{out:{id:{_eq:3}}}){id}}").await,
        json!({"links":[{"id":1}]})
    );
    assert_eq!(
        query(&schema, "{links(where:{in:{from_id:{_eq:1}}}){id}}").await,
        json!({"links":[{"id":1},{"id":2}]})
    );
}

#[actix_web::test]
async fn all_comparison_operators_and_zero_reference_null_semantics() {
    let (_dir, schema) = fixture();
    seed(&schema).await;
    assert_eq!(
        query(
            &schema,
            "{links(where:{id:{_gt:1,_gte:2,_lt:5,_lte:4,_neq:3,_in:[2,3,4],_nin:[4]}}){id}}"
        )
        .await,
        json!({"links":[{"id":2}]})
    );
    assert_eq!(
        query(&schema, "{links(where:{to_id:{_is_null:true}}){id to{id}}}").await,
        json!({"links":[{"id":4,"to":null},{"id":5,"to":null}]})
    );
    assert_eq!(
        query(&schema, "{links(where:{id:{_eq:-1}}){id}}").await,
        json!({"links":[]})
    );
    assert_eq!(
        query(&schema, "{links(where:{id:{_gt:-1}},limit:1){id}}").await,
        json!({"links":[{"id":1}]})
    );
}

#[actix_web::test]
async fn rejects_invalid_mutations_without_partial_validation_writes() {
    let (_dir, schema) = fixture();
    seed(&schema).await;
    let before = query(&schema, "{links{id from_id to_id}}").await;
    for source in [
        "mutation {insert_links(objects:[{from_id:4,to_id:4},{from_id:-1,to_id:0}]){affected_rows}}",
        "mutation {update_links(where:{id:{_in:[4,5]}},_set:{from_id:1,to_id:9}){affected_rows}}",
        "mutation {update_links(where:{id:{_eq:4}},_set:{to_id:-1}){affected_rows}}",
        "mutation {update_links(where:{id:{_eq:4}},_inc:{to_id:-1}){affected_rows}}",
        "mutation {update_links(where:{id:{_eq:4}},_set:{to_id:2},_inc:{to_id:1}){affected_rows}}",
        "mutation {update_links(where:{id:{_eq:4}}){affected_rows}}",
        "mutation {delete_links_by_pk(id:-1){id}}",
    ] {
        assert!(!schema.execute(source).await.errors.is_empty(), "accepted {source}");
        assert_eq!(query(&schema, "{links{id from_id to_id}}").await, before);
    }
    assert!(!schema
        .execute("{links(limit:-1){id}}")
        .await
        .errors
        .is_empty());
    assert!(!schema
        .execute("{links(offset:-1){id}}")
        .await
        .errors
        .is_empty());
    assert!(!schema
        .execute("{links_by_pk(id:0){id}}")
        .await
        .errors
        .is_empty());
}

#[actix_web::test]
async fn persistent_reopening_and_exclusive_directory_lock() {
    let dir = tempfile::tempdir().unwrap();
    let database = Database::open(dir.path()).unwrap();
    assert!(Database::open(dir.path()).is_err());
    let schema = build_schema(database);
    seed(&schema).await;
    let expected = query(&schema, "{links{id from_id to_id}}").await;
    drop(schema);
    let reopened = build_schema(Database::open(dir.path()).unwrap());
    assert_eq!(
        query(&reopened, "{links{id from_id to_id}}").await,
        expected
    );
}

#[actix_web::test]
async fn deleting_referenced_link_keeps_addresses_and_returns_null_relationship() {
    let (_dir, schema) = fixture();
    seed(&schema).await;
    query(&schema, "mutation {delete_links_by_pk(id:1){id}}").await;
    assert_eq!(
        query(&schema, "{links_by_pk(id:3){id from_id from{id} to{id}}}").await,
        json!({"links_by_pk":{"id":3,"from_id":1,"from":null,"to":{"id":2}}})
    );
    assert_eq!(
        query(&schema, "mutation {delete_links(where:{}){affected_rows}}").await,
        json!({"delete_links":{"affected_rows":4}})
    );
}

#[actix_web::test]
async fn http_routes_execute_graphql_and_reject_unsupported_schema_fields() {
    let (_dir, schema) = fixture();
    let app = test::init_service(
        App::new()
            .app_data(web::Data::new(schema))
            .configure(configure),
    )
    .await;
    let request = test::TestRequest::post()
        .uri("/v1/graphql")
        .set_json(json!({"query":"mutation {insert_links_one(object:{from_id:1,to_id:1}){id}}"}))
        .to_request();
    let response: Value = test::call_and_read_body_json(&app, request).await;
    assert_eq!(response, json!({"data":{"insert_links_one":{"id":1}}}));
    let request = test::TestRequest::post()
        .uri("/")
        .set_json(json!({"query":"{links{id from{id}}}"}))
        .to_request();
    let response: Value = test::call_and_read_body_json(&app, request).await;
    assert_eq!(
        response,
        json!({"data":{"links":[{"id":1,"from":{"id":1}}]}})
    );
    let request = test::TestRequest::post()
        .uri("/v1/graphql")
        .set_json(json!({"query":"{guest{id}}"}))
        .to_request();
    let response: Value = test::call_and_read_body_json(&app, request).await;
    assert!(response["errors"].is_array());
    let response = test::call_service(
        &app,
        test::TestRequest::get().uri("/ui/playground").to_request(),
    )
    .await;
    assert!(response.status().is_success());
}

#[actix_web::test]
async fn bulk_increment_checks_final_pairs_not_transient_collisions() {
    let (_dir, schema) = fixture();
    query(
        &schema,
        "mutation {insert_links(objects:[{from_id:1,to_id:0},{from_id:2,to_id:0}]){affected_rows}}",
    )
    .await;
    assert_eq!(query(&schema, "mutation {update_links(where:{},_inc:{from_id:1}){affected_rows returning{id from_id to_id}}}").await,
        json!({"update_links":{"affected_rows":2,"returning":[{"id":1,"from_id":2,"to_id":0},{"id":2,"from_id":3,"to_id":0}]}}));
    assert_eq!(query(&schema, "mutation {update_links(where:{},_inc:{from_id:-1}){affected_rows returning{id from_id}}}").await,
        json!({"update_links":{"affected_rows":2,"returning":[{"id":1,"from_id":1},{"id":2,"from_id":2}]}}));
    let invalid = schema
        .execute("mutation {update_links(where:{},_set:{from_id:7}){affected_rows}}")
        .await;
    assert!(!invalid.errors.is_empty());
    assert_eq!(
        query(&schema, "{links{id from_id to_id}}").await,
        json!({"links":[{"id":1,"from_id":1,"to_id":0},{"id":2,"from_id":2,"to_id":0}]})
    );
}

#[actix_web::test]
async fn duplicate_batch_inputs_return_one_result_per_input() {
    let (_dir, schema) = fixture();
    assert_eq!(query(&schema, "mutation {insert_links(objects:[{from_id:0,to_id:0},{from_id:0,to_id:0}]){affected_rows returning{id}}}").await,
        json!({"insert_links":{"affected_rows":2,"returning":[{"id":1},{"id":1}]}}));
    assert_eq!(
        query(&schema, "{links{id}}").await,
        json!({"links":[{"id":1}]})
    );
}

#[actix_web::test]
async fn reject_deep_filters_and_address_arithmetic_overflow() {
    let (_dir, schema) = fixture();
    query(
        &schema,
        "mutation {insert_links_one(object:{from_id:9223372036854775807,to_id:0}){id}}",
    )
    .await;
    let overflow = schema
        .execute("mutation {update_links(where:{},_inc:{from_id:1}){affected_rows}}")
        .await;
    assert!(!overflow.errors.is_empty());
    assert_eq!(
        query(&schema, "{links{from_id}}").await,
        json!({"links":[{"from_id":i64::MAX}]})
    );
    let mut filter = "{}".to_owned();
    for _ in 0..34 {
        filter = format!("{{_not:{filter}}}");
    }
    let deep = schema
        .execute(format!("{{links(where:{filter}){{id}}}}"))
        .await;
    assert!(!deep.errors.is_empty());
    assert_eq!(
        query(&schema, "{links{id}}").await,
        json!({"links":[{"id":1}]})
    );
}

#[actix_web::test]
async fn deleted_slots_reopen_and_are_reused_without_losing_other_links() {
    let (dir, schema) = fixture();
    seed(&schema).await;
    query(
        &schema,
        "mutation {delete_links(where:{id:{_in:[2,4]}}){affected_rows}}",
    )
    .await;
    let before = query(&schema, "{links{id from_id to_id}}").await;
    drop(schema);
    let reopened = build_schema(Database::open(dir.path()).unwrap());
    assert_eq!(query(&reopened, "{links{id from_id to_id}}").await, before);
    let inserted = query(
        &reopened,
        "mutation {insert_links(objects:[{from_id:9,to_id:0},{from_id:0,to_id:9}]){returning{id}}}",
    )
    .await;
    let mut ids: Vec<_> = inserted["insert_links"]["returning"]
        .as_array()
        .unwrap()
        .iter()
        .map(|r| r["id"].as_u64().unwrap())
        .collect();
    ids.sort();
    assert_eq!(ids, vec![2, 4]);
    let before = query(&reopened, "{links{id from_id to_id}}").await;
    drop(reopened);
    let twice = build_schema(Database::open(dir.path()).unwrap());
    assert_eq!(query(&twice, "{links{id from_id to_id}}").await, before);
    query(&twice, "mutation {delete_links(where:{}){affected_rows}}").await;
    drop(twice);
    let emptied = build_schema(Database::open(dir.path()).unwrap());
    assert_eq!(query(&emptied, "{links{id}}").await, json!({"links":[]}));
}

#[actix_web::test]
async fn malformed_native_files_are_rejected_without_modification() {
    use std::io::{Seek, SeekFrom, Write};
    for (word, value) in [
        (0, 1_048_575u64),
        (4, 999),
        (8 + 2, 1),
        (8 + 4, 99),
        (16 + 2, 1),
    ] {
        let (dir, schema) = fixture();
        query(
            &schema,
            "mutation {insert_links_one(object:{from_id:1,to_id:1}){id}}",
        )
        .await;
        drop(schema);
        let path = dir.path().join("db.links");
        let mut file = std::fs::OpenOptions::new().write(true).open(&path).unwrap();
        file.seek(SeekFrom::Start(word * 8)).unwrap();
        file.write_all(&value.to_ne_bytes()).unwrap();
        drop(file);
        let before = std::fs::read(&path).unwrap();
        assert!(
            Database::open(dir.path()).is_err(),
            "accepted invalid header/tree word {word}"
        );
        assert_eq!(std::fs::read(&path).unwrap(), before);
    }
    let directory = tempfile::tempdir().unwrap();
    std::fs::write(
        directory.path().join("db.links"),
        b"legacy or truncated database",
    )
    .unwrap();
    assert!(Database::open(directory.path()).is_err());
    assert_eq!(
        std::fs::read(directory.path().join("db.links")).unwrap(),
        b"legacy or truncated database"
    );
}

#[actix_web::test]
async fn inactive_index_metadata_is_rejected_before_it_can_be_attached() {
    use std::io::{Seek, SeekFrom, Write};
    let (directory, schema) = fixture();
    query(
        &schema,
        "mutation {insert_links_one(object:{from_id:0,to_id:1}){id}}",
    )
    .await;
    drop(schema);
    let path = directory.path().join("db.links");
    let mut file = std::fs::OpenOptions::new().write(true).open(&path).unwrap();
    file.seek(SeekFrom::Start((8 + 2) * 8)).unwrap();
    file.write_all(&1u64.to_ne_bytes()).unwrap();
    drop(file);
    assert!(Database::open(directory.path()).is_err());
}
