using System.Collections;
using System.Collections.Generic;

using CodeWars;

using Xunit;

namespace Sample_Test
{
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
            "\nmov   a, 8            ; value\nmov   b, 0            ; next\nmov   c, 0            ; counter\nmov   d, 0            ; first\nmov   e, 1            ; second\ncall  proc_fib\ncall  print\nend\n\nproc_fib:\n    cmp   c, 2\n    jl    func_0\n    mov   b, d\n    add   b, e\n    mov   d, e\n    mov   e, b\n    inc   c\n    cmp   c, a\n    jle   proc_fib\n    ret\n\nfunc_0:\n    mov   b, c\n    inc   c\n    jmp   proc_fib\n\nprint:\n    msg   'Term ', a, ' of Fibonacci series is: ', b        ; output text\n    ret\n";
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {"mov a, 5", new Dictionary<string, int> {["a"] = 5}, null},
            new object[] {"msg   'mod(', 5, ', ', 6, ') = ', 7        ; output", null, "mod(5, 6) = 7"},
            new object[] {"mov a, 5\nmov b, a", new Dictionary<string, int> {["a"] = 5, ["b"] = 5}, null},
            new object[] {"mov a, 5\ninc a\ndec a\ndec a", new Dictionary<string, int> {["a"] = 4}, null},
            new object[] {"mov a, 3\nmsg '(5+1)/2 = ', a   ; output message\n", new Dictionary<string, int> {["a"] = 3}, "(5+1)/2 = 3"},
            new object[] {"mov a, 3\ncall f1\ndec a\nend\nf1:\ndec a\nmul a, 2\nret", new Dictionary<string, int> {["a"] = 3}, null},
            new object[] {"\nmov   a, 5\nmov   b, a\nmov   c, a\ncall  proc_fact\ncall  print\nend\n\nproc_fact:\n    dec   b\n    mul   c, b\n    cmp   b, 1\n    jne   proc_fact\n    ret\n\nprint:\n    msg   a, '! = ', c ; output text\n    ret\n", new Dictionary<string, int> {["a"] = 5, ["b"] = 1, ["c"] = 120}, "5! = 120"},
            new object[] {BigProgram1, new Dictionary<string, int> {["a"] = 5, ["b"] = 1, ["c"] = 120}, "Term 8 of Fibonacci series is: 21"}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}