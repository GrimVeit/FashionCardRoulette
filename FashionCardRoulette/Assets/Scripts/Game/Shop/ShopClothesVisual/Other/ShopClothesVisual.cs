using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopClothesVisual : MonoBehaviour
{
    public int Id => _clothes.Id;
    public ClothesType ClothesType => _clothes.ClothesType;

    [SerializeField] private ToggleCustom toggle;
    [SerializeField] private GameObject buyed;
    [SerializeField] private Image imageClothes;
    [SerializeField] private TextMeshProUGUI textPrice;
    private Clothes _clothes;

    public void Initialize()
    {
        toggle.OnChooseToggle += ChooseToggle;
    }

    public void Dispose()
    {
        toggle.OnChooseToggle -= ChooseToggle;
    }

    public void SetData(Clothes clothes)
    {
        _clothes = clothes;

        textPrice.text = _clothes.Price.ToString();
        imageClothes.sprite = _clothes.Sprite;
        toggle.SetData(_clothes.Id);
    }

    public void ActivateToggle()
    {
        toggle.Activate();
    }

    public void DeactivateToggle()
    {
        toggle.Deactivate();
    }

    public void ActivateBuy()
    {
        toggle.gameObject.SetActive(true);
        buyed.SetActive(false);
        textPrice.gameObject.SetActive(true);
    }

    public void DeactivateBuy()
    {
        toggle.gameObject.SetActive(false);
        buyed.SetActive(true);
        textPrice.gameObject.SetActive(false);
    }

    #region Output

    public event Action<Clothes> OnChooseClothes;

    private void ChooseToggle(int id)
    {
        OnChooseClothes?.Invoke(_clothes);
    }

    #endregion
}

