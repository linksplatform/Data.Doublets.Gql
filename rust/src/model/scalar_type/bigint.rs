use async_graphql::*;
#[derive(Debug, Clone)]
pub struct Bigint(!);
#[Scalar(name = "bigint")]
impl ScalarType for Bigint {
    fn parse(value: Value) -> InputValueResult<Self> {
        todo!()
    }
    fn to_value(&self) -> Value {
        todo!()
    }
}
