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
        Debug.Log("<color=red>ACTIVATE STATE - MAIN STATE / GAME</color>");

        _taskVisualEventsProvider.OnChooseTask += ChangeStateToTaskDescription;
        _sceneRoot.OnClickToCharacter_Main += ChangeStateToShopWardrobe;
        _sceneRoot.OnClickToSpin_Main += ChangeStateToNumbersSelection;

        _sceneRoot.OpenRoulettePanel();
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
        _sceneRoot.OnClickToSpin_Main -= ChangeStateToNumbersSelection;

        _sceneRoot.CloseExitPanel();
        _sceneRoot.CloseMainPanel();

        _taskVisualProvider.DeactivateInteractionTask();
    }

    private void ChangeStateToNumbersSelection()
    {
        _sceneRoot.CloseTasksPanel();

        _machineProvider.SetState(_machineProvider.GetState<NumberSelectionState_Game>());
    }

    private void ChangeStateToShopWardrobe()
    {
        _sceneRoot.CloseTasksPanel();
        _sceneRoot.CloseRoulettePanel();

        _machineProvider.SetState(_machineProvider.GetState<ShopWardrobeState_Game>());
    }

    private void ChangeStateToTaskDescription()
    {
        _sceneRoot.CloseTasksPanel();
        _sceneRoot.CloseRoulettePanel();

        _machineProvider.SetState(_machineProvider.GetState<TaskDescriptionState_Game>());
    }
}
