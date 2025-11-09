using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreCoinsState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IVideoProvider _videoProvider;
    private readonly ISoundProvider _soundProvider;
    private readonly ISound _soundStone;

    private IEnumerator timer;

    public MoreCoinsState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IVideoProvider videoProvider, ISoundProvider soundProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _videoProvider = videoProvider;
        _soundProvider = soundProvider;

        _soundStone = _soundProvider.GetSound("Stone");
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - MORE COINS STATE / GAME</color>");

        if (timer != null) Coroutines.Stop(timer);

        timer = Timer(2.5f);
        Coroutines.Start(timer);

        _videoProvider.Play("MoreCoins");
        _sceneRoot.OpenMoreCoinsPanel();

        _soundStone.Play();
        _soundStone.SetVolume(0, 0.4f, 0.2f);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _soundStone.SetVolume(0.4f, 0f, 0.2f, _soundStone.Stop);

        _sceneRoot.CloseMoreCoinsPanel();
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        ChangeStateToCheckFinish();
    }

    public void ChangeStateToCheckFinish()
    {
        _machineProvider.SetState(_machineProvider.GetState<CheckFinishState_Game>());
    }
}
