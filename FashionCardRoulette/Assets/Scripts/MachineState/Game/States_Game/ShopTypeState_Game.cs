using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTypeState_Game : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly UIGameRoot _sceneRoot;

    public ShopTypeState_Game(IGlobalStateMachineProvider globalStateMachineProvider, UIGameRoot sceneRoot)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToBack_ShopType += ChangeStateToShop;

        _sceneRoot.OpenShopTypePanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_ShopType -= ChangeStateToShop;

        _sceneRoot.CloseShopTypePanel();
    }

    private void ChangeStateToShop()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<ShopWardrobeState_Game>());
    }
}
