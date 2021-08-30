use std::cmp::max;
use std::cmp::min;

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

#[cfg(test)]
mod unit_tests {
    use rstest::rstest;
    use super::*;

    #[rstest()]
    #[case((5, 3), Some(vec![3, 2, 1, 1]))]
    #[case((3, 5), Some(vec![3, 2, 1, 1]))]
    #[case((5, 5), None)]
    #[case((20, 14), Some(vec![14, 6, 6, 2, 2, 2]))]
    fn sqInRect_test(#[case] input: (i32, i32), #[case] expected: Option<Vec<i32>>) {
        let (heigth, width) = input;
        assert_eq!(sq_in_rect(heigth, width), expected);
    }
}