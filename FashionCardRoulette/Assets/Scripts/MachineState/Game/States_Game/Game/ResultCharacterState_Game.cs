using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCharacterState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IParticleEffectProvider _particleEffectProvider;

    private IEnumerator timer;

    public ResultCharacterState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IParticleEffectProvider particleEffectProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _particleEffectProvider = particleEffectProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - RESULT CHARACTER STATE / GAME</color>");

        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);

        _sceneRoot.OpenCharacterResultPanel();

        _particleEffectProvider.Play("FullCharacter_Bomb");
        _particleEffectProvider.Play("FullCharacter_Light");
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _sceneRoot.CloseCharacterResultPanel();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);

        ChangeStateToFinish();
    }

    private void ChangeStateToFinish()
    {
        _machineProvider.SetState(_machineProvider.GetState<FinishState_Game>());
    }
}
