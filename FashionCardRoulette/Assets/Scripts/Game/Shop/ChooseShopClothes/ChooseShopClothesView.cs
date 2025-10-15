using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChooseShopClothesView : View
{
    [SerializeField] private ChooseShopClothes chooseShopClothesPrefab;
    [SerializeField] private Transform transformContent;
    [SerializeField] private ClothesTypeNames clothesTypeNames;

    private readonly List<ChooseShopClothes> shopClothes = new();

    public void SetShopClothesType(List<ClothesType> types)
    {
        for (int i = transformContent.childCount - 1; i >= 0; i--)
        {
            shopClothes[i].OnChooseType -= ChooseType;
            shopClothes[i].Dispose();

            Destroy(transformContent.GetChild(i).gameObject);
        }

        for (int i = 0; i < types.Count; i++)
        {
            var shopClothes = Instantiate(chooseShopClothesPrefab, transformContent);

            shopClothes.OnChooseType += ChooseType;

            shopClothes.SetData(types[i], clothesTypeNames.GetNameByClothesType(types[i]));
            shopClothes.Initialize();
        }
    }

    #region Output

    public event Action<ClothesType> OnChooseType;

    private void ChooseType(ClothesType type)
    {
        OnChooseType?.Invoke(type);
    }

    #endregion
}

[System.Serializable]
public class ClothesTypeName
{
    [SerializeField] private ClothesType type;
    [SerializeField] private string name;

    public ClothesType ClothesType => type;
    public string Name => name;
}

[System.Serializable]
public class ClothesTypeNames
{
    [SerializeField] private List<ClothesTypeName> clothesTypeNames;

    public string GetNameByClothesType(ClothesType type)
    {
        return clothesTypeNames.FirstOrDefault(data => data.ClothesType == type).Name;
    }
}
