using System.Collections;
using System.Collections.Generic;
using CodeWars;
using Xunit;

namespace Sample_Test;

public class AssemblyAdvancedTest
{
    [Theory]
    [ClassData(typeof(CommandTestDataGenerator))]
    public void ProgramExecutionTests(string program, Dictionary<string, int> expectedMem, string expectedOutput)
    {
        Assert.Equal(expectedOutput, Kata.AssemblyAdvancedInterpret(program));
    }
}
    
public class CommandTestDataGenerator : IEnumerable<object[]>
{
    private const string BigProgram1 =
        "\nmov   a, 8            ; value\n" +
        "mov   b, 0            ; next\n" +
        "mov   c, 0            ; counter\n" +
        "mov   d, 0            ; first\n" +
        "mov   e, 1            ; second\n" +
        "call  proc_fib\n" +
        "call  print\n" +
        "end\n" +
        "\nproc_fib:\n    cmp   c, 2\n    jl    func_0\n    mov   b, d\n    add   b, e\n    mov   d, e\n    mov   e, b\n    inc   c\n    cmp   c, a\n    jle   proc_fib\n    ret\n\nfunc_0:\n    mov   b, c\n    inc   c\n    jmp   proc_fib\n\nprint:\n    msg   'Term ', a, ' of Fibonacci series is: ', b        ; output text\n    ret\n";
    private readonly List<object[]> _data = new List<object[]>
    {

        new object[] {"mov a, 5", new Dictionary<string, int> {["a"] = 5}, null},
        new object[] {            "\nmov   a, 2            ; value1\nmov   b, 10           ; value2\nmov   c, a            ; temp1\nmov   d, b            ; temp2\ncall  proc_func\ncall  print\nend\n\nproc_func:\n    cmp   d, 1\n    je    continue\n    mul   c, a\n    dec   d\n    call  proc_func\n\ncontinue:\n    ret\n\nprint:\n    msg a, '^', b, ' = ', c\n    ret\n", null, "2^10 = 1024"},
        new object[] {"\ncall  func1\ncall  print\nend\n\nfunc1:\n    call  func2\n    ret\n\nfunc2:\n    ret\n\nprint:\n    msg 'This program should return null'\n", null, null},
        new object[] {"\nmov   a, 11           ; value1\nmov   b, 3            ; value2\ncall  mod_func\nmsg   'mod(', a, ', ', b, ') = ', d        ; output\nend\n\n; Mod function\nmod_func:\n    mov   c, a        ; temp1\n    div   c, b\n    mul   c, b\n    mov   d, a        ; temp2\n    sub   d, c\n    ret\n", null, "mod(11, 3) = 2"},
        //new object[] {"msg   'mod(', 5, ', ', 6, ') = ', 7        ; output", null, "mod(5, 6) = 7"},
        new object[] {"mov a, 5\nmov b, a", new Dictionary<string, int> {["a"] = 5, ["b"] = 5}, null},
        new object[] {"mov a, 5\ninc a\ndec a\ndec a", new Dictionary<string, int> {["a"] = 4}, null},
        //new object[] {"mov a, 3\nmsg '(5+1)/2 = ', a   ; output message\n", new Dictionary<string, int> {["a"] = 3}, "(5+1)/2 = 3"},
        new object[] {"mov a, 3\ncall f1\ndec a\nend\nf1:\ndec a\nmul a, 2\nret", new Dictionary<string, int> {["a"] = 3}, null},
        new object[] {"\nmov   a, 5\nmov   b, a\nmov   c, a\ncall  proc_fact\ncall  print\nend\n\nproc_fact:\n    dec   b\n    mul   c, b\n    cmp   b, 1\n    jne   proc_fact\n    ret\n\nprint:\n    msg   a, '! = ', c ; output text\n    ret\n", new Dictionary<string, int> {["a"] = 5, ["b"] = 1, ["c"] = 120}, "5! = 120"},
        new object[] {BigProgram1, null, "Term 8 of Fibonacci series is: 21"}
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}