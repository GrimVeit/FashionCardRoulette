using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaycheckState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IShopClothesEventsProvider _clothesEventsProvider;


    public PaycheckState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IShopClothesEventsProvider clothesEventsProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _clothesEventsProvider = clothesEventsProvider;
    }

    public void EnterState()
    {
        _clothesEventsProvider.OnBuy += ChangeStateToShop;
        _clothesEventsProvider.OnCancelBuy += ChangeStateToShop;

        _sceneRoot.OpenPaycheckPanel();
    }

    public void ExitState()
    {
        _clothesEventsProvider.OnBuy -= ChangeStateToShop;
        _clothesEventsProvider.OnCancelBuy -= ChangeStateToShop;

        _sceneRoot.ClosePaycheckPanel();
    }

    private void ChangeStateToShop()
    {
        _machineProvider.SetState(_machineProvider.GetState<ShopState_Game>());
    }
}
