use std::collections::HashSet;
fn count_duplicates(input: &str) -> u32 {
    let input = input.to_uppercase();
    let chars : &HashSet<_> = &input.chars().collect();
    let counts = chars.into_iter()
        .map(|letter| input.matches(&letter.to_string()).count())
        .fold(0, |acc, item| {
        match item {
            0 | 1 => acc,
            _ => acc + 1
        }
    });
    counts
}

#[cfg(test)]
mod unit_tests {
    use rstest::rstest;
    use super::*;

    #[rstest]
    #[case("abcde", 0)]
    #[case("aabbcde", 2)]
    #[case("indivisibility", 1)]
    #[case("aA11", 2)]
    #[case("aBBA", 2)]
    fn count_duplicates_test(#[case] input: &str, #[case] expected: u32) {
        assert_eq!(count_duplicates(input), expected)
    }
}