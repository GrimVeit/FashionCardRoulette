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
        _sceneRoot.OnClickToContinue_ChooseGender += ChangeStateToChooseCharacter;

        _sceneRoot.OpenChooseGenderPanel();
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
