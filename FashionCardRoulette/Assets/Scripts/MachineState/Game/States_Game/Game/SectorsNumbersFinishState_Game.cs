using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorsNumbersFinishState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    private IEnumerator timer;

    public SectorsNumbersFinishState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - SECTORS NUMBERS FINISH STATE / GAME</color>");

        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _sceneRoot.CloseNumbersSelectionPanel();
        _sceneRoot.CloseSectorsPanel();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);

        ChangeStateToRoulette();
    }

    private void ChangeStateToRoulette()
    {
        _machineProvider.SetState(_machineProvider.GetState<RouletteState_Game>());
    }
}
