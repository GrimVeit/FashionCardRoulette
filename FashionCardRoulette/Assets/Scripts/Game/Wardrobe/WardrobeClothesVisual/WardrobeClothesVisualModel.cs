using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeClothesVisualModel
{
    private readonly IStoreClothesEventsProvider _storeClothesEventsProvider;
    private readonly IStoreClothesSelectorProvider _storeClothesSelectorProvider;

    private Clothes _currentSelectClothes;

    private readonly ISoundProvider _soundProvider;

    public WardrobeClothesVisualModel(IStoreClothesEventsProvider storeClothesEventsProvider, IStoreClothesSelectorProvider storeClothesSelectorProvider, ISoundProvider soundProvider)
    {
        _storeClothesEventsProvider = storeClothesEventsProvider;
        _storeClothesSelectorProvider = storeClothesSelectorProvider;

        _storeClothesEventsProvider.OnSelectClothes += SetSelectClothes;
        _storeClothesEventsProvider.OnDeselectClothes += SetDeselectClothes;

        _storeClothesEventsProvider.OnChangeChooseClothes += ClearClothes;
        _storeClothesEventsProvider.OnEndChangeChooseClothes += ClearEndClothes;
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _storeClothesEventsProvider.OnSelectClothes -= SetSelectClothes;
        _storeClothesEventsProvider.OnDeselectClothes -= SetDeselectClothes;

        _storeClothesEventsProvider.OnChangeChooseClothes -= ClearClothes;
        _storeClothesEventsProvider.OnEndChangeChooseClothes -= ClearEndClothes;
    }

    public void SetChooseClothes(Clothes clothes)
    {
        if (_currentSelectClothes == null)
        {
            _currentSelectClothes = clothes;
            OnActivate?.Invoke(_currentSelectClothes.ClothesType, _currentSelectClothes.Id);
            OnActivateSubmit?.Invoke();

            _soundProvider.PlayOneShot("Toggle");
            return;
        }
        else
        {
            if(clothes == _currentSelectClothes)
            {
                OnDeactivate?.Invoke(_currentSelectClothes.ClothesType, _currentSelectClothes.Id);
                _currentSelectClothes = null;
                OnDeactivateSubmit?.Invoke();
                return;
            }

            OnDeactivate?.Invoke(_currentSelectClothes.ClothesType, _currentSelectClothes.Id);

            _currentSelectClothes = clothes;
            OnActivate?.Invoke(_currentSelectClothes.ClothesType, _currentSelectClothes.Id);

            OnActivateSubmit?.Invoke();

            _soundProvider.PlayOneShot("Toggle");
        }
    }

    public void LeftRight()
    {
        _soundProvider.PlayOneShot("Click");
    }

    public void SubmitChoice()
    {
        if(_currentSelectClothes == null) return;

        _storeClothesSelectorProvider.SelectClothes(_currentSelectClothes.Id);

        OnDeactivate?.Invoke(_currentSelectClothes.ClothesType, _currentSelectClothes.Id);

        _currentSelectClothes = null;
        OnDeactivateSubmit?.Invoke();

        OnSubmitSelect?.Invoke();

        _soundProvider.PlayOneShot("Click");
    }

    #region Input

    private void SetSelectClothes(Clothes clothes)
    {
        OnSetSelectClothes?.Invoke(clothes);
    }

    private void SetDeselectClothes(Clothes clothes)
    {
        OnSetDeselectClothes?.Invoke(clothes);
    }


    private void ClearClothes(ClothesType type)
    {
        _currentSelectClothes = null;

        OnChangeClothesType?.Invoke(type);
    }

    private void ClearEndClothes()
    {
        OnEndChangeClothesType?.Invoke();
    }

    public event Action<Clothes> OnSetSelectClothes;
    public event Action<Clothes> OnSetDeselectClothes;

    public event Action<ClothesType> OnChangeClothesType;
    public event Action OnEndChangeClothesType;

    #endregion

    public event Action<ClothesType, int> OnActivate;
    public event Action<ClothesType, int> OnDeactivate;

    public event Action OnActivateSubmit;
    public event Action OnDeactivateSubmit;

    public event Action OnSubmitSelect;
}
