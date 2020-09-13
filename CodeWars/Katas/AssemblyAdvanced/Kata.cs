using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CodeWars
{
    public partial class Kata
    {
        public static string AssemblyAdvancedInterpret(string input) => new AssemblyAdvanced().Interpret(input);
    }

    public class AssemblyAdvanced
    {
        private static class Comparer
        {
            public static int A;
            public static int B;
            public static bool InvokeComparison(Func<int, int, bool> predicate)
            {
                return predicate.Invoke(A, B);
            }
        }
        private readonly Dictionary<string, int> _variableValue = new Dictionary<string, int>();
        private string _output = null;
        private int _instructionPointer = 0;
        private readonly Stack<int> _returnPointerStack = new Stack<int>();
        private readonly Dictionary<string, int> _functionPointers = new Dictionary<string, int>();
        private Dictionary<string, Action<string, string>> InstructionActions =>
            new Dictionary<string, Action<string, string>>
            {
                ["mov"] = Move,
                ["inc"] = Increase,
                ["dec"] = Decrease,
                ["add"] = Add,
                ["sub"] = Subtract,
                ["mul"] = Multiply,
                ["div"] = Divide,
                ["call"] = Call,
                ["ret"] = Return,
                ["jmp"] = Jump,
                ["msg"] = Message,
                ["cmp"] = Compare,
                ["jne"] = JumpNotEqual,
                ["je"] = JumpEqual,
                ["jge"] = JumpGreaterOrEqual,
                ["jg"] = JumpGreater,
                ["jle"] = JumpLessOrEqual,
                ["jl"] = JumpLess,
                ["end"] = End
            };

        private void JumpLess(string arg1, string arg2)
        {
            if (!Comparer.InvokeComparison((i, i1) => i < i1))
                return;
            Jump(arg1, null);
        }

        private void JumpLessOrEqual(string arg1, string arg2)
        {
            if (!Comparer.InvokeComparison((i, i1) => i == i1 || i < i1))
                return;
            Jump(arg1, null);
        }

        private void JumpGreater(string arg1, string arg2)
        {
            if (!Comparer.InvokeComparison((i, i1) => i > i1))
                return;
            Jump(arg1, null);
        }

        private void JumpGreaterOrEqual(string arg1, string arg2)
        {
            if (!Comparer.InvokeComparison((i, i1) => i == i1 || i > i1))
                return;
            Jump(arg1, null);
        }
        private void JumpEqual(string arg1, string arg2)
        {
            if (!Comparer.InvokeComparison((i, i1) => i == i1))
                return;
            Jump(arg1, null);
        }
        private void JumpNotEqual(string arg1, string arg2)
        {
            if (!Comparer.InvokeComparison((i, i1) => i != i1))
                return;
            Jump(arg1, null);
        }
        private void Compare(string arg1, string arg2)
        {
            Comparer.A = _variableValue[arg1];
            Comparer.B = int.TryParse(arg2, out var intValue) ? intValue : _variableValue[arg2];
        }
        private void End(string arg1, string arg2) => _instructionPointer = int.MaxValue - 2;
        private void Jump(string arg1, string arg2) => _instructionPointer = _functionPointers[arg1];
        private void Return(string arg1, string arg2) => _instructionPointer = _returnPointerStack.Pop(); 
        private void Call(string arg1, string arg2)
        {
            _returnPointerStack.Push(_instructionPointer);
            _instructionPointer = _functionPointers[arg1];
        }
        private void Message(string arg1, string arg2)
        {
            // TODO: Something should be here
            //_output += string.Join("", outputArgs);
        }
        private void Add(string arg1, string arg2)
            => _variableValue[arg1] += _variableValue.ContainsKey(arg2) ? _variableValue[arg2] : int.Parse(arg2);
        private void Subtract(string arg1, string arg2)
            => _variableValue[arg1] -= _variableValue.ContainsKey(arg2) ? _variableValue[arg2] : int.Parse(arg2);
        private void Multiply(string arg1, string arg2)
            => _variableValue[arg1] *= _variableValue.ContainsKey(arg2) ? _variableValue[arg2] : int.Parse(arg2);
        private void Divide(string arg1, string arg2)
            => _variableValue[arg1] /= _variableValue.ContainsKey(arg2) ? _variableValue[arg2] : int.Parse(arg2);
        private void Decrease(string arg1, string arg2 = null) => _variableValue[arg1]--;
        private void Increase(string arg1, string arg2 = null) => _variableValue[arg1]++;
        private void Move(string nameA, string nameB)
        {
            var isInt = int.TryParse(nameB, out var intValue);
            if (!_variableValue.ContainsKey(nameA))
                _variableValue.Add(nameA, isInt ? intValue : _variableValue[nameB]);
            else _variableValue[nameA] = isInt ? intValue : _variableValue[nameB];
        }

        public string Interpret(string program)
        {
            _output = null;
            var lines = 
                program.Split(Environment.NewLine)
                    .Where(item => !item.StartsWith(";"))
                    .Select(item => !item.Contains(";") ? item : string.Join("", item.TakeWhile(ch => ch != ';')))
                    .Where(item => !string.IsNullOrEmpty(item))
                    .ToImmutableList();
            lines.Where(str => str.EndsWith(':')).ToImmutableList().ForEach(item => _functionPointers.Add(item.Replace(":",""), lines.IndexOf(item)));
            Interpret(lines);
            return _output;
        }

        private void Interpret(IReadOnlyList<string> program)
        {
            while (_instructionPointer < program.Count)
            {
                ProcessInstruction(program[_instructionPointer]);
                _instructionPointer++;
            }
        }

        private void ProcessInstruction(string input)
        {
            if (input.Contains("msg"))
            {
                input = SkipStartEmpties(input);
                var content = input.Substring(3, input.Length - 3);
                InstructionActions["msg"].Invoke(content, null);
                return;
            }
            var parts = input
                .Split(" ")
                .Select(item => item.Replace(",", ""))
                .Where(item => !string.IsNullOrEmpty(item))
                .ToImmutableList();
            InstructionActions[parts[0]].Invoke(parts.Count == 1 ? null : parts[1], parts.Count < 3 ? null : parts[2]);
        }
        private static string SkipStartEmpties(string item) =>
            string.Join("", item.SkipWhile(ch => ch == ' '));
    }
}