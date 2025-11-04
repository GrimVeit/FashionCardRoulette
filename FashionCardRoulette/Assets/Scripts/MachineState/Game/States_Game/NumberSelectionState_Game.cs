using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSelectionState_Game : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly IRouletteStateProvider _rouletteStateProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly INumberSelectionActivatorProvider _numberSelectionActivatorProvider;

    public NumberSelectionState_Game(IGlobalStateMachineProvider globalStateMachineProvider, IRouletteStateProvider rouletteStateProvider, UIGameRoot sceneRoot, INumberSelectionActivatorProvider numberSelectionActivatorProvider)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _rouletteStateProvider = rouletteStateProvider;
        _sceneRoot = sceneRoot;
        _numberSelectionActivatorProvider = numberSelectionActivatorProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - NUMBER SELECTION STATE / GAME</color>");

        _sceneRoot.OnClickToContinue_ChooseNumbers += ChangeStateToSectorNumbers;
        _sceneRoot.OnClickToBack_ChooseNumbers += ChangeStateToMain;

        _rouletteStateProvider.SetGame_Smooth();

        _sceneRoot.OpenNumbersSelectionPanel();
        _sceneRoot.OpenChooseNumbersPanel();
        _numberSelectionActivatorProvider.Activate();
        _numberSelectionActivatorProvider.ChooseRandom();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToContinue_ChooseNumbers -= ChangeStateToSectorNumbers;
        _sceneRoot.OnClickToBack_ChooseNumbers -= ChangeStateToMain;

        _numberSelectionActivatorProvider.Deactivate();

        _sceneRoot.CloseChooseNumbersPanel();
    }

    private void ChangeStateToSectorNumbers()
    {
        _sceneRoot.CloseRoulettePanel();

        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<SectorsNumbersState_Game>());
    }

    private void ChangeStateToMain()
    {
        _rouletteStateProvider.SetIdle_Smooth();
        _sceneRoot.CloseNumbersSelectionPanel();

        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<MainState_Game>());
    }
}
