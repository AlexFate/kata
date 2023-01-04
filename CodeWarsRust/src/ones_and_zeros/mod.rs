/// https://www.codewars.com/kata/578553c3a1b8d5c40300037c/
fn binary_slice_to_number(slice: &[u32]) -> u32 {
    slice.into_iter()
    .rev()
    .enumerate()
    .map(|(i, &item)| {
        match item {
            1 => 2_u32.pow(i as u32),
            _ => 0
        }
    }).sum()
}

#[cfg(test)]
mod unit_tests {
    use rstest::rstest;
    use super::*;

    #[rstest]
    #[case(&[1], 1)]
    #[case(&[0, 1], 1)]
    #[case(&[1, 0], 2)]
    #[case(&[1, 0, 1, 0], 10)]
    fn binary_slice_to_number_test(#[case] slice: &[u32], #[case] expected: u32) {
        assert_eq!(binary_slice_to_number(slice), expected)
    }
}