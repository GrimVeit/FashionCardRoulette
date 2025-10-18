using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopClothesView : View
{
    [SerializeField] private Button buttonBuy;
    [SerializeField] private ScaleEffect scaleEffect;

    public void Initialize()
    {
        buttonBuy.onClick.AddListener(() => OnBuy?.Invoke());
        scaleEffect.Initialize();
    }

    public void Dispose()
    {
        buttonBuy.onClick.RemoveListener(() => OnBuy?.Invoke());
        scaleEffect.Dispose();
    }

    public void Activate()
    {
        scaleEffect.ActivateEffect();
    }

    public void Deactivate()
    {
        scaleEffect.DeactivateEffect();
    }

    #region Outpuut

    public event Action OnBuy;

    #endregion
}
