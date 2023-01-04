/// https://www.codewars.com/kata/55fd2d567d94ac3bc9000064/
fn row_sum_odd_numbers(row: i64) -> i64 {
    let initial_n: i64 = (1..row).sum();
    let initial_number = 2 * initial_n + 1;
    let end_number = 2 * (initial_n + row) + 1;
    (initial_number..end_number).step_by(2).sum()
}

#[cfg(test)]
mod unit_tests {
    use rstest::rstest;
    use super::*;

    #[rstest]
    #[case(1, 1)]
    #[case(2, 8)]
    fn row_sum_odd_numbers_test(#[case] row: i64, #[case] expected: i64) {
        assert_eq!(row_sum_odd_numbers(row), expected)
    }
}