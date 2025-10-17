using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopClothesVisualView : View
{
    [SerializeField] private Transform transformContent;
    [SerializeField] private ShopClothesVisual shopClothesVisual_Prefab;
    [SerializeField] private ShopClothesConfigs shopClothesConfigs;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    private readonly List<ShopClothesVisual> shopClothesVisuals = new List<ShopClothesVisual>();

    private ShopClothesConfig _currentShopClothesConfig;

    public void SetOpenClothes(Clothes clothes)
    {
        var shopClothesVisual = shopClothesVisuals.FirstOrDefault(data => data.ClothesType == clothes.ClothesType && data.Id == clothes.Id);

        
    }

    public void SetCloseClothes(Clothes clothes)
    {

    }

    public void ChangeClothesType(ClothesType type)
    {
        if (shopClothesVisuals.Count != 0)
        {
            for (int i = shopClothesVisuals.Count - 1; i >= 0; i--)
            {
                if (shopClothesVisuals[i] != null)
                {
                    shopClothesVisuals[i].OnChooseClothes -= ChooseToBuy;
                    shopClothesVisuals[i].Dispose();

                    Destroy(shopClothesVisuals[i].gameObject);
                    shopClothesVisuals.RemoveAt(i);
                }
            }
        }

        _currentShopClothesConfig = shopClothesConfigs.GetShopClothesConfig(type);

        if(_currentShopClothesConfig == null)
        {
            Debug.LogError("Not found ShopClothesConfig with ClothesType - " + type);
            return;
        }

        gridLayoutGroup.padding = _currentShopClothesConfig.Padding;
        gridLayoutGroup.cellSize = _currentShopClothesConfig.CellSize;
        gridLayoutGroup.spacing = _currentShopClothesConfig.Spacing;
    }

    #region Output

    public event Action<ClothesType, int> OnChooseToBuy;

    private void ChooseToBuy(ClothesType type, int id)
    {
        OnChooseToBuy?.Invoke(type, id);
    }

    #endregion
}

[Serializable]
public class ShopClothesConfigs
{
    [SerializeField] private List<ShopClothesConfig> shopClothesConfigs = new List<ShopClothesConfig>();

    public ShopClothesConfig GetShopClothesConfig(ClothesType type)
    {
        return shopClothesConfigs.FirstOrDefault(data => data.ClothesType == type);
    }
}


[Serializable]
public class ShopClothesConfig
{
    [SerializeField] private ClothesType type;
    [SerializeField] private RectOffset padding;
    [SerializeField] private Vector2 cellSize;
    [SerializeField] private Vector2 spacing;

    public ClothesType ClothesType => type;
    public RectOffset Padding => padding;
    public Vector2 CellSize => cellSize;
    public Vector2 Spacing => spacing;
}
