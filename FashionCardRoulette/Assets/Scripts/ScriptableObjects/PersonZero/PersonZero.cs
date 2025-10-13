using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "PersonZero", menuName = "Game/Person/Zero")]
public class PersonZero : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Gender gender;
    [SerializeField] private Sprite sprite;

    public int ID => id;
    public Gender Gender => gender;
    public Sprite Sprite => sprite;
}