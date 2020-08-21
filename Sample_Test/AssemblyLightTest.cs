using System.Collections.Generic;
using CodeWars;
using Xunit;

namespace Sample_Test
{
    public class AssemblyLightTest
    {
        [Fact]
        public void Test1()
        {
            var inputs = new string[]
            {
                "mov a 5","inc a","dec a","dec a","jnz a -1","inc a"
            };
            var expected = new Dictionary<string, int>()
            {
                {"a", 1}
            };
            Assert.Equal(expected, Kata.Interpret(inputs));
        }        
        
        [Fact]
        public void Test2()
        {
            var inputs = new string[]
            {
                "mov b 2", "mov a b","inc a","inc a","dec a","dec a","inc a"
            };
            var expected = new Dictionary<string, int>()
            {
                {"b", 2},
                {"a", 3}
            };
            Assert.Equal(expected, Kata.Interpret(inputs));
        }        
        [Fact]
        public void Test3()
        {
            var inputs = new string[]
            {
                "mov b 2", "mov a b", "jnz b 2","inc a","inc a","dec b", "jnz b -4","dec a","inc a"
            };
            var expected = new Dictionary<string, int>()
            {
                {"b", 0},
                {"a", 4}
            };
            Assert.Equal(expected, Kata.Interpret(inputs));
        }        
        [Fact]
        public void Test4()
        {
            var inputs = new string[]
            {
                "mov a -10", "mov b a", "inc a", "dec b", "jnz a -2"
            };
            var expected = new Dictionary<string, int>()
            {
                {"a", 0},
                {"b", -20}
            };
            Assert.Equal(expected, Kata.Interpret(inputs));
        }
    }
}