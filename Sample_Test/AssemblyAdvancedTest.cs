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
        public void ProgramExecutionTests(string commands, Dictionary<string, int> expectedMem, string expectedOutput)
        {
            Assert.Equal(expectedMem, Kata.AssemblyAdvanced.Interpret(commands));
            Assert.Equal(expectedOutput, Kata.AssemblyAdvanced.GetOutput());
        }
    }
    
    public class CommandTestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {"mov a, 5", new Dictionary<string, int> {["a"] = 5}, null},
            new object[] {"mov a, 5\nmov b, a", new Dictionary<string, int> {["a"] = 5, ["b"] = 5}, null},
            new object[] {"mov a, 5\ninc a\ndec a\ndec a", new Dictionary<string, int> {["a"] = 4}, null},
            new object[] {"mov a, 3\nmsg '(5+1)/2 = ', a   ; output message\n", new Dictionary<string, int> {["a"] = 3}, "(5+1)/2 = 3"},
            new object[] {"mov a, 3\ncall f1\ndec a\nend\nf1:\ndec a\nmul a, 2\nret", new Dictionary<string, int> {["a"] = 3}, null},
            new object[] {"\nmov   a, 5\nmov   b, a\nmov   c, a\ncall  proc_fact\ncall  print\nend\n\nproc_fact:\n    dec   b\n    mul   c, b\n    cmp   b, 1\n    jne   proc_fact\n    ret\n\nprint:\n    msg   a, '! = ', c ; output text\n    ret\n", new Dictionary<string, int> {["a"] = 5, ["b"] = 1, ["c"] = 120}, "5! = 120"}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}