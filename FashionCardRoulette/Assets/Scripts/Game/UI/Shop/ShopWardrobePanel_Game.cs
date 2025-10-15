using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWardrobePanel_Game : MovePanel
{
    [SerializeField] private Button buttonBack;
    [SerializeField] private Button buttonWardrobe;
    [SerializeField] private Button buttonShop;

    public override void Initialize()
    {
        base.Initialize();

        buttonBack.onClick.AddListener(() => OnClickToBack?.Invoke());
        buttonWardrobe.onClick.AddListener(() => OnClickToWardrobe?.Invoke());
        buttonShop.onClick.AddListener(() => OnClickToShop?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonBack.onClick.RemoveListener(() => OnClickToBack?.Invoke());
        buttonWardrobe.onClick.RemoveListener(() => OnClickToWardrobe?.Invoke());
        buttonShop.onClick.RemoveListener(() => OnClickToShop?.Invoke());
    }

    #region Output

    public event Action OnClickToBack;
    public event Action OnClickToWardrobe;
    public event Action OnClickToShop;

    #endregion
}
