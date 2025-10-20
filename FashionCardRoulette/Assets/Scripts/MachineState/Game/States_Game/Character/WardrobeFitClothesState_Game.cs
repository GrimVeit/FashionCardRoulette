using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeFitClothesState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    private IEnumerator timer;

    public WardrobeFitClothesState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OpenWardrobeFitClothesPanel();

        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        _sceneRoot.CloseWardrobeFitClothesPanel();

        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);

        ChangeStateToWardrobe();
    }

    private void ChangeStateToWardrobe()
    {
        _machineProvider.SetState(_machineProvider.GetState<WardrobeState_Game>());
    }
}
