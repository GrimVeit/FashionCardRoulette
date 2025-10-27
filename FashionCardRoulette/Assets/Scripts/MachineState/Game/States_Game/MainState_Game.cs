using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly ITaskVisualEventsProvider _taskVisualEventsProvider;
    private readonly ITaskVisualProvider _taskVisualProvider;

    public MainState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, ITaskVisualEventsProvider taskVisualEventsProvider, ITaskVisualProvider taskVisualProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _taskVisualEventsProvider = taskVisualEventsProvider;
        _taskVisualProvider = taskVisualProvider;
    }

    public void EnterState()
    {
        _taskVisualEventsProvider.OnChooseTask += ChangeStateToTaskDescription;
        _sceneRoot.OnClickToCharacter_Main += ChangeStateToShopWardrobe;
        _sceneRoot.OnClickToSpin_Main += ChangeStateToRoulette;

        _sceneRoot.OpenMainPanel();
        _sceneRoot.OpenCoinsPanel();
        _sceneRoot.OpenExitPanel();
        _sceneRoot.OpenTasksPanel();

        _taskVisualProvider.ActivateInteractionTask();
    }

    public void ExitState()
    {
        _taskVisualEventsProvider.OnChooseTask -= ChangeStateToTaskDescription;
        _sceneRoot.OnClickToCharacter_Main -= ChangeStateToShopWardrobe;
        _sceneRoot.OnClickToSpin_Main -= ChangeStateToRoulette;

        _sceneRoot.CloseExitPanel();

        _taskVisualProvider.DeactivateInteractionTask();
    }

    private void ChangeStateToRoulette()
    {
        _sceneRoot.CloseTasksPanel();

        _machineProvider.SetState(_machineProvider.GetState<RouletteState_Game>());
    }

    private void ChangeStateToShopWardrobe()
    {
        _sceneRoot.CloseTasksPanel();
        _sceneRoot.CloseMainPanel();

        _machineProvider.SetState(_machineProvider.GetState<ShopWardrobeState_Game>());
    }

    private void ChangeStateToTaskDescription()
    {
        _sceneRoot.CloseTasksPanel();
        _sceneRoot.CloseMainPanel();

        _machineProvider.SetState(_machineProvider.GetState<TaskDescriptionState_Game>());
    }
}
