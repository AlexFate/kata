using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CodeWars;

public partial class Kata
{
    public static string AssemblyAdvancedInterpret(string input) => new AssemblyAdvanced().Execute(input);
}

public class AssemblyAdvanced
{
    private readonly ApplicationMemory _appMemory;
    private InstructionProcessor Processor { get; }
    public AssemblyAdvanced()
    {
        _appMemory = new ApplicationMemory();
        Processor = new InstructionProcessor(ref _appMemory);
    }
    public string Execute(string program)
    {
        var lines = PrepareProgram(program);
        Interpret(lines);
        return _appMemory.Output;
    }
    private void Interpret(IReadOnlyList<string> program)
    {
        while (!Processor.Completed)
            Processor.Process(pointer => pointer < program.Count ? program[pointer] : null);
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
            .ForEach(item => _appMemory.FillFunctionPointer(dic => dic.Add(item.Key, item.Value)));
    }
}
public sealed class Comparer
{
    public static readonly Func<int, int, bool> EqualPredicate = (a, b) => a == b;
    public static readonly Func<int, int, bool> NotEqualPredicate = (a, b) => !EqualPredicate(a, b);
    public static readonly Func<int, int, bool> GreaterPredicate = (a, b) => a > b;
    public static readonly Func<int, int, bool> LessPredicate = (a, b) => a < b;
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
    private IImmutableList<string> Args { get; }
    public Arguments(IImmutableList<string> list) => Args = list;
    public Arguments(params string[] param) => Args = param.ToImmutableList();
    public string First => Args[0];
    public string Second => Args.Count == 2 ? Args[1] : "";
    public string this[int index] => Args[index];
    public ImmutableList<string> ToImmutableList() => Args.ToImmutableList();
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
        get => _variableValue.ContainsKey(key) ? _variableValue[key] : int.Parse(key);
        set
        {
            if (_variableValue.ContainsKey(key))
                _variableValue[key] = value;
            else
                _variableValue.Add(key, value);
        }
    }
    public string InjectValues(string input)
    {
        foreach (var (key, value) in _variableValue) 
            input = input.Replace(key, value.ToString());
        return input;
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
    public void PushReturnPointer(int pointer) => _returnPointerStack.Push(pointer);
}
public class InstructionProcessor
{
    private readonly ApplicationMemory _appMemory;
    private int _instructionPointer;
    private Dictionary<string, Action<Arguments>> InstructionActions =>
        new Dictionary<string, Action<Arguments>>
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
    public InstructionProcessor(ref ApplicationMemory appMemory) => _appMemory = appMemory;
    public void Process(Func<int, string> getLine)
    {
        var input = getLine(_instructionPointer++);
        if (IsProgramBroken(input))
        {
            Completed = true;
            _appMemory.Output = null;
            return;
        }
        if (NeedSpecialMsgArgumentParser(input))
        {
            input = SkipStartEmpties(input.Replace("msg", ""));
            InstructionActions["msg"](new Arguments(input.Split("',")));
            return;
        }
            
        var parts = SplitWhiteSpaceAndRemoveComma(input);
        try
        {
            InstructionActions[parts[0]](new Arguments(parts.Skip(1).ToImmutableList()));
        }
        catch
        {
            _instructionPointer++;
        }
    }
    private static string SkipStartEmpties(string item) =>
        string.Join("", item.SkipWhile(ch => ch == ' '));
    private static bool IsProgramBroken(string input) => string.IsNullOrEmpty(input);
    private static bool NeedSpecialMsgArgumentParser(string input) => input.Contains("msg");
    private static ImmutableList<string> SplitWhiteSpaceAndRemoveComma(string input) => input
        .Split(" ")
        .Select(item => item.Replace(",", ""))
        .Where(item => !string.IsNullOrEmpty(item))
        .ToImmutableList();
       
    #region Actions
    private void JumpLess(Arguments arguments)
    {
        if (_appMemory.InvokeComparison(Comparer.LessPredicate)) Jump(arguments);
    }
    private void JumpLessOrEqual(Arguments arguments)
    {
        if (_appMemory.InvokeComparison(Comparer.LessOrEqualPredicate)) Jump(arguments);
    }
    private void JumpGreater(Arguments arguments)
    {
        if (_appMemory.InvokeComparison(Comparer.GreaterPredicate)) Jump(arguments);
    }
    private void JumpGreaterOrEqual(Arguments arguments)
    {
        if (_appMemory.InvokeComparison(Comparer.GreaterOrEqualPredicate)) Jump(arguments);
    }
    private void JumpEqual(Arguments arguments)
    {
        if (_appMemory.InvokeComparison(Comparer.EqualPredicate)) Jump(arguments);
    }
    private void JumpNotEqual(Arguments arguments)
    {
        if(_appMemory.InvokeComparison(Comparer.NotEqualPredicate)) Jump(arguments);
    }
    private void Compare(Arguments arguments)
    {
        _appMemory.AddComparedValues(arguments.First, arguments.Second);
    }
    private void End(Arguments arguments = null) => Completed = true;
    private void Jump(Arguments arguments) => _instructionPointer = _appMemory.GetPointerOnLabel(arguments.First);
    private void Return(Arguments arguments = null)
    {
        _instructionPointer = _appMemory.GetReturnPointer();
    }

    private void Call(Arguments arguments)
    {
        _appMemory.PushReturnPointer(_instructionPointer);
        Jump(arguments);
    }
    private void Message(Arguments arguments)
    {
        var cleanedArgs = arguments.ToImmutableList()
            .Select(
                item => _appMemory.InjectValues(string.Join("", item.TakeWhile(ch => ch != "'".First())))
                            .Replace(" ", "")
                            .Replace(",", "") +
                        string.Join("", item.SkipWhile(ch => ch != "'".First())).Replace("'", ""))
            .Select(item => string.IsNullOrEmpty(item) ? ", " : item);
        _appMemory.Output += string.Join("", cleanedArgs.ToList());
    }
    private void Add(Arguments arguments)
    {
        var isInt = int.TryParse(arguments.Second, out var intValue);
        _appMemory[arguments.First] += isInt
            ? intValue
            : _appMemory[arguments.Second];
    }

    private void Subtract(Arguments arguments)
    {
        var isInt = int.TryParse(arguments.Second, out var intValue);
        _appMemory[arguments.First] -= isInt
            ? intValue
            : _appMemory[arguments.Second];
    }
    private void Multiply(Arguments arguments)
    {
        var isInt = int.TryParse(arguments.Second, out var intValue);
        _appMemory[arguments.First] *= isInt
            ? intValue
            : _appMemory[arguments.Second];
    }
    private void Divide(Arguments arguments)
    {
        var isInt = int.TryParse(arguments.Second, out var intValue);
        _appMemory[arguments.First] /= isInt
            ? intValue
            : _appMemory[arguments.Second];
    }
    private void Decrease(Arguments arguments) => _appMemory[arguments.First]--;
    private void Increase(Arguments arguments) => _appMemory[arguments.First]++;
    private void Move(Arguments arguments)
    {
        var isInt = int.TryParse(arguments.Second, out var intValue);
        _appMemory[arguments.First] = isInt ? intValue : _appMemory[arguments.Second];
    }
    #endregion
}