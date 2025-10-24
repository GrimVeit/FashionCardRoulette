using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly NumberValues _numberValues;
    private readonly IChooseNumberEventsProvider _chooseNumberEventsProvider;
    private readonly IChooseNumberProvider _chooseNumberProvider;

    public MainState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, NumberValues numberValues, IChooseNumberEventsProvider chooseNumberEventsProvider, IChooseNumberProvider chooseNumberProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _numberValues = numberValues;
        _chooseNumberEventsProvider = chooseNumberEventsProvider;
        _chooseNumberProvider = chooseNumberProvider;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToCharacter_Main += ChangeStateToShopWardrobe;
        _sceneRoot.OnClickToSpin_Main += TEST;
        _chooseNumberEventsProvider.OnSetNumber += ChangeStateToSetNumber;

        _sceneRoot.OpenMainPanel();
        _sceneRoot.OpenCoinsPanel();
        _sceneRoot.OpenExitPanel();
        _sceneRoot.OpenTasksPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToCharacter_Main -= ChangeStateToShopWardrobe;
        _sceneRoot.OnClickToSpin_Main -= TEST;
        _chooseNumberEventsProvider.OnSetNumber -= ChangeStateToSetNumber;

        _sceneRoot.CloseMainPanel();
        _sceneRoot.CloseExitPanel();
    }

    private void TEST()
    {
        _chooseNumberProvider.SetNumber(_numberValues.GetRandomNumberValue());
    }

    private void ChangeStateToSetNumber()
    {
        _machineProvider.SetState(_machineProvider.GetState<SetNumberState_Game>());
    }

    private void ChangeStateToShopWardrobe()
    {
        _sceneRoot.CloseTasksPanel();

        _machineProvider.SetState(_machineProvider.GetState<ShopWardrobeState_Game>());
    }
}
