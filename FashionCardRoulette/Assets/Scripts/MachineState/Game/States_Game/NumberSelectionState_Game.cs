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
        _rouletteStateProvider.SetGame();

        _sceneRoot.OpenNumbersSelectionPanel();
        _numberSelectionActivatorProvider.Activate();
    }

    public void ExitState()
    {
        _numberSelectionActivatorProvider.Deactivate();
    }
}
