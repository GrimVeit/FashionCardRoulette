using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public ShopState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToBack_Shop += ChangeStateToShopType;

        _sceneRoot.OpenShopPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_Shop -= ChangeStateToShopType;

        _sceneRoot.CloseShopPanel();
    }

    private void ChangeStateToShopType()
    {
        _machineProvider.SetState(_machineProvider.GetState<ShopTypeState_Game>());
    }
}
