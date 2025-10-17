using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopClothesVisualView : View
{
    [SerializeField] private Transform itemsContainer;
    [SerializeField] private Transform content;
    [SerializeField] private ShopClothesVisual shopClothesVisual_Prefab;
    [SerializeField] private ShopClothesConfigs shopClothesConfigs;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    [SerializeField] private Button buttonLeft;
    [SerializeField] private Button buttonRight;

    private readonly List<ShopClothesVisual> shopClothesVisuals = new List<ShopClothesVisual>();

    private ShopClothesConfig _currentShopClothesConfig;
    private int _currentPage = 0;

    public void Initialize()
    {
        buttonLeft.onClick.AddListener(() => Left());
        buttonRight.onClick.AddListener(() => Right());
    }

    public void Dispose()
    {
        buttonLeft.onClick.RemoveListener(() => Left());
        buttonRight.onClick.RemoveListener(() => Right());
    }

    private void Left()
    {
        if(_currentPage > 0)
        {
            _currentPage--;
            UpdatePage();
        }
    }

    private void Right()
    {
        if((_currentPage + 1) * _currentShopClothesConfig.ItemsPerPage < shopClothesVisuals.Count)
        {
            _currentPage++;
            UpdatePage();
        }
    }

    private void UpdatePage()
    {
        int startIndex = _currentPage * _currentShopClothesConfig.ItemsPerPage;
        int endIndex = Mathf.Min(startIndex + _currentShopClothesConfig.ItemsPerPage, shopClothesVisuals.Count);

        for (int i = 0; i < shopClothesVisuals.Count; i++)
        {
            if(i >= startIndex && i < endIndex)
            {
                shopClothesVisuals[i].transform.SetParent(content);
                shopClothesVisuals[i].gameObject.SetActive(true);
            }
            else
            {
                shopClothesVisuals[i].transform.SetParent(itemsContainer);
                shopClothesVisuals[i].gameObject.SetActive(false);
            }
        }

        buttonLeft.gameObject.SetActive(_currentPage > 0);
        buttonRight.gameObject.SetActive((_currentPage + 1) * _currentShopClothesConfig.ItemsPerPage < shopClothesVisuals.Count);
    }

    #region Clothes

    public void SetOpenClothes(Clothes clothes)
    {
        var shopClothesVisual = shopClothesVisuals.FirstOrDefault(data => data.ClothesType == clothes.ClothesType && data.Id == clothes.Id);

        if(shopClothesVisual == null)
        {
            shopClothesVisual = Instantiate(shopClothesVisual_Prefab, itemsContainer);

            shopClothesVisual.OnChooseClothes += ChooseToBuy;

            shopClothesVisual.SetData(clothes);
            shopClothesVisual.Initialize();

            shopClothesVisuals.Add(shopClothesVisual);
        }

        shopClothesVisual.DeactivateBuy();
    }

    public void SetCloseClothes(Clothes clothes)
    {
        var shopClothesVisual = shopClothesVisuals.FirstOrDefault(data => data.ClothesType == clothes.ClothesType && data.Id == clothes.Id);

        if (shopClothesVisual == null)
        {
            shopClothesVisual = Instantiate(shopClothesVisual_Prefab, itemsContainer);

            shopClothesVisual.OnChooseClothes += ChooseToBuy;

            shopClothesVisual.SetData(clothes);
            shopClothesVisual.Initialize();

            shopClothesVisuals.Add(shopClothesVisual);
        }

        shopClothesVisual.ActivateBuy();
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

        _currentPage = 0;
    }

    public void EndChangeClothesType()
    {
        UpdatePage();
    }

    public void ActivateToggle(ClothesType type, int id)
    {
        shopClothesVisuals.FirstOrDefault(data => data.Id == id && data.ClothesType == type).ActivateToggle();
    }

    public void DeactivateToggle(ClothesType type, int id)
    {
        shopClothesVisuals.FirstOrDefault(data => data.Id == id && data.ClothesType == type).DeactivateToggle();
    }

    #endregion

    #region Output

    public event Action<Clothes> OnChooseToBuy;

    private void ChooseToBuy(Clothes clothes)
    {
        OnChooseToBuy?.Invoke(clothes);
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
    [SerializeField] private int itemsPerPage;

    public ClothesType ClothesType => type;
    public RectOffset Padding => padding;
    public Vector2 CellSize => cellSize;
    public Vector2 Spacing => spacing;
    public int ItemsPerPage => itemsPerPage;
}
