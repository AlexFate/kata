fn range_extraction(list: &[i32]) -> String { 
    let is_gap = |left, right| right - left > 1;
    let last = list.len() - 1;
    let mut start = 0;
    let mut result = vec![];
    for current in 0..last {
        let next = current + 1;
        if is_gap(list[current], list[next]) {
            result.push((start, current));
            start = next;
        }
    }
    result.push((start, last));
    
    result.into_iter()
          .map(|(start, end)| {
            match end - start {
                0 => list[end].to_string(),
                1 => format!("{},{}", list[start], list[end]),
                _ => format!("{}-{}", list[start], list[end])
            }
          }).collect::<Vec<_>>().join(",")
}

#[cfg(test)]
mod unit_tests {
    use rstest::rstest;
    use super::*;

    #[rstest()]
    #[case(&[-6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20], "-6,-3-1,3-5,7-11,14,15,17-20")]
    #[case(&[10, 15], "10,15")]
    #[case(&[10, 11, 12, 13, 15], "10-13,15")]
    fn is_gap_test(#[case] input: &[i32], #[case] expected: String) {
        assert_eq!(range_extraction(input), expected)
    }
}