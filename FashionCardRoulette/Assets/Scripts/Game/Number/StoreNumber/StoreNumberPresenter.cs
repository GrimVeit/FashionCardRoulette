using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNumberPresenter : IStoreNumberProvider, IStoreNumberInfoProvider
{
    private readonly StoreNumberModel _model;

    public StoreNumberPresenter(StoreNumberModel model)
    {
        _model = model;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Input

    public void SetSector(int sector) => _model.SetSector(sector);

    public int GetRandomNumber() => _model.GetRandomNumber();

    #endregion
}

public interface IStoreNumberProvider
{
    void SetSector(int sector);
}

public interface IStoreNumberInfoProvider
{
    public int GetRandomNumber();
}
