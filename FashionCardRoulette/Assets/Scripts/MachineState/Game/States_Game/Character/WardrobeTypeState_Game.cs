using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeTypeState_Game : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IStoreClothesEventsProvider _clothesEventsProvider;

    public WardrobeTypeState_Game(IGlobalStateMachineProvider globalStateMachineProvider, UIGameRoot sceneRoot, IStoreClothesEventsProvider clothesEventsProvider)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _sceneRoot = sceneRoot;
        _clothesEventsProvider = clothesEventsProvider;
    }

    public void EnterState()
    {
        _clothesEventsProvider.OnChangeChooseClothes += ChangeStateToWardrobe;
        _sceneRoot.OnClickToBack_WardrobeType += ChangeStateToShopWardrobe;

        _sceneRoot.OpenWardrobeTypePanel();
    }

    public void ExitState()
    {
        _clothesEventsProvider.OnChangeChooseClothes -= ChangeStateToWardrobe;
        _sceneRoot.OnClickToBack_WardrobeType -= ChangeStateToShopWardrobe;

        _sceneRoot.CloseWardrobeTypePanel();
    }

    private void ChangeStateToShopWardrobe()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<ShopWardrobeState_Game>());
    }

    private void ChangeStateToWardrobe(ClothesType type)
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<WardrobeState_Game>());
    }
}
