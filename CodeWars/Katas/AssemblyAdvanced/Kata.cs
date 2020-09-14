using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CodeWars
{
    public partial class Kata
    {
        public static string AssemblyAdvancedInterpret(string input) => new AssemblyAdvanced().Execute(input);
    }

    public class AssemblyAdvanced
    {
        private readonly ApplicationMemory _applicationMemory;
        private InstructionProcessor Processor { get; }
        public AssemblyAdvanced()
        {
            _applicationMemory = new ApplicationMemory();
            Processor = new InstructionProcessor(ref _applicationMemory);
        }
        public string Execute(string program)
        {
            var lines = PrepareProgram(program);
            Interpret(lines);
            return _applicationMemory.Output;
        }
        private void Interpret(IReadOnlyList<string> program)
        {
            while (!Processor.Completed)
            {
                Processor.Process(pointer => pointer < program.Count ? program[pointer] : null);
            }
        }
        private IReadOnlyList<string> PrepareProgram(string program)
        {
            var programLines = GetProgramLinesWithRemovedComments(program);
            MarkLabel(programLines.ToImmutableList());
            return programLines;
        }
        private static IReadOnlyList<string> GetProgramLinesWithRemovedComments(string program)
        {
            return program.Split(Environment.NewLine)
                .Where(item => !item.StartsWith(";"))
                .Select(item => !item.Contains(";") ? item : string.Join("", item.TakeWhile(ch => ch != ';')))
                .Where(item => !string.IsNullOrEmpty(item))
                .ToImmutableList();
        }
        private void MarkLabel(IImmutableList<string> programLines)
        {
            programLines.Where(str => str.EndsWith(':'))
                .Select(item => new KeyValuePair<string, int>(item.Replace(":",""), programLines.IndexOf(item) + 1))
                .ToImmutableList()
                .ForEach(item => _applicationMemory.FillFunctionPointer(dic => dic.Add(item.Key, item.Value)));
        }
    }
    public sealed class Comparer
    {
        public static readonly Func<int, int, bool> EqualPredicate = (a, b) => a == b;
        public static readonly Func<int, int, bool> NotEqualPredicate = (a, b) => !EqualPredicate(a, b);
        public static readonly Func<int, int, bool> GreaterPredicate = (a, b) => a > b;
        public static readonly Func<int, int, bool> LessPredicate = (a, b) => !GreaterPredicate(a, b);
        public static readonly Func<int, int, bool> LessOrEqualPredicate = (a, b) => LessPredicate(a, b) || EqualPredicate(a, b);
        public static readonly Func<int, int, bool> GreaterOrEqualPredicate = (a, b) => GreaterPredicate(a, b) || EqualPredicate(a, b);
        
        private int A { get; }
        private int B { get; }
        public Comparer(int a, int b)
        {
            A = a;
            B = b;
        }
        public bool InvokeComparison(Func<int, int, bool> predicate) => predicate.Invoke(A, B);
    }

    public sealed class Arguments
    {
        private List<string> args => new List<string>();
        public string First => args[0];
        public string Second => args.Count == 2 ? args[1] : "";
        public int Count => args.Count;
        public string this[int index]
        {
            get => args[index];
            set => args.Add(value);
        }
    }
    public class ApplicationMemory
    {
        private readonly Dictionary<string, int> _variableValue = new Dictionary<string, int>();
        private readonly Stack<int> _returnPointerStack = new Stack<int>();
        private readonly Dictionary<string, int> _functionPointers = new Dictionary<string, int>();
        private Comparer Comparer { get; set; }
        public string Output { get; set; }
        public int this[string key]
        {
            get => _variableValue[key];
            set
            {
                if (_variableValue.ContainsKey(key))
                    _variableValue[key] = value;
                else
                    _variableValue.Add(key, value);
            }
        }
        public void FillFunctionPointer(Action<Dictionary<string, int>> fill) => fill(_functionPointers);
        public void AddComparedValues(string arg1, string arg2)
        {
            Comparer = new Comparer(_variableValue[arg1],
                int.TryParse(arg2, out var intValue) ? intValue : _variableValue[arg2]);
        }
        public bool InvokeComparison(Func<int, int, bool> predicate) => Comparer.InvokeComparison(predicate);
        public int GetPointerOnLabel(string label) => _functionPointers[label];
        public int GetReturnPointer() => _returnPointerStack.Pop();
        public void AddReturnPointer(int pointer) => _returnPointerStack.Push(pointer);
    }
    public class InstructionProcessor
    {
        private readonly ApplicationMemory _appMemory;
        private int _instructionPointer;
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
        public bool Completed { get; private set; }
        public InstructionProcessor(ref ApplicationMemory appMemory)
        {
            _appMemory = appMemory;
        }
        public void Process(Func<int, string> getLine)
        {
            var input = getLine(_instructionPointer++);
            if (string.IsNullOrEmpty(input))
            {
                Completed = true;
                return;
            }
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
            InstructionActions[parts[0]](parts.Count == 1 ? null : parts[1], parts.Count < 3 ? null : parts[2]);
        }
        private static string SkipStartEmpties(string item) =>
            string.Join("", item.SkipWhile(ch => ch == ' '));
        #region Actions

                private void JumpLess(string arg1, string arg2)
        {
            if (_appMemory.InvokeComparison(Comparer.LessPredicate)) Jump(arg1, null);
        }
        private void JumpLessOrEqual(string arg1, string arg2)
        {
            if (_appMemory.InvokeComparison(Comparer.LessOrEqualPredicate)) Jump(arg1, null);
        }
        private void JumpGreater(string arg1, string arg2)
        {
            if (_appMemory.InvokeComparison(Comparer.GreaterPredicate)) Jump(arg1, null);
        }
        private void JumpGreaterOrEqual(string arg1, string arg2)
        {
            if (_appMemory.InvokeComparison(Comparer.GreaterOrEqualPredicate)) Jump(arg1, null);
        }
        private void JumpEqual(string arg1, string arg2)
        {
            if (_appMemory.InvokeComparison(Comparer.EqualPredicate)) Jump(arg1, null);
        }
        private void JumpNotEqual(string arg1, string arg2)
        {
            if(_appMemory.InvokeComparison(Comparer.NotEqualPredicate)) Jump(arg1, null);
        }
        private void Compare(string arg1, string arg2)
        {
            _appMemory.AddComparedValues(arg1, arg2);
        }
        private void End(string arg1, string arg2) => Completed = true;
        private void Jump(string arg1, string arg2) => _instructionPointer = _appMemory.GetPointerOnLabel(arg1);
        private void Return(string arg1, string arg2) => _instructionPointer = _appMemory.GetReturnPointer(); 
        private void Call(string arg1, string arg2)
        {
            _appMemory.AddReturnPointer(_instructionPointer);
            _instructionPointer = _appMemory.GetPointerOnLabel(arg1);
        }
        private void Message(string arg1, string arg2)
        {
            // TODO: Something should be here
            //_output += string.Join("", outputArgs);
        }
        private void Add(string arg1, string arg2)
        {
            var isInt = int.TryParse(arg2, out var intValue);
            _appMemory[arg1] += isInt
                ? intValue
                : _appMemory[arg2];
        }

        private void Subtract(string arg1, string arg2)
        {
            var isInt = int.TryParse(arg2, out var intValue);
            _appMemory[arg1] -= isInt
                ? intValue
                : _appMemory[arg2];;
        }
        private void Multiply(string arg1, string arg2)
        {
            var isInt = int.TryParse(arg2, out var intValue);
            _appMemory[arg1] *= isInt
                ? intValue
                : _appMemory[arg2];
        }
        private void Divide(string arg1, string arg2)
        {
            var isInt = int.TryParse(arg2, out var intValue);
            _appMemory[arg1] /= isInt
                ? intValue
                : _appMemory[arg2];
        }
        private void Decrease(string arg1, string arg2 = null) => _appMemory[arg1]--;
        private void Increase(string arg1, string arg2 = null) => _appMemory[arg1]++;
        private void Move(string nameA, string nameB)
        {
            var isInt = int.TryParse(nameB, out var intValue);
            _appMemory[nameA] = isInt ? intValue : _appMemory[nameB];
        }

        #endregion
    }
}