using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClothesGroup", menuName = "Game/Clothes/NewGroup")]
public class ClothesGroup : ScriptableObject
{
    public ClothesType ClothesType;
    public List<Clothes> Clothes = new();
}
