using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDescriptionState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public TaskDescriptionState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToBack_TaskDescription += ChangeStateToMain;

        _sceneRoot.OpenTaskDescriptionPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_TaskDescription -= ChangeStateToMain;

        _sceneRoot.CloseTaskDescriptionPanel();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Game>());
    }
}
