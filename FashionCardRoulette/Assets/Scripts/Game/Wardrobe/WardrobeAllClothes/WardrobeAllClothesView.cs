using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WardrobeAllClothesView : View
{
    [SerializeField] private Image imagePrefab;
    [SerializeField] private ShopClothesSizes clothesSizes;
    [SerializeField] private WardrobeAllClothesZones clothesZones;

    public void SetClothes(Clothes clothes)
    {
        var size = clothesSizes.GetShopClothesSize(clothes.ClothesType);
        var transformSpawn = clothesZones.GetTransformSpawn(clothes.ClothesType);

        var element = Instantiate(imagePrefab, transformSpawn);
        element.rectTransform.sizeDelta = size.VectorSize;
        element.sprite = clothes.Sprite;
    }
}

[Serializable]
public class WardrobeAllClothesZones
{
    [SerializeField] private List<WardrobeAllClothesZone> zones = new();

    public Transform GetTransformSpawn(ClothesType type)
    {
        return zones.FirstOrDefault(data => data.ClothesType == type).TransformSpawn;
    }
}

[Serializable]
public class WardrobeAllClothesZone
{
    [SerializeField] private ClothesType type;
    [SerializeField] private Transform transform;

    public ClothesType ClothesType => type;
    public Transform TransformSpawn => transform;
}
