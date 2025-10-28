using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseGenderState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public ChooseGenderState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - CHOOSE GENDER STATE / GAME</color>");

        _sceneRoot.OnClickToContinue_ChooseGender += ChangeStateToChooseCharacter;

        _sceneRoot.OpenChooseGenderPanel();
        _sceneRoot.OpenExitPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToContinue_ChooseGender -= ChangeStateToChooseCharacter;

        _sceneRoot.CloseChooseGenderPanel();
    }

    private void ChangeStateToChooseCharacter()
    {
        _machineProvider.SetState(_machineProvider.GetState<ChooseCharacterState_Game>());
    }
}
