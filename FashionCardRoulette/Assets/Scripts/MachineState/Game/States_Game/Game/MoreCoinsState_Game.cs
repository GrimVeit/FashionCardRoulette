using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreCoinsState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IVideoProvider _videoProvider;

    private IEnumerator timer;

    public MoreCoinsState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IVideoProvider videoProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _videoProvider = videoProvider;
    }

    public void EnterState()
    {
        if (timer != null) Coroutines.Stop(timer);

        timer = Timer(2.5f);
        Coroutines.Start(timer);

        _videoProvider.Play("MoreCoins");
        _sceneRoot.OpenMoreCoinsPanel();
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _sceneRoot.CloseMoreCoinsPanel();
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        ChangeStateToMain();
    }

    public void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Game>());
    }
}
