/// https://www.codewars.com/kata/5727bb0fe81185ae62000ae3
fn clean_string(s: &str) -> String {
    let backspace = |agg: String, ch: char| {
        match (agg.as_str(), ch) {
            ("", '#') => agg,
            (v, '#') => String::from(&v[..v.len()-1]),
            (_, _) => format!("{}{}", agg, ch)
        }
    };
    s.chars().fold("".to_string(), backspace)  
}

#[cfg(test)]
mod unit_tests {
    use rstest::rstest;
    use super::*;

    #[rstest]
    #[case("abc#d##c", String::from("ac"))]
    #[case("#d##c", String::from("c"))]
    #[case("######", String::from(""))]
    #[case("", String::from(""))]
    fn clean_string_test(#[case] input: &str, #[case] expected: String) {
        assert_eq!(clean_string(input), expected)
    }
}