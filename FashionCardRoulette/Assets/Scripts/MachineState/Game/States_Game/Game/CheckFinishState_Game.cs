using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinishState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly ITaskVisualInfoProvider _taskVisualInfoProvider;

    public CheckFinishState_Game(IGlobalStateMachineProvider machineProvider, ITaskVisualInfoProvider taskVisualInfoProvider)
    {
        _machineProvider = machineProvider;
        _taskVisualInfoProvider = taskVisualInfoProvider;
    }

    public void EnterState()
    {
        if (_taskVisualInfoProvider.IsAllTaskFinished())
        {
            ChangeStateToResult();
        }
        else
        {
            ChangeStateToMainMenu();
        }
    }

    public void ExitState()
    {

    }

    private void ChangeStateToMainMenu()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Game>());
    }

    private void ChangeStateToResult()
    {

    }
}
