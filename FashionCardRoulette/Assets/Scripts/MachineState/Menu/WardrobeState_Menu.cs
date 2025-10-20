using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeState_Menu : IState
{
    private IGlobalStateMachineProvider _machineProvider;
    private UIMainMenuRoot _sceneRoot;

    public WardrobeState_Menu(IGlobalStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToBack_Wardrobe += ChangeStateToMain;

        _sceneRoot.OpenWardrobePanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_Wardrobe -= ChangeStateToMain;

        _sceneRoot.CloseWardrobePanel();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Menu>());
    }
}
