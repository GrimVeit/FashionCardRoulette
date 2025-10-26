using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromTaskDescriptionToMoreCoinsState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    private IEnumerator timer;

    public FromTaskDescriptionToMoreCoinsState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        if (timer != null) Coroutines.Stop(timer);

        timer = Timer(0.5f);
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _sceneRoot.CloseTaskDescriptionPanel();
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        ChangeStateToMoreCoins();
    }

    private void ChangeStateToMoreCoins()
    {
        _machineProvider.SetState(_machineProvider.GetState<MoreCoinsState_Game>());
    }
}
