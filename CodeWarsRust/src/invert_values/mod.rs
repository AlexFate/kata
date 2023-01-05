/// https://www.codewars.com/kata/5899dc03bc95b1bf1b0000ad/
fn invert(values: &[i32]) -> Vec<i32> {
    values.iter().map(|v| !v + 1).collect()
}

#[cfg(test)]
mod unit_tests {
    use rstest::rstest;
    use super::*;

    #[rstest]
    #[case(&[1, 0], vec![-1, 0])]
    #[case(&[4, -3, 2, -1, 0, 1], vec![-4, 3, -2, 1, 0, -1])]
    fn invert_test(#[case] values: &[i32], #[case] expected: Vec<i32>) {
        assert_eq!(invert(values), expected)
    }
}