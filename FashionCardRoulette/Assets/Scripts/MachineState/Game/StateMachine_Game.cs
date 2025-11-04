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
        ITaskVisualProvider taskVisualProvider,
        IClaimEventsProvider claimEventsProvider,
        IVideoProvider videoProvider,
        RoulettePresenter roulettePresenter,
        RouletteBallPresenter rouletteBallPresenter,
        IRouletteStateProvider rouletteStateProvider,
        INumberTrashEventsProvider numberTrashEventsProvider,
        IRouletteSpinCountProvider rouletteSpinCountProvider,
        ITaskVisualMoveProvider taskVisualMoveProvider,
        ITaskVisualInfoProvider taskVisualInfoProvider,
        INumberSelectionActivatorProvider numberSelectionActivatorProvider,
        ISectorArrowProvider sectorArrowProvider,
        ISectorArrowEventsProvider sectorArrowEventsProvider,
        IStoreNumberInfoProvider storeNumberInfoProvider)
    {
        states[typeof(ChooseGenderState_Game)] = new ChooseGenderState_Game(this, sceneRoot);
        states[typeof(ChooseCharacterState_Game)] = new ChooseCharacterState_Game(this, sceneRoot);

        states[typeof(MainState_Game)] = new MainState_Game(this, sceneRoot, taskVisualEventsProvider, taskVisualProvider);
        states[typeof(NumberSelectionState_Game)] = new NumberSelectionState_Game(this, rouletteStateProvider, sceneRoot, numberSelectionActivatorProvider);
        states[typeof(SectorsNumbersState_Game)] = new SectorsNumbersState_Game(this, sceneRoot, sectorArrowProvider, sectorArrowEventsProvider);
        states[typeof(SectorsNumbersFinishState_Game)] = new SectorsNumbersFinishState_Game(this, sceneRoot);

        states[typeof(CheckFinishState_Game)] = new CheckFinishState_Game(this, taskVisualInfoProvider);
        states[typeof(ResultState_Game)] = new ResultState_Game(this, sceneRoot, taskVisualMoveProvider);
        states[typeof(RouletteState_Game)] = new RouletteState_Game(this, roulettePresenter, rouletteBallPresenter, chooseNumberProvider, sceneRoot, rouletteStateProvider, rouletteSpinCountProvider, storeNumberInfoProvider);
        states[typeof(SetNumberState_Game)] = new SetNumberState_Game(this, sceneRoot, taskVisualEventsProvider, taskVisualProvider, rouletteStateProvider, numberTrashEventsProvider);
        states[typeof(TaskDescriptionState_Game)] = new TaskDescriptionState_Game(this, sceneRoot, claimEventsProvider);
        states[typeof(FromTaskDescriptionToMoreCoinsState_Game)] = new FromTaskDescriptionToMoreCoinsState_Game(this, sceneRoot);
        states[typeof(MoreCoinsState_Game)] = new MoreCoinsState_Game(this, sceneRoot, videoProvider);





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
