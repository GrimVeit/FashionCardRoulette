using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clothes", menuName = "Game/Clothes/New")]
public class Clothes : ScriptableObject
{
    [SerializeField] private ClothesType type;
    [SerializeField] private int id;
    [SerializeField] private string description;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int price;

    private ClothesData _data;

    public ClothesType ClothesType => type;
    public int Id => id;
    public string Description => description;
    public Sprite Sprite => sprite;
    public ClothesData Data => _data;
    public int Price => price;


    public void SetData(ClothesData data)
    {
        _data = data;
    }
}