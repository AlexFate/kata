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
    #[case(5, 80)]
    #[case(7, 216)]
    fn perimetr_sum_test(#[case] input: u32, #[case] expected: u32) {
        assert_eq!(perimetr(input), expected);
    }
}