using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class WardrobeFitClothesView : View
{
    [SerializeField] private List<ClothesVisual> clothesVisuals = new List<ClothesVisual>();
    [SerializeField] private ClothesCharactersGroup clothesCharactersGroups;

    private Tween tweenScale;

    public void SetClothes(Clothes clothes)
    {
        tweenScale?.Kill();

        clothesVisuals.ForEach(data => data.Deactivate());

        var visual = clothesVisuals.FirstOrDefault(data => data.Type == clothes.ClothesType);

        if (visual == null)
        {
            Debug.LogError("Not found clothes visual with type - " + clothes.ClothesType);
            return;
        }

        visual.SetData(clothesCharactersGroups.GetSprite(clothes.ClothesType, clothes.Id));
        visual.Activate();

        var transformVis = visual.Images[0].transform;

        transformVis.localScale = Vector3.zero;

        tweenScale = transformVis.DOScale(1, 1);
    }
}
