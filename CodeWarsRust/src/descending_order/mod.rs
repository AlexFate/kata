/// https://www.codewars.com/kata/5467e4d82edf8bbf40000155
fn descending_order(x: u64) -> u64 {
    let mut chars: Vec<char> = x.to_string().chars().collect();
    chars.sort_by(|a, b| b.cmp(a));
    let result: String = chars.into_iter().collect();
    result.parse().unwrap()
}

#[cfg(test)]
mod unit_tests {
    use rstest::rstest;
    use super::*;

    #[rstest]
    #[case(0, 0)]
    #[case(123, 321)]
    #[case(481, 841)]
    #[case(987, 987)]
    fn row_sum_odd_numbers_test(#[case] input: u64, #[case] expected: u64) {
        assert_eq!(descending_order(input), expected)
    }
}