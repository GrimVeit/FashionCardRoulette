using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseNumberView : View
{
    [SerializeField] private TextMeshProUGUI textNumber;
    [SerializeField] private Image imageColor;
    [SerializeField] private NumberColorSprites numberColorSprites;

    public void SetNumber(NumberValue numberValue)
    {
        textNumber.text = numberValue.Number.ToString();
        imageColor.sprite = numberColorSprites.GetSprite(numberValue.Color);
    }
}

public class NumberColorSprites
{
    [SerializeField] private List<NumberColorSprite> numberColorSprites = new();

    public Sprite GetSprite(ColorNumber colorNumber)
    {
        return numberColorSprites.FirstOrDefault(data => data.ColorNumber == colorNumber).ColorSprite;
    }
}

public class NumberColorSprite
{
    [SerializeField] private ColorNumber colorNumber;
    [SerializeField] private Sprite colorSprite;

    public ColorNumber ColorNumber => colorNumber;
    public Sprite ColorSprite => colorSprite;
}
