using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopClothesView : View
{
    [SerializeField] private Button buttonChoose;
    [SerializeField] private ScaleEffect scaleEffect_Choose;

    [SerializeField] private Button buttonSubmitChoice;
    [SerializeField] private Button buttonCancel;

    [SerializeField] private ShopClothesSizes shopClothesSizes;

    [SerializeField] private ShopClothesGrid shopClothesGridPrefab;
    [SerializeField] private Transform transformContent;

    private List<ShopClothesGrid> shopClothesGrids = new();

    public void Initialize()
    {
        buttonChoose.onClick.AddListener(() => OnChoose?.Invoke());
        scaleEffect_Choose.Initialize();

        buttonSubmitChoice.onClick.AddListener(() => OnSubmitChoice?.Invoke());
        buttonCancel.onClick.AddListener(() => OnCancel?.Invoke());
    }

    public void Dispose()
    {
        buttonChoose.onClick.RemoveListener(() => OnChoose?.Invoke());
        scaleEffect_Choose.Dispose();

        buttonSubmitChoice.onClick.RemoveListener(() => OnSubmitChoice?.Invoke());
        buttonCancel.onClick.RemoveListener(() => OnCancel?.Invoke());
    }

    public void SetClothes(List<Clothes> clothes)
    {
        if (shopClothesGrids.Count != 0)
        {
            for (int i = shopClothesGrids.Count - 1; i >= 0; i--)
            {
                if (shopClothesGrids[i] != null)
                {
                    Destroy(shopClothesGrids[i].gameObject);
                    shopClothesGrids.RemoveAt(i);
                }
            }
        }

        for (int i = 0; i < clothes.Count; i++)
        {
            var size = shopClothesSizes.GetShopClothesSize(clothes[i].ClothesType);

            if(size == null)
            {
                Debug.LogError("Not found ShopClothesSizes with type - " + clothes[i]);
                continue;
            }

            var grid = Instantiate(shopClothesGridPrefab, transformContent);

            grid.SetData(i + 1, clothes[i].Price, clothes[i].Sprite, size.VectorSize, size.LeftPos);

            shopClothesGrids.Add(grid);
        }
    }

    public void Clear()
    {
        if (shopClothesGrids.Count != 0)
        {
            for (int i = shopClothesGrids.Count - 1; i >= 0; i--)
            {
                if (shopClothesGrids[i] != null)
                {
                    Destroy(shopClothesGrids[i].gameObject);
                    shopClothesGrids.RemoveAt(i);
                }
            }
        }
    }

    public void Activate()
    {
        scaleEffect_Choose.ActivateEffect();
    }

    public void Deactivate()
    {
        scaleEffect_Choose.DeactivateEffect();
    }

    #region Outpuut

    public event Action OnChoose;

    public event Action OnSubmitChoice;

    public event Action OnCancel;

    #endregion
}

[System.Serializable]
public class ShopClothesSizes
{
    [SerializeField] private List<ShopClothesSize> sizes = new();

    public ShopClothesSize GetShopClothesSize(ClothesType clothesType)
    {
        return sizes.FirstOrDefault(data => data.ClothesType == clothesType);
    }
}

[System.Serializable]
public class ShopClothesSize
{
    [SerializeField] private ClothesType type;
    [SerializeField] private Vector2 vectorSize;
    [SerializeField] private float leftPos;

    public ClothesType ClothesType => type;
    public Vector2 VectorSize => vectorSize;
    public float LeftPos => leftPos;
}
