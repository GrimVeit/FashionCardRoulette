using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacterState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public ChooseCharacterState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - CHOOSE CHARACTER STATE / GAME</color>");

        _sceneRoot.OnClickToContinue_ChooseCharacter += ChangeStateToMain;
        _sceneRoot.OnClickToBack_ChooseCharacter += ChangeStateToChooseGender;

        _sceneRoot.OpenChooseCharacterPanel();
        _sceneRoot.OpenExitPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToContinue_ChooseCharacter -= ChangeStateToMain;
        _sceneRoot.OnClickToBack_ChooseCharacter -= ChangeStateToChooseGender;

        _sceneRoot.CloseChooseCharacterPanel();
    }

    private void ChangeStateToChooseGender()
    {
        _machineProvider.SetState(_machineProvider.GetState<ChooseGenderState_Game>());
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Game>());
    }
}
