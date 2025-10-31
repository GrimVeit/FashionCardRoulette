using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNumberState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly ITaskVisualEventsProvider _taskVisualEventsProvider;
    private readonly ITaskVisualProvider _taskVisualProvider;
    private readonly IRouletteStateProvider _rouletteStateProvider;
    private readonly INumberTrashEventsProvider _numberTrashEventsProvider;

    public SetNumberState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, ITaskVisualEventsProvider taskVisualEventsProvider, ITaskVisualProvider taskVisualProvider, IRouletteStateProvider rouletteStateProvider, INumberTrashEventsProvider numberTrashEventsProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _taskVisualEventsProvider = taskVisualEventsProvider;
        _taskVisualProvider = taskVisualProvider;
        _rouletteStateProvider = rouletteStateProvider;
        _numberTrashEventsProvider = numberTrashEventsProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - SET NUMBER STATE / GAME</color>");

        _taskVisualEventsProvider.OnChooseCell += ChangeStateToCheckFinish;
        _numberTrashEventsProvider.OnMoveToTrash += ChangeStateToCheckFinish;

        _taskVisualProvider.ActivateCells();

        _sceneRoot.OpenTasksPanel();
        _sceneRoot.OpenNumberPanel();
    }

    public void ExitState()
    {
        _taskVisualEventsProvider.OnChooseCell -= ChangeStateToCheckFinish;
        _numberTrashEventsProvider.OnMoveToTrash -= ChangeStateToCheckFinish;

        _taskVisualProvider.DeactivateCells();

        _sceneRoot.CloseNumberPanel();
        _rouletteStateProvider.SetIdle();
    }

    private void ChangeStateToCheckFinish()
    {
        _machineProvider.SetState(_machineProvider.GetState<CheckFinishState_Game>());
    }
}
