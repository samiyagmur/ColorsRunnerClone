using Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public  IState _currentState;

    private List<Transition> _currentTransition = new List<Transition>();

    private List<Transition> _anyTransition = new List<Transition>();

    private static List<Transition> EmtyTranstion=new List<Transition>();

    private Dictionary<Type, List<Transition>> _transition= new Dictionary<Type, List<Transition>>();

    public void Setup()
    {
        var transition = GetTransition();

        if (transition != null)
        {
            SetState(transition.To);

        }
        _currentState.OnSetup();
    }

    public void SetState(IState state)
    {
        if (state == _currentState)
            return;
        _currentState?.OnExit();
        _currentState = state;
        _transition.TryGetValue(_currentState.GetType(), out _currentTransition);
        if (_currentTransition==null)
        {
            _currentTransition = EmtyTranstion;
        }
        _currentState.OnEnter();
    }

    public void AddTransition(IState from,IState to,Func<bool> predicate)
    {
        if (!_transition.TryGetValue(from.GetType(),out var transitions))
        {
            transitions = new List<Transition>();
            _transition[from.GetType()]=transitions;
        }

        transitions.Add(new Transition(predicate,to));

    }

    public void AddAnyTransition(IState state,Func<bool>predicate)
    {
        _anyTransition.Add(new Transition(predicate, state));
    }

    private Transition GetTransition()
    {
        foreach (var transition in _anyTransition)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }

        foreach (var transition in _currentTransition)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }
        return null;
    }

    private class Transition 
    {
        public Func<bool> Condition { get; }

        public IState To { get; }

        public Transition(Func<bool> condition, IState to)
        {
            Condition = condition;
            To = to;
        }


    }
}

