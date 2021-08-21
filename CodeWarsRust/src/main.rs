use std::cmp::max;
use std::cmp::min;
use std::collections::HashSet;

fn main() {
    todo!()
}

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

fn sq_in_rect(lng: i32, wdth: i32) -> Option<Vec<i32>> {
    if lng == wdth {
        return None;
    }
    
    let lowest = min(lng, wdth);
    let current = max(lng, wdth) - lowest;

    let mut result = vec![lowest];
    let next = sq_in_rect(lowest, current).unwrap_or(vec![lowest]);
    result.extend(next);
    Some(result)
}

fn perimetr(n: u32) -> u32 {
    let next = |n_before, n_before_before| -> u32 {
        n_before + n_before_before
    };
    let mut initial: Vec<u32> = vec![1, 1];
    for i in 2 .. (n as usize)+1 {
        initial.push(next(initial[i-1], initial[i-2]))
    }
    initial.iter().sum::<u32>() * 4
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

    #[rstest]
    #[case("camelCase", "camel Case")]
    #[case("some", "some")]
    #[case("camelCasingTest", "camel Casing Test")]
    fn camel_case_test(#[case] input: &str, #[case] expected: &str) {
        assert_eq!(camel_case(input), expected)
    }

    #[rstest()]
    #[case((5, 3), Some(vec![3, 2, 1, 1]))]
    #[case((3, 5), Some(vec![3, 2, 1, 1]))]
    #[case((5, 5), None)]
    #[case((20, 14), Some(vec![14, 6, 6, 2, 2, 2]))]
    fn sqInRect_test(#[case] input: (i32, i32), #[case] expected: Option<Vec<i32>>) {
        let (heigth, width) = input;
        assert_eq!(sq_in_rect(heigth, width), expected);
    }

    #[rstest]
    #[case(5, 80)]
    #[case(7, 216)]
    fn perimetr_sum_test(#[case] input: u32, #[case] expected: u32) {
        assert_eq!(perimetr(input), expected);
    }
}