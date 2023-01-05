/// https://www.codewars.com/kata/54da539698b8a2ad76000228
fn is_valid_walk(walk: &[char]) -> bool {
    if walk.len() != 10 {
        false
    }
    else {
        let result_position = walk.into_iter()
        .map(to_direction)
        .fold((0, 0), |agg, i| {
            match (agg, i) {
                ((cur_vert,cur_h), (vert, hor)) => (cur_vert + vert, cur_h + hor)
            }
        });
        result_position == (0, 0)
    }
}

fn to_direction(ch: &char) -> (i32, i32) {
    match ch {
        'n' => (1, 0),
        's' => (-1, 0),
        'w' => (0, 1),
        'e' => (0, -1),
        _ => panic!("Unsupported direction")
    }
}

#[cfg(test)]
mod unit_tests {
    use rstest::rstest;
    use super::*;

    #[rstest]
    #[case(&['n','s','n','s','n','s','n','s','n','s'], true)]
    #[case(&['n','n','n','s','n','s','n','s','n','s'], false)]
    #[case(&['n','n','n','s','n'], false)]
    #[case(&['n','s','n','s','n','s','n','s','n','s','s'], false)]
    fn is_valid_walk_test(#[case] row: &[char], #[case] expected: bool) {
        assert_eq!(is_valid_walk(row), expected)
    }
}