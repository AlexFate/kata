fn rgb(r: i32, g: i32, b: i32) -> String {
    let (r, g, b) = round_to_nearest_valid_u8(r, g, b);
    format!("{:02X}{:02X}{:02X}", r, g, b)
}

fn round_to_nearest_valid_u8(r: i32, g: i32, b: i32) -> (u8, u8, u8) {
    let round_u8 = |x| if x < 0 { 0u8 } else if x > 255 { 255u8 } else {x as u8};
    (round_u8(r), round_u8(g), round_u8(b))
}


#[cfg(test)]
mod unit_tests {
    use rstest::rstest;
    use super::*;

    #[rstest]
    #[case((0, 0, 0), "000000")]
    #[case((1, 2, 3), "010203")]
    #[case((255, 255, 255), "FFFFFF")]
    #[case((-20, 275, 125), "00FF7D")]
    fn to_hex_test(#[case] input: (i32, i32, i32), #[case] expected: String) {
        let (r, g, b) = input;
        assert_eq!(rgb(r, g, b), expected);
    }

    #[rstest]
    #[case((-20, 275, 125), (0, 255, 125))]
    fn round_to_nearest_valid_u8_test(#[case] input: (i32, i32, i32), #[case] expected: (u8, u8, u8)) {
        let (r, g, b) = input;
        assert_eq!(round_to_nearest_valid_u8(r, g, b), expected);
    }
}