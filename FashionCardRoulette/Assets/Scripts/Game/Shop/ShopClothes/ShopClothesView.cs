using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopClothesView : View
{
    [SerializeField] private Button buttonBuy;

    public void Initialize()
    {
        buttonBuy.onClick.AddListener(() => OnBuy?.Invoke());
    }

    public void Dispose()
    {
        buttonBuy.onClick.RemoveListener(() => OnBuy?.Invoke());
    }

    public void Activate()
    {

    }

    public void Deactivate()
    {

    }

    #region Outpuut

    public event Action OnBuy;

    #endregion
}
