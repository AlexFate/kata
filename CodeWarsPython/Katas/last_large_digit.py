import math
import unittest


class Test(unittest.TestCase):

    def test_upper(self):
        self.assertEqual(last_digit("4", "1"), 4)
        self.assertEqual(last_digit(3715290469715693021198967285016729344580685479654510946723,
                                    68819615221552997273737174557165657483427362207517952651), 7)


def get_on_four_remainder(s_input):
    result = 0
    for char in s_input:
        result = (result * 10 + int(char)) % 4
    return result


def is_number_zero(s_input):
    return len(s_input) == 1 and s_input[0] == '0'


def last_digit(n1, n2):
    n1 = str(n1)
    n2 = str(n2)
    if is_number_zero(n2) or is_number_zero(n1) and is_number_zero(n2):
        return 1
    if is_number_zero(n1):
        return 0
    remainder = get_on_four_remainder(n2)
    if remainder == 0:
        exp = 4
    else:
        exp = remainder

    result = math.pow(int(n1[-1]), exp)
    return result % 10