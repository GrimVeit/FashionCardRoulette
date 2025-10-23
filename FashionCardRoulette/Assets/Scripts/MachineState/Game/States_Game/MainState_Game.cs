using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public MainState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToCharacter_Main += ChangeStateToShopWardrobe;

        _sceneRoot.OpenMainPanel();
        _sceneRoot.OpenCoinsPanel();
        _sceneRoot.OpenExitPanel();
        _sceneRoot.OpenTasksPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToCharacter_Main -= ChangeStateToShopWardrobe;

        _sceneRoot.CloseMainPanel();
        _sceneRoot.CloseExitPanel();
        _sceneRoot.CloseTasksPanel();
    }

    private void ChangeStateToShopWardrobe()
    {
        _machineProvider.SetState(_machineProvider.GetState<ShopWardrobeState_Game>());
    }
}
