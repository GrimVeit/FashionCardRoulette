using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorsNumbersState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly ISectorArrowProvider _sectorArrowProvider;

    public SectorsNumbersState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, ISectorArrowProvider sectorArrowProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _sectorArrowProvider = sectorArrowProvider;
    }

    public void EnterState()
    {
        _sectorArrowProvider.ActivateMove();

        _sceneRoot.OpenSectorsPanel();
    }

    public void ExitState()
    {
        _sceneRoot.CloseSectorsPanel();
        _sceneRoot.CloseNumbersSelectionPanel();
    }
}
