using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotCoinsState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    private IEnumerator timer;

    public NotCoinsState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OpenNotCoinsPanel();

        if(timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        _sceneRoot.CloseNotCoinsPanel();

        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);

        ChangeStateToShop();
    }

    private void ChangeStateToShop()
    {
        _machineProvider.SetState(_machineProvider.GetState<ShopState_Game>());
    }
}
