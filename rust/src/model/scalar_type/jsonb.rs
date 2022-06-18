use async_graphql::*;
#[derive(Debug, Clone)]
pub struct Jsonb(!);
#[Scalar(name = "jsonb")]
impl ScalarType for Jsonb {
    fn parse(value: Value) -> InputValueResult<Self> {
        todo!()
    }
    fn to_value(&self) -> Value {
        todo!()
    }
}
