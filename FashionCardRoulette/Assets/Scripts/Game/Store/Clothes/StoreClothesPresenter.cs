using System;
using System.Collections.Generic;

public class StoreClothesPresenter  : IStoreClothesEventsProvider, IStoreClothesChooseProvider, IStoreClothesActivatorProvider, IStoreClothesSelectorProvider
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

    public event Action OnEndChangeChooseClothes
    {
        add => _model.OnEndChangeChooseClothes += value;
        remove => _model.OnEndChangeChooseClothes -= value; 
    }




    public event Action<Clothes> OnSelectClothes
    {
        add => _model.OnSelectClothes += value;
        remove => _model.OnSelectClothes -= value;
    }

    public event Action<Clothes> OnDeselectClothes
    {
        add => _model.OnDeselectClothes += value;
        remove => _model.OnDeselectClothes -= value;
    }

    #endregion

    #region Input

    public void ChooseByClothesTypeForShop(ClothesType clothesType)
    {
        _model.ChooseByClothesTypeForShop(clothesType);
    }

    public void ChooseByClothesTypeForWardrobe(ClothesType clothesType)
    {
        _model.ChooseByClothesTypeForWardrobe(clothesType);
    }

    public void OpenClothes(int id)
    {
        _model.OpenClothes(id);
    }

    public void SelectClothes(int id)
    {
        _model.SelectClothes(id);
    }

    #endregion
}

public interface IStoreClothesEventsProvider
{
    public event Action<Clothes> OnChooseOpenClothes;
    public event Action<Clothes> OnChooseCloseClothes;
    public event Action<ClothesType> OnChangeChooseClothes;
    public event Action OnEndChangeChooseClothes;

    public event Action<Clothes> OnSelectClothes;
    public event Action<Clothes> OnDeselectClothes;
}

public interface IStoreClothesChooseProvider
{
    public void ChooseByClothesTypeForShop(ClothesType clothesType);
    public void ChooseByClothesTypeForWardrobe(ClothesType clothesType);
}

public interface IStoreClothesActivatorProvider
{
    public void OpenClothes(int id);
}

public interface IStoreClothesSelectorProvider
{
    public void SelectClothes(int id);
}
