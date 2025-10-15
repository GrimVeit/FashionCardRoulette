using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseShopClothes : MonoBehaviour
{
    [SerializeField] private Button buttonChoose;
    [SerializeField] private TextMeshProUGUI textName;

    private ClothesType _type = ClothesType.None;

    public void Initialize()
    {
        buttonChoose.onClick.AddListener(() => OnChooseType?.Invoke(_type));
    }

    public void Dispose()
    {
        buttonChoose.onClick.RemoveListener(() => OnChooseType?.Invoke(_type));
    }

    public void SetData(ClothesType type, string name)
    {
        textName.text = name;
        _type = type;
    }

    #region Output

    public event Action<ClothesType> OnChooseType;

    #endregion
}
