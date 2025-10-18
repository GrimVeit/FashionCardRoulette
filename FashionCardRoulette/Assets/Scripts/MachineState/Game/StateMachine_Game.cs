using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Game : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Game
        (UIGameRoot sceneRoot,
        IStoreClothesEventsProvider storeClothesEventsProvider)
    {
        states[typeof(ChooseGenderState_Game)] = new ChooseGenderState_Game(this, sceneRoot);
        states[typeof(ChooseCharacterState_Game)] = new ChooseCharacterState_Game(this, sceneRoot);

        states[typeof(MainState_Game)] = new MainState_Game(this, sceneRoot);

        states[typeof(ShopWardrobeState_Game)] = new ShopWardrobeState_Game(this, sceneRoot);

        states[typeof(ShopTypeState_Game)] = new ShopTypeState_Game(this, sceneRoot, storeClothesEventsProvider);
        states[typeof(ShopState_Game)] = new ShopState_Game(this, sceneRoot);

        states[typeof(WardrobeTypeState_Game)] = new WardrobeTypeState_Game(this, sceneRoot, storeClothesEventsProvider);
        states[typeof(WardrobeState_Game)] = new WardrobeState_Game(this, sceneRoot);
    }

    public void Initialize()
    {
        SetState(GetState<ChooseGenderState_Game>());
    }

    public void Dispose()
    {

    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }

    public void SetState(IState state)
    {
        _currentState?.ExitState();

        _currentState = state;
        _currentState.EnterState();
    }
}
