using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTypeState_Game : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IStoreClothesEventsProvider _clothesEventsProvider;

    public ShopTypeState_Game(IGlobalStateMachineProvider globalStateMachineProvider, UIGameRoot sceneRoot, IStoreClothesEventsProvider clothesEventsProvider)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _sceneRoot = sceneRoot;
        _clothesEventsProvider = clothesEventsProvider;
    }

    public void EnterState()
    {
        _clothesEventsProvider.OnChangeChooseClothes += ChangeStateToShop;
        _sceneRoot.OnClickToBack_ShopType += ChangeStateToShopWardrobe;

        _sceneRoot.OpenShopTypePanel();
    }

    public void ExitState()
    {
        _clothesEventsProvider.OnChangeChooseClothes -= ChangeStateToShop;
        _sceneRoot.OnClickToBack_ShopType -= ChangeStateToShopWardrobe;

        _sceneRoot.CloseShopTypePanel();
    }

    private void ChangeStateToShopWardrobe()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<ShopWardrobeState_Game>());
    }

    private void ChangeStateToShop(ClothesType type)
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<ShopState_Game>());
    }
}
