using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IWardrobeClothesEventsProvider _wardrobeClothesEventsProvide;

    public WardrobeState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IWardrobeClothesEventsProvider wardrobeClothesEventsProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _wardrobeClothesEventsProvide = wardrobeClothesEventsProvider;
    }

    public void EnterState()
    {
        _wardrobeClothesEventsProvide.OnSubmitSelect += ChangeStateToWardrobeFitState;
        _sceneRoot.OnClickToBack_Wardrobe += ChangeStateToWardrobeType;

        _sceneRoot.OpenWardrobePanel();
    }

    public void ExitState()
    {
        _wardrobeClothesEventsProvide.OnSubmitSelect -= ChangeStateToWardrobeFitState;
        _sceneRoot.OnClickToBack_Wardrobe -= ChangeStateToWardrobeType;

        _sceneRoot.CloseWardrobePanel();
    }

    private void ChangeStateToWardrobeType()
    {
        _machineProvider.SetState(_machineProvider.GetState<WardrobeTypeState_Game>());
    }

    private void ChangeStateToWardrobeFitState()
    {
        _machineProvider.SetState(_machineProvider.GetState<WardrobeFitClothesState_Game>());
    }
}
