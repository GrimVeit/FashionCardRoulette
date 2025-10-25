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
        IStoreClothesEventsProvider storeClothesEventsProvider,
        IShopClothesEventsProvider shopClothesEventsProvider,
        IWardrobeClothesEventsProvider wardrobeClothesEventsProvider, 
        NumberValues numberValues,
        IChooseNumberEventsProvider chooseNumberEventsProvider,
        IChooseNumberProvider chooseNumberProvider,
        ITaskVisualEventsProvider taskVisualEventsProvider,
        ITaskVisualProvider taskVisualProvider)
    {
        states[typeof(ChooseGenderState_Game)] = new ChooseGenderState_Game(this, sceneRoot);
        states[typeof(ChooseCharacterState_Game)] = new ChooseCharacterState_Game(this, sceneRoot);

        states[typeof(MainState_Game)] = new MainState_Game(this, sceneRoot, numberValues, chooseNumberEventsProvider, chooseNumberProvider, taskVisualEventsProvider);
        states[typeof(SetNumberState_Game)] = new SetNumberState_Game(this, sceneRoot, taskVisualEventsProvider, taskVisualProvider);
        states[typeof(TaskDescriptionState_Game)] = new TaskDescriptionState_Game(this, sceneRoot);





        states[typeof(ShopWardrobeState_Game)] = new ShopWardrobeState_Game(this, sceneRoot);

        states[typeof(ShopTypeState_Game)] = new ShopTypeState_Game(this, sceneRoot, storeClothesEventsProvider);
        states[typeof(ShopState_Game)] = new ShopState_Game(this, sceneRoot, shopClothesEventsProvider);
        states[typeof(NotCoinsState_Game)] = new NotCoinsState_Game(this, sceneRoot);
        states[typeof(PaycheckState_Game)] = new PaycheckState_Game(this, sceneRoot, shopClothesEventsProvider);

        states[typeof(WardrobeTypeState_Game)] = new WardrobeTypeState_Game(this, sceneRoot, storeClothesEventsProvider);
        states[typeof(WardrobeState_Game)] = new WardrobeState_Game(this, sceneRoot, wardrobeClothesEventsProvider);
        states[typeof(WardrobeFitClothesState_Game)] = new WardrobeFitClothesState_Game(this, sceneRoot);
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
