/// https://www.codewars.com/kata/56bc28ad5bdaeb48760009b0
pub fn remove_char(s: &str) -> String {
    s[1..s.len()-1].to_string()
}

#[cfg(test)]
mod unit_tests {
    use rstest::rstest;
    use super::*;

    #[rstest]
    #[case("input", String::from("npu"))]
    #[case(" input ", String::from("input"))]
    fn perimetr_sum_test(#[case] input: &str, #[case] expected: String) {
        assert_eq!(remove_char(input), expected);
    }
}