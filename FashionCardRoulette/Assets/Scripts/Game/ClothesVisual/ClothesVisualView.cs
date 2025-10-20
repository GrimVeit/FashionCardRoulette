using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClothesVisualView : View
{
    [SerializeField] private List<ClothesVisual> clothesVisuals = new();
    [SerializeField] private ClothesCharactersGroup clothesCharactersGroups;

    public void SetClothes(Clothes clothes)
    {
        var visual = clothesVisuals.FirstOrDefault(data => data.Type == clothes.ClothesType);

        if (visual == null)
        {
            Debug.LogError("Not found clothes visual with type - " + clothes.ClothesType);
            return;
        }

        Debug.Log(clothes.ClothesType + "//" + clothes.Id);

        visual.SetData(clothesCharactersGroups.GetSprite(clothes.ClothesType, clothes.Id));
    }

    public void SetClothesType(List<ClothesType> clothesTypes)
    {
        clothesVisuals.ForEach(data => data.Deactivate());

        for (int i = 0; i < clothesTypes.Count; i++)
        {
            var visual = clothesVisuals.FirstOrDefault(data => data.Type == clothesTypes[i]);

            if(visual == null)
            {
                Debug.LogError("Not found clothes visual with type - " + clothesTypes[i]);
                continue;
            }

            visual.Activate();
        }
    }
}

[Serializable]
public class ClothesVisual
{
    public ClothesType Type => type;
    public List<Image> Images => images;

    [SerializeField] private ClothesType type;
    [SerializeField] private List<Image> images;
    [SerializeField] private Color noneColor;
    [SerializeField] private Color normalColor;

    public void Deactivate()
    {
        images.ForEach(data =>
        {
            data.gameObject.SetActive(false);
            data.color = noneColor;
        });
    }

    public void Activate()
    {
        images.ForEach(data =>
        {
            data.gameObject.SetActive(true);
            data.color = normalColor;
        });
    }

    public void SetData(Sprite sprite)
    {
        images.ForEach(data => data.sprite = sprite);
    } 
}

[Serializable]
public class ClothesCharactersGroup
{
    [SerializeField] private List<ClothesCharacters> clothesCharacters = new();

    public Sprite GetSprite(ClothesType clothesType, int id)
    {
        return clothesCharacters.FirstOrDefault(data => data.ClothesType == clothesType).GetSprite(id);
    }
}

[Serializable]
public class ClothesCharacters
{
    public ClothesType ClothesType => clothesType;

    [SerializeField] private ClothesType clothesType;
    [SerializeField] private List<ClothesCharacter> clothesCharacters = new();

    public Sprite GetSprite(int id)
    {
        return clothesCharacters.FirstOrDefault(data => data.Id == id).Sprite;
    }
}

[Serializable]
public class ClothesCharacter
{
    [SerializeField] private int id;
    [SerializeField] private Sprite sprite;

    public int Id => id;
    public Sprite Sprite => sprite;
}
