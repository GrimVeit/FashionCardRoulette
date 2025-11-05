using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public FinishState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - FINISH STATE / GAME</color>");

        _sceneRoot.OpenFinishPanel();
    }

    public void ExitState()
    {
        _sceneRoot.CloseFinishPanel();
    }
}
