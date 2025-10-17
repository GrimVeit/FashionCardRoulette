using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopClothesModel
{
    public event Action<Clothes> OnChooseClothes;
    public event Action<Clothes> OnUnchooseClothes;

    private List<Clothes> _clothesBuy = new List<Clothes>();

    private IStoreClothesActivatorProvider _clothesActivatorProvider;
    private IMoneyProvider _moneyProvider;

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
    }

    public void AllDelete()
    {
        _clothesBuy.Clear();
    }

    public void SubmitBuy()
    {
        if(_clothesBuy.Count == 0) return;

        int allPrice = 0;

        for (int i = 0; i < _clothesBuy.Count; i++)
        {
            allPrice += _clothesBuy[i].Price;
        }

        if (_moneyProvider.CanAfford(allPrice))
        {
            _moneyProvider.SendMoney(-allPrice);

            for (int i = 0; i < _clothesBuy.Count; i++)
            {
                OnUnchooseClothes?.Invoke(_clothesBuy[i]);
                _clothesActivatorProvider.OpenClothes(_clothesBuy[i].Id);
            }
        }
        else
        {
            for (int i = 0; i < _clothesBuy.Count; i++)
            {
                OnUnchooseClothes?.Invoke(_clothesBuy[i]);
            }
        }

        _clothesBuy.Clear();

        Debug.Log(allPrice);
    }
}
