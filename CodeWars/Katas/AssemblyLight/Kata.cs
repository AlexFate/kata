using System;
using System.Collections.Generic;

namespace CodeWars
{
    public partial class Kata
    {
        private static Dictionary<string, int> _variableValue = new Dictionary<string, int>();

        private static Dictionary<string, Action<string, string>> InstructionActions =>
            new Dictionary<string, Action<string, string>>()
            {
                ["mov"] = Move,
                ["inc"] = Increase,
                ["dec"] = Decrease,
                ["jnz"] = Jump,
            };

        private static int _instructionPointer = 0;

        private static void Jump(string arg1, string arg2)
        {
            var jumpValue = Convert.ToInt32(arg2);
            if(_variableValue.ContainsKey(arg1) && _variableValue[arg1] == 0) return;
            
            _instructionPointer--;
            _instructionPointer += jumpValue;
        }

        private static void Decrease(string arg1, string arg2 = null) => _variableValue[arg1]--;

        private static void Increase(string arg1, string arg2 = null) => _variableValue[arg1]++;

        private static void Move(string nameA, string nameB)
        {
            var isInt = int.TryParse(nameB, out var intValue);
            if (!_variableValue.ContainsKey(nameA))
            {
                _variableValue.Add(nameA, isInt ? intValue : _variableValue[nameB]);
            }
            else _variableValue[nameA] = isInt ? intValue : _variableValue[nameB];
        }
        
        public static Dictionary<string, int> Interpret(string[] program)
        {
            while (_instructionPointer < program.Length)
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
            var parts = input.Split(" ");
            InstructionActions[parts[0]].Invoke(parts[1], parts.Length < 3 ? null : parts[2]);
        }

        private static void ClearRegistry()
        {
            _variableValue = new Dictionary<string, int>();
            _instructionPointer = 0;
        }
    }
}