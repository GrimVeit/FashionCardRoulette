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
    private readonly IRouletteStateProvider _rouletteStateProvider;
    private readonly IRouletteSpinCountProvider _rouletteSpinCountProvider;

    public RouletteState_Game(IGlobalStateMachineProvider machineProvider, RoulettePresenter roulettePresenter, RouletteBallPresenter rouletteBallPresenter, IChooseNumberProvider chooseNumberProvider, UIGameRoot sceneRoot, IRouletteStateProvider rouletteStateProvider, IRouletteSpinCountProvider rouletteSpinCountProvider)
    {
        _machineProvider = machineProvider;
        _roulettePresenter = roulettePresenter;
        _rouletteBallPresenter = rouletteBallPresenter;
        _chooseNumberProvider = chooseNumberProvider;
        _sceneRoot = sceneRoot;
        _rouletteStateProvider = rouletteStateProvider;
        _rouletteSpinCountProvider = rouletteSpinCountProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - ROULETTE STATE / GAME</color>");

        _rouletteBallPresenter.OnBallStopped += _roulettePresenter.RollBallToSlot;
        _roulettePresenter.OnGetNumberValue += _chooseNumberProvider.SetNumber;
        _roulettePresenter.OnStopSpin += ChangeStateToSetNumber;

        _roulettePresenter.StartSpin();
        _rouletteBallPresenter.StartSpin();
        _rouletteStateProvider.SetGame();
    }

    public void ExitState()
    {
        _rouletteBallPresenter.OnBallStopped -= _roulettePresenter.RollBallToSlot;
        _roulettePresenter.OnGetNumberValue -= _chooseNumberProvider.SetNumber;
        _roulettePresenter.OnStopSpin -= ChangeStateToSetNumber;

        _sceneRoot.CloseRoulettePanel();
        _rouletteSpinCountProvider.RemoveSpin();
    }

    private void ChangeStateToSetNumber()
    {
        _machineProvider.SetState(_machineProvider.GetState<SetNumberState_Game>());
    }
}
