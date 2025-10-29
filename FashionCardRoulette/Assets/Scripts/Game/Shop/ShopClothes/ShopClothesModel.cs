using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopClothesModel
{
    public event Action<Clothes> OnChooseClothes;
    public event Action<Clothes> OnUnchooseClothes;
    public event Action OnCanBuy;
    public event Action OnCannotBuy;
    public event Action OnBuy;
    public event Action OnCancelBuy;

    public event Action OnActivate;
    public event Action OnDeactivate;

    public event Action<List<Clothes>> OnAddClothes;
    public event Action OnClearClothes;

    private List<Clothes> _clothesBuy = new List<Clothes>();

    private readonly IStoreClothesActivatorProvider _clothesActivatorProvider;
    private readonly IMoneyProvider _moneyProvider;

    private int _allPrice = 0;

    public ShopClothesModel(IMoneyProvider moneyProvider, IStoreClothesActivatorProvider storeClothesActivatorProvider)
    {
        _moneyProvider = moneyProvider;
        _clothesActivatorProvider = storeClothesActivatorProvider;
    }

    public void ChooseClothes(Clothes clothes)
    {
        if (_clothesBuy.Contains(clothes))
        {
            _clothesBuy.Remove(clothes);
            OnUnchooseClothes?.Invoke(clothes);
        }
        else
        {
            _clothesBuy.Add(clothes);
            OnChooseClothes?.Invoke(clothes);
        }

        if(_clothesBuy.Count > 0)
        {
            OnActivate?.Invoke();
        }
        else
        {
            OnDeactivate?.Invoke();
        }
    }

    public void AllDelete()
    {
        _clothesBuy.Clear();

        OnDeactivate?.Invoke();
    }

    public void CancelBuy()
    {
        for (int i = 0; i < _clothesBuy.Count; i++)
        {
            OnUnchooseClothes?.Invoke(_clothesBuy[i]);
        }

        OnClearClothes?.Invoke();

        AllDelete();

        OnCancelBuy?.Invoke();
    }

    public void Choose()
    {
        OnClearClothes?.Invoke();

        if (_clothesBuy.Count == 0) return;

        _allPrice = 0;

        for (int i = 0; i < _clothesBuy.Count; i++)
        {
            _allPrice += _clothesBuy[i].Price;
        }

        if (_moneyProvider.CanAfford(_allPrice))
        {
            for (int i = 0; i < _clothesBuy.Count; i++)
            {
                OnUnchooseClothes?.Invoke(_clothesBuy[i]);
            }

            OnAddClothes?.Invoke(_clothesBuy);

            OnCanBuy?.Invoke();
        }
        else
        {
            for (int i = 0; i < _clothesBuy.Count; i++)
            {
                OnUnchooseClothes?.Invoke(_clothesBuy[i]);
            }

            OnCannotBuy?.Invoke();

            AllDelete();
        }

        OnChangeAllPrice?.Invoke(_allPrice);
    }

    public void SubmitBuy()
    {
        if (_moneyProvider.CanAfford(_allPrice))
        {
            _moneyProvider.SendMoney(-_allPrice);

            for (int i = 0; i < _clothesBuy.Count; i++)
            {
                OnUnchooseClothes?.Invoke(_clothesBuy[i]);
                _clothesActivatorProvider.OpenClothes(_clothesBuy[i].Id);
            }

            OnClearClothes?.Invoke();

            OnBuy?.Invoke();
        }

        AllDelete();
    }

    #region Output

    public event Action<int> OnChangeAllPrice;

    #endregion
}
