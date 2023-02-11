using System;

namespace UnitsSystem.States
{
    public class Transition
    {
        public Func<bool> Condition { get; }
        
        public IState ToState { get; }

        public Transition(IState toState, Func<bool> predicateFunc)
        {
            ToState = toState;
            Condition = predicateFunc;
        }
    }
}