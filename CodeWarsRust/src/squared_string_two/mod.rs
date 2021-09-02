use crate::squared_string_one::*;

pub fn rot(input: String) -> String {
    input.chars().rev().collect::<String>()
}

pub fn selfie_and_rot(input: String) -> String {
    input.split("\n")
        .into_iter()
        .map(|sub| sub.to_owned() + &generate_points_string(sub.chars().count()))
        .collect::<Vec<_>>()
        .join("\n") + "\n" +
    &rot(input).split("\n")
        .into_iter()
        .map(|sub| generate_points_string(sub.chars().count()) + &sub.to_owned())
        .collect::<Vec<_>>()
        .join("\n")
    
}

fn generate_points_string(count: usize) -> String {
    (0..count).map(|_| ".").collect::<String>()
}

#[cfg(test)]
mod unit_tests {
    use rstest::rstest;
    use super::*;

    #[rstest()]
    #[case("abcd\nefgh\nijkl\nmnop", "ponm\nlkji\nhgfe\ndcba")]
    fn test_rot(#[case] input: &str, #[case] expected: &str) {
        assert_eq!(rot(input.to_string()), expected)
    }

    #[rstest()]
    #[case("abcd\nefgh\nijkl\nmnop", "abcd....\nefgh....\nijkl....\nmnop....\n....ponm\n....lkji\n....hgfe\n....dcba")]
    fn test_selfie_and_rot(#[case] input: &str, #[case] expected: &str) {
        assert_eq!(selfie_and_rot(input.to_string()), expected)
    }
}