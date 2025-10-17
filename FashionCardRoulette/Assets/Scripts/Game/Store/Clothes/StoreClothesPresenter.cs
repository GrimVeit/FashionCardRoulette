using System;
using System.Collections.Generic;

public class StoreClothesPresenter  : IStoreClothesEventsProvider, IStoreClothesChooseProvider
{
    private readonly StoreClothesModel _model;

    public StoreClothesPresenter(StoreClothesModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Output

    public event Action<Clothes> OnChooseOpenClothes
    {
        add => _model.OnChooseOpenClothes += value;
        remove => _model.OnChooseOpenClothes -= value;
    }

    public event Action<Clothes> OnChooseCloseClothes
    {
        add => _model.OnChooseCloseClothes += value;
        remove => _model.OnChooseCloseClothes -= value;
    }

    public event Action<ClothesType> OnChangeChooseClothes
    {
        add => _model.OnChangeChooseClothes += value;
        remove => _model.OnChangeChooseClothes -= value;
    }

    #endregion

    #region Input

    public void ChooseByClothesType(ClothesType clothesType)
    {
        _model.ChooseByClothesType(clothesType);
    }

    #endregion
}

public interface IStoreClothesEventsProvider
{
    public event Action<Clothes> OnChooseOpenClothes;
    public event Action<Clothes> OnChooseCloseClothes;
    public event Action<ClothesType> OnChangeChooseClothes;
}

public interface IStoreClothesChooseProvider
{
    public void ChooseByClothesType(ClothesType clothesType);
}
