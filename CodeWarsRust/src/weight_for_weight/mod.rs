fn order_weight(input: &str) -> String {
    let mut result: Vec<&str> = input.split(' ')
        .into_iter()
        .filter(|x| !(x.is_empty()))
        .collect();

    result.sort_by(|left, right| calc_weight(left).cmp(&calc_weight(right)).then(left.cmp(right)));

    result.join(" ")
}

fn calc_weight(num: &str) -> usize {
    num.as_bytes().into_iter().map(|x| x - b'0').sum::<u8>().into()
}

#[cfg(test)]
mod unit_tests {
    use rstest::rstest;
    use super::*;

    #[rstest()]
    #[case(" 56 65 74 100 99  68 86 180 90 ", "100 180 90 56 65 74 68 86 99")]
    #[case("2000 10003 1234000 44444444 9999 11 11 22 123", "11 11 2000 10003 22 123 1234000 44444444 9999")]
    fn test_some(#[case] input: &str, #[case] expected: &str) {
        assert_eq!(order_weight(input), expected)
    }

    #[rstest()]
    #[case("123", 6)]
    #[case("223", 7)]
    fn test_cac_weight(#[case] input: &str, #[case] expected: usize) {
        assert_eq!(calc_weight(input), expected)
    }
}