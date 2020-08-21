using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars
{
    public partial class Kata
    {
        private static Dictionary<string, int> _variableValue = new Dictionary<string, int>();

        private static Dictionary<string, Action<object, object>> InstructionActions =>
            new Dictionary<string, Action<object, object>>()
            {
                ["mov"] = Move,
                ["inc"] = Increase,
                ["dec"] = Decrease,
                ["jnz"] = Jump,
            };

        private static int _instructionPointer = 0;

        private static void Jump(object arg1, object arg2)
        {
            var jumpValue = Convert.ToInt32(arg2);
            var varName = arg1 as string;
            if(_variableValue.ContainsKey(varName))
                if (_variableValue[varName] == 0) return;
            _instructionPointer--;
            _instructionPointer += jumpValue;
        }

        private static void Decrease(object arg1, object arg2 = null)
        {
            var varName = arg1 as string;
            _variableValue[varName]--;
        }

        private static void Increase(object arg1, object arg2 = null)
        {
            var key = Convert.ToString(arg1);
            _variableValue[key]++;
        }

        private static void Move(object nameA, object nameB)
        {
            var key = Convert.ToString(nameA);
            try
            {
                int insert = Convert.ToInt32(nameB);
                _variableValue.Add(key, insert);
            }
            catch
            {
                string secondKey = Convert.ToString(nameB);
                if (!_variableValue.ContainsKey(key))
                {
                    _variableValue.Add(key, _variableValue[secondKey]);
                }
                else _variableValue[key] = _variableValue[secondKey];
            }
        }
        
        public static Dictionary<string, int> Interpret(string[] program)
        {
            while (_instructionPointer < program.Count())
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