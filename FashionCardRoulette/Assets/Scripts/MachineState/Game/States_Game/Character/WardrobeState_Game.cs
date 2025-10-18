using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public WardrobeState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToBack_Wardrobe += ChangeStateToWardrobeType;

        _sceneRoot.OpenWardrobePanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_Wardrobe -= ChangeStateToWardrobeType;

        _sceneRoot.CloseWardrobePanel();
    }

    private void ChangeStateToWardrobeType()
    {
        _machineProvider.SetState(_machineProvider.GetState<WardrobeTypeState_Game>());
    }
}
