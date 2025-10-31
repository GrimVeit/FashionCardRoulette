using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly ITaskVisualMoveProvider _visualMoveProvider;

    public ResultState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, ITaskVisualMoveProvider visualMoveProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _visualMoveProvider = visualMoveProvider;
    }

    public void EnterState()
    {
        _sceneRoot.OpenTasksPanel();
        _sceneRoot.OpenResultPanel();
        _sceneRoot.CloseCoinsPanel();

        _visualMoveProvider.MoveFinish();
    }

    public void ExitState()
    {
        _sceneRoot.CloseTasksPanel();
        _sceneRoot.CloseResultPanel();
    }
}
