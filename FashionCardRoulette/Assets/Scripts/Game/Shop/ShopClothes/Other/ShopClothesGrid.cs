using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopClothesGrid : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textNumber;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textPrice;

    public void SetData(int number, int price, Sprite sprite, Vector2 sizeImage, float posXImage)
    {
        textNumber.text = number.ToString();
        textPrice.text = price.ToString();

        image.sprite = sprite;
        image.rectTransform.sizeDelta = sizeImage;
        image.rectTransform.localPosition = new Vector3(posXImage, image.rectTransform.localPosition.y, image.rectTransform.localPosition.z);
    }
}
