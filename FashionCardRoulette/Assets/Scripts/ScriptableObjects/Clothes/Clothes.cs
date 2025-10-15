using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clothes", menuName = "Game/Clothes/New")]
public class Clothes : ScriptableObject
{
    [SerializeField] private ClothesType type;
    [SerializeField] private int id;
    [SerializeField] private string description;

    private ClothesData _data;

    public ClothesType ClothesType;
    public int Id => id;
    public string Description => description;
    public ClothesData Data => _data;


    public void SetData(ClothesData data)
    {
        _data = data;
    }
}