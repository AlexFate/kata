fn oper<F: Fn(String) -> String>(fct: F, s: String) -> String {
    fct(s)
}

fn vert_mirror(input: String) -> String {
    input.split("\n")
            .into_iter()
            .map(|sub| sub.chars().rev().collect::<String>())
            .collect::<Vec<String>>()
            .join("\n")
}

fn hor_mirror(input: String) -> String {
    input.split('\n')
            .rev()
            .collect::<Vec<&str>>()
            .join("\n")
}

#[cfg(test)]
mod unit_tests {
    use rstest::rstest;
    use super::*;

    #[rstest()]
    #[case("abcd\nefgh\nijkl\nmnop", "dcba\nhgfe\nlkji\nponm")]
    fn test_vert_mirror(#[case] input: &str, #[case] expected: &str) {
        assert_eq!(vert_mirror(input.to_string()), expected)
    }

    #[rstest()]
    #[case("abcd\nefgh\nijkl\nmnop", "mnop\nijkl\nefgh\nabcd")]
    fn test_hor_mirror(#[case] input: &str, #[case] expected: &str) {
        assert_eq!(hor_mirror(input.to_string()), expected)
    }

    #[rstest]
    #[case("abcd\nefgh\nijkl\nmnop", "dcba\nhgfe\nlkji\nponm", &vert_mirror)]
    #[case("abcd\nefgh\nijkl\nmnop", "mnop\nijkl\nefgh\nabcd", &hor_mirror)]
    fn test_oper(#[case] input: &str, #[case] expected: &str, #[case] f: &dyn Fn(String) -> String) {
        assert_eq!(oper(f, input.to_string()), expected)
    }
}