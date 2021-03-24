using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars
{
    public partial class Kata
    {
        public static int[] TheLift(int[][] floorsAndPeoples, int capacity)
        {
            var lift = new Lift(capacity);
            var controlPanel = new ControlPanel(lift, floorsAndPeoples);
            var memory = controlPanel.WorkedUp();
            
            return memory.ToArray().Reverse().ToArray();
        }

        public sealed class Lift
        {
            private List<int> Slots { get; }
            private int MaxCapacity { get; }
            private int FreeSlots => MaxCapacity - Slots.Count;

            public Lift(int maxCapacity)
            {
                Slots = new List<int>(maxCapacity);
                MaxCapacity = maxCapacity;
            }
            
            /// <summary>
            /// Push people into the lift. It looked on free slots in the lift.
            /// Returns the people who continue awaiting on the floor
            /// </summary>
            /// <param name="waitingPeople"></param>
            public IEnumerable<int> PushPeople(IEnumerable<int> waitingPeople)
            {
                var requiredSlots = waitingPeople.Count();
                if (requiredSlots <= FreeSlots)
                {
                    Slots.AddRange(waitingPeople);
                    waitingPeople = Array.Empty<int>();
                }
                else if (FreeSlots != 0)
                {
                    var takeSkipValue = FreeSlots;
                    Slots.AddRange(waitingPeople.Take(takeSkipValue));
                    waitingPeople = waitingPeople.Skip(takeSkipValue);
                }
                return waitingPeople;
            }

            /// <summary>
            /// Is lift should stop on the floor
            /// </summary>
            /// <param name="currentState"></param>
            /// <returns></returns>
            public bool IsStopRequired(int currentState)
            {
                return Slots.Contains(currentState);
            }

            /// <summary>
            /// Pop people from lift to floor
            /// </summary>
            /// <param name="currentState"></param>
            public void PopPeople(int currentState)
            {
                Slots.RemoveAll(item => item == currentState);
            }
        }
        public sealed class ControlPanel
        {
            private readonly Lift _lift;
            private readonly int[][] _floorsWithQueues;
            private readonly int _maxFloor;
            private readonly int _minFloor;
            private Stack<int> VisitsMemory { get; }
            private int CurrentState { get; set; }
            private int StateChanger { get; set; }
            private Func<int, bool> IsLiftAwaitedFunc { get; set; }
            private Func<bool> IsMovingShouldContinueFunc { get; set; }

            public ControlPanel(Lift lift, int[][] floorsWithQueues)
            {
                _lift = lift;
                _floorsWithQueues = floorsWithQueues;
                _maxFloor = floorsWithQueues.Length - 1;
                VisitsMemory = new Stack<int>();
                _minFloor = 0;
                CurrentState = _minFloor;
                VisitsMemory.Push(CurrentState);
            }

            /// <summary>
            /// Lift working process
            /// </summary>
            /// <returns></returns>
            public Stack<int> WorkedUp()
            {
                while (_floorsWithQueues.Any(item => item.Any()))
                {
                    if (CurrentState == _minFloor)
                    {
                        SetMovingUpRules();
                    }
                    if (CurrentState == _maxFloor)
                    {
                        SetMovingDownRules();
                    }
                    MoveTheLift();
                }

                MoveLiftOnGround();
                return VisitsMemory;
            }

            private void MoveLiftOnGround()
            {
                if (VisitsMemory.Peek() == _minFloor) return;
                
                VisitsMemory.Push(_minFloor);
                CurrentState = _minFloor;
            }

            /// <summary>
            /// Lift movement logic
            /// </summary>
            private void MoveTheLift()
            {
                while (IsMovingShouldContinueFunc())
                {
                    ref var floorQueue = ref _floorsWithQueues[CurrentState];
                    if (_lift.IsStopRequired(CurrentState))
                    {
                        VisitsMemory.Push(CurrentState);
                        _lift.PopPeople(CurrentState);
                    }
                    if (IsSomebodyAwaitTheLift(floorQueue))
                    {
                        if(VisitsMemory.Peek() != CurrentState)
                            VisitsMemory.Push(CurrentState);
                    
                        var notMigratedPeople = _lift.PushPeople(GetPotentialPassengers(floorQueue)).ToList();
                        notMigratedPeople.AddRange(GetNextDoorGuys(floorQueue));
                        floorQueue = notMigratedPeople.ToArray();
                    }
                    CurrentState += StateChanger;
                }
                CurrentState -= StateChanger;
            }
            
            private bool IsSomebodyAwaitTheLift(IEnumerable<int> floorQueue)
            {
                return floorQueue.Any(IsLiftAwaitedFunc);
            }
            
            private IEnumerable<int> GetPotentialPassengers(IEnumerable<int> floor)
            {
                return floor.Where(IsLiftAwaitedFunc);
            }

            private IEnumerable<int> GetNextDoorGuys(IEnumerable<int> floor)
            {
                return floor.Where(item => !IsLiftAwaitedFunc(item));
            }

            private void SetMovingUpRules()
            {
                IsLiftAwaitedFunc = item => item > CurrentState;
                IsMovingShouldContinueFunc = () => CurrentState <= _maxFloor;
                StateChanger = 1;
            }
            
            private void SetMovingDownRules()
            {
                IsLiftAwaitedFunc = item => item < CurrentState;
                IsMovingShouldContinueFunc = () => CurrentState >= _minFloor;
                StateChanger = -1;
            }
        }
    }
}