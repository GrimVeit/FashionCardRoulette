using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopClothesVisual : MonoBehaviour
{
    public int Id => toggle.Id;
    public ClothesType ClothesType => clothesType;

    [SerializeField] private ToggleCustom toggle;
    [SerializeField] private GameObject buyed;
    [SerializeField] private Image imageCharacter;
    private ClothesType clothesType;

    public void Initialize()
    {
        toggle.OnChooseToggle += ChooseToggle;
    }

    public void Dispose()
    {
        toggle.OnChooseToggle -= ChooseToggle;
    }

    public void SetData(ClothesType type, int id, Sprite sprite)
    {
        clothesType = type;
        imageCharacter.sprite = sprite;
        toggle.SetData(id);
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
    }

    public void DeactivateBuy()
    {
        toggle.gameObject.SetActive(false);
        buyed.SetActive(true);
    }

    #region Output

    public event Action<ClothesType, int> OnChooseClothes;

    private void ChooseToggle(int id)
    {
        OnChooseClothes?.Invoke(clothesType, toggle.Id);
    }

    #endregion
}

