using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorsNumbersState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly ISectorArrowProvider _sectorArrowProvider;
    private readonly ISectorArrowEventsProvider _sectorArrowEventsProvider;

    public SectorsNumbersState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, ISectorArrowProvider sectorArrowProvider, ISectorArrowEventsProvider sectorArrowEventsProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _sectorArrowProvider = sectorArrowProvider;
        _sectorArrowEventsProvider = sectorArrowEventsProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - SECTORS NUMBERS STATE / GAME</color>");

        _sectorArrowEventsProvider.OnDeactivateZone += ChangeStateToSectorsFinish;

        _sectorArrowProvider.ActivateZone();

        _sceneRoot.OpenSectorsPanel();
    }

    public void ExitState()
    {
        _sectorArrowEventsProvider.OnDeactivateZone -= ChangeStateToSectorsFinish;
    }

    private void ChangeStateToSectorsFinish()
    {
        _machineProvider.SetState(_machineProvider.GetState<SectorsNumbersFinishState_Game>());
    }
}
