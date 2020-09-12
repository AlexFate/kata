using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CodeWars
{
    public partial class Kata
    {
        public static AssemblyAdvanced AssemblyAdvanced => new AssemblyAdvanced();
    }

    public sealed class AssemblyAdvanced
    {
        private static Dictionary<string, int> _variableValue = new Dictionary<string, int>();
        private static string _output = null;
        private static int _instructionPointer = 0;
        private static readonly Stack<int> ReturnPointerStack = new Stack<int>();
        private static readonly Dictionary<string, int> FunctionPointers = new Dictionary<string, int>();
        private static Func<int, int, bool> _comparePredicate = null;
        private static Dictionary<string, Action<string, string>> InstructionActions =>
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
                ["end"] = End
            };

        private static void Compare(string arg1, string arg2)
        {
            if(_comparePredicate == null) return;
            else
            {
                var isInt = int.TryParse(arg2, out var intValue);
                if (!_variableValue.ContainsKey(arg1))
                    _variableValue.Add(arg1, isInt ? intValue : _variableValue[arg2]);
                else _variableValue[arg1] = isInt ? intValue : _variableValue[arg2];
            }
        }
        private static void End(string arg1, string arg2) => _instructionPointer = int.MaxValue - 2;
        private static void Jump(string arg1, string arg2) => _instructionPointer = FunctionPointers[arg1];
        private static void Return(string arg1, string arg2) => _instructionPointer = ReturnPointerStack.Pop(); 
        private static void Call(string arg1, string arg2)
        {
            ReturnPointerStack.Push(_instructionPointer);
            _instructionPointer = FunctionPointers[arg1];
        }
        private static void Message(string arg1, string arg2)
            => _output = arg1 + string.Join(" ", arg2.Split(' ').Select(item =>
                _variableValue.ContainsKey(item) ? _variableValue[item].ToString() : item).Where(item => !string.IsNullOrEmpty(item)));
        private static void Add(string arg1, string arg2)
            => _variableValue[arg1] += _variableValue.ContainsKey(arg2) ? _variableValue[arg2] : int.Parse(arg2);
        private static void Subtract(string arg1, string arg2)
            => _variableValue[arg1] -= _variableValue.ContainsKey(arg2) ? _variableValue[arg2] : int.Parse(arg2);
        private static void Multiply(string arg1, string arg2)
            => _variableValue[arg1] *= _variableValue.ContainsKey(arg2) ? _variableValue[arg2] : int.Parse(arg2);
        private static void Divide(string arg1, string arg2)
            => _variableValue[arg1] /= _variableValue.ContainsKey(arg2) ? _variableValue[arg2] : int.Parse(arg2);
        private static void Decrease(string arg1, string arg2 = null) => _variableValue[arg1]--;
        private static void Increase(string arg1, string arg2 = null) => _variableValue[arg1]++;
        private static void Move(string nameA, string nameB)
        {
            var isInt = int.TryParse(nameB, out var intValue);
            if (!_variableValue.ContainsKey(nameA))
                _variableValue.Add(nameA, isInt ? intValue : _variableValue[nameB]);
            else _variableValue[nameA] = isInt ? intValue : _variableValue[nameB];
        }

        public Dictionary<string, int> Interpret(string program)
        {
            ClearRegistry();
            _output = null;
            var lines = 
                program.Split(Environment.NewLine)
                    .Where(item => !item.StartsWith(";"))
                    .Select(item => !item.Contains(";") ? item : string.Join("", item.TakeWhile(ch => ch != ';')))
                    .Where(item => !string.IsNullOrEmpty(item))
                    .ToImmutableList();
            lines.Where(str => str.EndsWith(':')).ToImmutableList().ForEach(item => FunctionPointers.Add(item.Replace(":",""), lines.IndexOf(item)));
            return Interpret(lines);
        }

        public string GetOutput() => _output;
        
        private static Dictionary<string, int> Interpret(IReadOnlyList<string> program)
        {
            while (_instructionPointer < program.Count)
            {
                ProcessInstruction(program[_instructionPointer]);
                _instructionPointer++;
            }
            var result = _variableValue;
            ClearRegistry();
            return result;
        }

        private static void ProcessInstruction(string input)
        {
            var parts = input
                .Split(input.Contains("msg") ? "'" : " ")
                .Select(item => item.Replace(",", ""))
                .Where(item => !string.IsNullOrEmpty(item))
                .ToImmutableList();
            InstructionActions[parts[0].Replace(" ", "")].Invoke(parts.Count == 1 ? null : parts[1], parts.Count < 3 ? null : parts[2]);
        }
        private static void ClearRegistry()
        {
            _variableValue = new Dictionary<string, int>();
            _instructionPointer = 0;
        }
    }
}