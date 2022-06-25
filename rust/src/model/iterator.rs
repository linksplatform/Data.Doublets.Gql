pub trait AtLeastAnyExt
where
    Self: Iterator,
{
    fn least_all<F>(&mut self, mut f: F) -> bool
    where
        Self: Sized,
        F: FnMut(Self::Item) -> bool,
    {
        let mut marker = false;
        self.all(|item| {
            marker = true;
            f(item)
        }) && marker
    }
}

impl<All: Iterator> AtLeastAnyExt for All {}
