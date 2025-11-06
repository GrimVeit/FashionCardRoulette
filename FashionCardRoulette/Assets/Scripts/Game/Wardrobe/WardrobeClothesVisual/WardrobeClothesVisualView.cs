using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WardrobeClothesVisualView : View
{
    [SerializeField] private Transform itemsContainer;
    [SerializeField] private Transform content;
    [SerializeField] private WardrobeClothesVisual wardrobeClothesVisual_Prefab;
    [SerializeField] private ShopClothesConfigs shopClothesConfigs;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    [SerializeField] private Button buttonLeft;
    [SerializeField] private Button buttonRight;
    [SerializeField] private Button buttonSubmit;
    [SerializeField] private ScaleEffect scaleEffect_Left;
    [SerializeField] private ScaleEffect scaleEffect_Right;
    [SerializeField] private ScaleEffect scaleEffect_Submit;

    private readonly List<WardrobeClothesVisual> wardrobeClothesVisuals = new();

    private ShopClothesConfig _currentWardrobeClothesConfig;
    private int _currentPage = 0;

    public void Initialize()
    {
        buttonLeft.onClick.AddListener(() => Left());
        buttonRight.onClick.AddListener(() => Right());
        buttonSubmit.onClick.AddListener(() => OnSubmitChoice?.Invoke());

        scaleEffect_Left.Initialize();
        scaleEffect_Right.Initialize();
        scaleEffect_Submit.Initialize();
    }

    public void Dispose()
    {
        buttonLeft.onClick.RemoveListener(() => Left());
        buttonRight.onClick.RemoveListener(() => Right());
        buttonSubmit.onClick.RemoveListener(() => OnSubmitChoice?.Invoke());

        scaleEffect_Left.Dispose();
        scaleEffect_Right.Dispose();
        scaleEffect_Submit.Dispose();
    }

    private void Left()
    {
        if (_currentPage > 0)
        {
            _currentPage--;
            UpdatePage();

            OnClickLeftRight?.Invoke();
        }
    }

    private void Right()
    {
        if ((_currentPage + 1) * _currentWardrobeClothesConfig.ItemsPerPage < wardrobeClothesVisuals.Count)
        {
            _currentPage++;
            UpdatePage();

            OnClickLeftRight?.Invoke();
        }
    }

    private void UpdatePage()
    {
        if (wardrobeClothesVisuals.Count == 0) return;

        int startIndex = _currentPage * _currentWardrobeClothesConfig.ItemsPerPage;
        int endIndex = Mathf.Min(startIndex + _currentWardrobeClothesConfig.ItemsPerPage, wardrobeClothesVisuals.Count);

        for (int i = 0; i < wardrobeClothesVisuals.Count; i++)
        {
            if (i >= startIndex && i < endIndex)
            {
                wardrobeClothesVisuals[i].transform.SetParent(content);
                wardrobeClothesVisuals[i].Show(0.2f);
            }
            else
            {
                wardrobeClothesVisuals[i].transform.SetParent(itemsContainer);
                wardrobeClothesVisuals[i].Hide(0.2f);
            }
        }

        if (_currentPage > 0)
        {
            scaleEffect_Left.ActivateEffect();
        }
        else
        {
            scaleEffect_Left.DeactivateEffect();
        }

        if ((_currentPage + 1) * _currentWardrobeClothesConfig.ItemsPerPage < wardrobeClothesVisuals.Count)
        {
            scaleEffect_Right.ActivateEffect();
        }
        else
        {
            scaleEffect_Right.DeactivateEffect();
        }

        //buttonLeft.gameObject.SetActive(_currentPage > 0);
        //buttonRight.gameObject.SetActive((_currentPage + 1) * _currentShopClothesConfig.ItemsPerPage < shopClothesVisuals.Count);
    }

    #region Clothes

    public void SetSelectClothes(Clothes clothes)
    {
        var shopClothesVisual = wardrobeClothesVisuals.FirstOrDefault(data => data.ClothesType == clothes.ClothesType && data.Id == clothes.Id);

        if (shopClothesVisual == null)
        {
            shopClothesVisual = Instantiate(wardrobeClothesVisual_Prefab, itemsContainer);

            shopClothesVisual.OnChooseClothes += ChooseToSelect;

            shopClothesVisual.SetData(clothes);
            shopClothesVisual.Initialize();

            wardrobeClothesVisuals.Add(shopClothesVisual);
        }

        shopClothesVisual.DeactivateChoose();
    }

    public void SetDeselectClothes(Clothes clothes)
    {
        var shopClothesVisual = wardrobeClothesVisuals.FirstOrDefault(data => data.ClothesType == clothes.ClothesType && data.Id == clothes.Id);

        if (shopClothesVisual == null)
        {
            shopClothesVisual = Instantiate(wardrobeClothesVisual_Prefab, itemsContainer);

            shopClothesVisual.OnChooseClothes += ChooseToSelect;

            shopClothesVisual.SetData(clothes);
            shopClothesVisual.Initialize();

            wardrobeClothesVisuals.Add(shopClothesVisual);
        }

        shopClothesVisual.ActivateChoose();
    }

    public void ChangeClothesType(ClothesType type)
    {
        if (wardrobeClothesVisuals.Count != 0)
        {
            for (int i = wardrobeClothesVisuals.Count - 1; i >= 0; i--)
            {
                if (wardrobeClothesVisuals[i] != null)
                {
                    wardrobeClothesVisuals[i].OnChooseClothes -= ChooseToSelect;
                    wardrobeClothesVisuals[i].Dispose();

                    Destroy(wardrobeClothesVisuals[i].gameObject);
                    wardrobeClothesVisuals.RemoveAt(i);
                }
            }
        }

        _currentWardrobeClothesConfig = shopClothesConfigs.GetShopClothesConfig(type);

        if (_currentWardrobeClothesConfig == null)
        {
            Debug.LogError("Not found ShopClothesConfig with ClothesType - " + type);
            return;
        }

        gridLayoutGroup.padding = _currentWardrobeClothesConfig.Padding;
        gridLayoutGroup.cellSize = _currentWardrobeClothesConfig.CellSize;
        gridLayoutGroup.spacing = _currentWardrobeClothesConfig.Spacing;

        _currentPage = 0;
    }

    public void EndChangeClothesType()
    {
        UpdatePage();
    }

    public void ActivateToggle(ClothesType type, int id)
    {
        wardrobeClothesVisuals.FirstOrDefault(data => data.Id == id && data.ClothesType == type).ActivateToggle();
    }

    public void DeactivateToggle(ClothesType type, int id)
    {
        wardrobeClothesVisuals.FirstOrDefault(data => data.Id == id && data.ClothesType == type).DeactivateToggle();
    }


    public void ActivateSubmit()
    {
        scaleEffect_Submit.ActivateEffect();
    }

    public void DeactivateSubmit()
    {
        scaleEffect_Submit.DeactivateEffect();
    }

    #endregion

    #region Output

    public event Action OnSubmitChoice;
    public event Action<Clothes> OnChooseToSelect;
    public event Action OnClickLeftRight;

    private void ChooseToSelect(Clothes clothes)
    {
        OnChooseToSelect?.Invoke(clothes);
    }

    #endregion
}
