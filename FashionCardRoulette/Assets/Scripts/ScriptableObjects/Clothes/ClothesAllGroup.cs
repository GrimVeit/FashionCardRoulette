using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClothesAllGroup", menuName = "Game/Clothes/NewAll")]
public class ClothesAllGroup : ScriptableObject
{
    public List<ClothesGroup> Groups = new();
}
