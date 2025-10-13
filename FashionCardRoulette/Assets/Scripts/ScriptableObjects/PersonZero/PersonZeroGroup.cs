using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PersonZeroGroup", menuName = "Game/Person/ZeroGroup")]
public class PersonZeroGroup : ScriptableObject
{
    public List<PersonZero> Persons = new();

    public List<PersonZero> GetPersonsByGender(Gender gender)
    {
        return Persons.Where(data => data.Gender == gender).ToList();
    }
}
