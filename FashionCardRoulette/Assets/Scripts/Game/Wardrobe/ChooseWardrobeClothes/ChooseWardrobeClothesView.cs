using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWardrobeClothesView : View
{
    [SerializeField] private ChooseWardrobeClothes chooseWardrobeClothesPrefab;
    [SerializeField] private Transform transformContent;
    [SerializeField] private ClothesTypeNames clothesTypeNames;

    private readonly List<ChooseWardrobeClothes> wardrobeClothes = new();

    public void SetWardrobeClothesType(List<ClothesType> types)
    {
        for (int i = transformContent.childCount - 1; i >= 0; i--)
        {
            wardrobeClothes[i].OnChooseType -= ChooseType;
            wardrobeClothes[i].Dispose();

            Destroy(transformContent.GetChild(i).gameObject);
        }

        wardrobeClothes.Clear();

        for (int i = 0; i < types.Count; i++)
        {
            var clothes = Instantiate(chooseWardrobeClothesPrefab, transformContent);

            clothes.OnChooseType += ChooseType;

            clothes.SetData(types[i], clothesTypeNames.GetNameByClothesType(types[i]));
            clothes.Initialize();

            wardrobeClothes.Add(clothes);
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
