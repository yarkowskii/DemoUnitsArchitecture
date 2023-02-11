using System;
using System.Collections.Generic;

namespace UnitsSystem.States
{
    public class StatesMachine
    {
        private IState _initState;
        private IState _currentState;

        private Dictionary<Type, List<Transition>> _transitions = new();
        private List<Transition> _currentTransitions = new();
        private List<Transition> _anyTransitions = new();
        
        
        public void Tick()
        {
            var transition = GetTransition();
            if(transition != null)
                SetState(transition.ToState);

            _currentState?.Tick();
        }

        public void SetInitState(IState state) => _initState = state;
        public void RunFromInitState() => SetState(_initState);


        private void SetState(IState newState)
        {
            if (newState == _currentState)
                return;
            
            _currentState?.OnExit();
            _currentState = newState;

            // Update current transitions for new state
            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            
            _currentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicateFunc)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }
            
            transitions.Add(new Transition(to, predicateFunc));
        }

        public void AddAnyTransition(IState state, Func<bool> predicateFunc) => _anyTransitions.Add(new Transition(state, predicateFunc));

        private Transition GetTransition()
        {
            foreach (var anyTransition in _anyTransitions)
            {
                if (anyTransition.Condition())
                    return anyTransition;
            }
            
            
            foreach (var currentTransition in _currentTransitions)
            {
                if (currentTransition.Condition())
                    return currentTransition;
            }

            return null;
        }
    }
}