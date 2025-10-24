using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNumberState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly ITaskVisualEventsProvider _taskVisualEventsProvider;
    private readonly ITaskVisualProvider _taskVisualProvider;

    public SetNumberState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, ITaskVisualEventsProvider taskVisualEventsProvider, ITaskVisualProvider taskVisualProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _taskVisualEventsProvider = taskVisualEventsProvider;
        _taskVisualProvider = taskVisualProvider;
    }

    public void EnterState()
    {
        _taskVisualEventsProvider.OnChooseCell += ChangeStateToMain;

        _taskVisualProvider.ActivateCells();

        _sceneRoot.OpenTasksPanel();
        _sceneRoot.OpenNumberPanel();
    }

    public void ExitState()
    {
        _taskVisualEventsProvider.OnChooseCell -= ChangeStateToMain;

        _taskVisualProvider.DeactivateCells();

        _sceneRoot.CloseNumberPanel();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Game>());
    }
}
