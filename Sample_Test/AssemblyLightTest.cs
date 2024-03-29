using System.Collections.Generic;
using CodeWars;
using Xunit;

namespace Sample_Test;

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
    [Fact]
    public void Test5()
    {
        var inputs = new string[]
        {
            "mov a 1", "mov b 1", "mov c 0", "mov d 26", "jnz c 2", "jnz 1 5", "mov c 7", "inc d", "dec c", "jnz c -2", 
            "mov c a", "inc a", "dec b", "jnz b -2", "mov b c", "dec d", "jnz d -6", "mov c 18", "mov d 11", "inc a", 
            "dec d", "jnz d -2", "dec c", "jnz c -5"
        };
        var expected = new Dictionary<string, int>()
        {
            {"a", 318009},
            {"b", 196418},
            {"c", 0},
            {"d", 0}
        };
        Assert.Equal(expected, Kata.Interpret(inputs));
    }
}