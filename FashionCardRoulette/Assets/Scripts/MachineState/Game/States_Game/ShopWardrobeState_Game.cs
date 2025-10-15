using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWardrobeState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public ShopWardrobeState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToBack_ShopWardrobe += ChangeStateToMain;

        _sceneRoot.OpenShopWardrobePanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_ShopWardrobe -= ChangeStateToMain;

        _sceneRoot.CloseShopWardrobePanel();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Game>());
    }
}
