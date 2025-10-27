using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly RoulettePresenter _roulettePresenter;
    private readonly RouletteBallPresenter _rouletteBallPresenter;
    private readonly IChooseNumberProvider _chooseNumberProvider;

    public RouletteState_Game(IGlobalStateMachineProvider machineProvider, RoulettePresenter roulettePresenter, RouletteBallPresenter rouletteBallPresenter, IChooseNumberProvider chooseNumberProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _roulettePresenter = roulettePresenter;
        _rouletteBallPresenter = rouletteBallPresenter;
        _chooseNumberProvider = chooseNumberProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - ROULETTE");

        _rouletteBallPresenter.OnBallStopped += _roulettePresenter.RollBallToSlot;
        _roulettePresenter.OnGetNumberValue += _chooseNumberProvider.SetNumber;
        _roulettePresenter.OnStopSpin += ChangeStateToSetNumber;

        _roulettePresenter.StartSpin();
        _rouletteBallPresenter.StartSpin();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - ROULETTE");

        _rouletteBallPresenter.OnBallStopped -= _roulettePresenter.RollBallToSlot;
        _roulettePresenter.OnGetNumberValue -= _chooseNumberProvider.SetNumber;
        _roulettePresenter.OnStopSpin -= ChangeStateToSetNumber;

        _sceneRoot.CloseMainPanel();
    }

    private void ChangeStateToSetNumber()
    {
        _machineProvider.SetState(_machineProvider.GetState<SetNumberState_Game>());
    }
}
