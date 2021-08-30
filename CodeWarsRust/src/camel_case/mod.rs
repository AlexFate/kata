fn camel_case<'a>(input: &'a str) -> String {
    let upper = input.to_uppercase().as_bytes().to_owned();
    let inp = input.as_bytes();
    let mut result = String::from(input);
    for i in 0 .. input.len() {
        if inp[i] == upper[i] {
            let offset = result.len() - input.len();
            result.insert(i + offset, ' ');
        }
    }
    result
}

#[cfg(test)]
mod unit_tests {
    use rstest::rstest;
    use super::*;

    #[rstest]
    #[case("camelCase", "camel Case")]
    #[case("some", "some")]
    #[case("camelCasingTest", "camel Casing Test")]
    fn camel_case_test(#[case] input: &str, #[case] expected: &str) {
        assert_eq!(camel_case(input), expected)
    }
}