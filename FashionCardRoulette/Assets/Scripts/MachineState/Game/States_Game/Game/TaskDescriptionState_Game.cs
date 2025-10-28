using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDescriptionState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IClaimEventsProvider _claimEventsProvider;

    public TaskDescriptionState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IClaimEventsProvider claimEventsProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _claimEventsProvider = claimEventsProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TASK DESCRIPTION STATE / GAME</color>");

        _claimEventsProvider.OnClaimTask += ChangeStateToFromTaskDescriptionToMoreCoins;
        _sceneRoot.OnClickToBack_TaskDescription += ChangeStateToMain;

        _sceneRoot.OpenTaskDescriptionPanel();
    }

    public void ExitState()
    {
        _claimEventsProvider.OnClaimTask -= ChangeStateToFromTaskDescriptionToMoreCoins;
        _sceneRoot.OnClickToBack_TaskDescription -= ChangeStateToMain;
    }

    private void ChangeStateToMain()
    {
        _sceneRoot.CloseTaskDescriptionPanel();

        _machineProvider.SetState(_machineProvider.GetState<MainState_Game>());
    }

    private void ChangeStateToFromTaskDescriptionToMoreCoins()
    {
        _machineProvider.SetState(_machineProvider.GetState<FromTaskDescriptionToMoreCoinsState_Game>());
    }
}
