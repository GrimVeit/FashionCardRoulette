using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IShopClothesEventsProvider _clothesEventsProvider;

    public ShopState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IShopClothesEventsProvider shopClothesEventsProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _clothesEventsProvider = shopClothesEventsProvider;
    }

    public void EnterState()
    {
        _clothesEventsProvider.OnCannotBuy += ChangeStateToNotCoins;
        _clothesEventsProvider.OnCanBuy += ChangeStateToPaycheck;
        _sceneRoot.OnClickToBack_Shop += ChangeStateToShopType;

        _sceneRoot.OpenShopPanel();
    }

    public void ExitState()
    {
        _clothesEventsProvider.OnCannotBuy -= ChangeStateToNotCoins;
        _clothesEventsProvider.OnCanBuy -= ChangeStateToPaycheck;
        _sceneRoot.OnClickToBack_Shop -= ChangeStateToShopType;

        _sceneRoot.CloseShopPanel();
    }

    private void ChangeStateToShopType()
    {
        _machineProvider.SetState(_machineProvider.GetState<ShopTypeState_Game>());
    }

    private void ChangeStateToNotCoins()
    {
        _machineProvider.SetState(_machineProvider.GetState<NotCoinsState_Game>());
    }

    private void ChangeStateToPaycheck()
    {
        _machineProvider.SetState(_machineProvider.GetState<PaycheckState_Game>());
    }
}
