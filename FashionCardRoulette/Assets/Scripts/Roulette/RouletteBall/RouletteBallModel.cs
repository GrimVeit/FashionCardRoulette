using System;
using UnityEngine;

public class RouletteBallModel
{
    public event Action<Vector3> OnBallStopped;
    public event Action OnStartSpin_Random;
    public event Action<int> OnStartSpin_Number;

    private ISoundProvider _soundProvider;
    //private ISound _soundSpin;
    //private ISound _soundFall;

    public RouletteBallModel(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
        //_soundSpin = _soundProvider.GetSound("BallSpin");
        //_soundFall = _soundProvider.GetSound("BallFall");
    }
    public void StartSpin()
    {
        OnStartSpin_Random?.Invoke();

        //_soundSpin.Play();
    }

    public void StartSpin(int number)
    {
        OnStartSpin_Number?.Invoke(number);

        //_soundSpin.Play();
    }

    public void BallStopped(Vector3 vector)
    {
        OnBallStopped?.Invoke(vector);

        //_soundSpin.Stop();
        //_soundFall.Play();
    }
}
