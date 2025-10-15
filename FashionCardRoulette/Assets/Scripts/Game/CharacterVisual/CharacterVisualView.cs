using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterVisualView : View
{
    [SerializeField] private CharacterVisuals visuals;
    [SerializeField] private Transform transformCharacter;
    [SerializeField] private Image imageCharacter;

    private CharacterVisual _currentCharacterVisual;

    public void SetCharacter(Gender gender, int id)
    {
        _currentCharacterVisual = visuals.GetCharacterVisual(gender, id);

        if(_currentCharacterVisual == null)
        {
            Debug.LogWarning($"Not found CharacterVisual with Gender - {gender} and Id - {id}");
            return;
        }

        transformCharacter.localPosition = _currentCharacterVisual.Position;
        transformCharacter.localScale = _currentCharacterVisual.Scale;
        imageCharacter.sprite = _currentCharacterVisual.Sprite;
    }
}

[System.Serializable]
public class CharacterVisuals
{
    [SerializeField] private List<CharacterVisual> characterVisuals = new();

    public CharacterVisual GetCharacterVisual(Gender gender, int id)
    {
        return characterVisuals.FirstOrDefault(data => data.Id == id && data.Gender == gender);
    }
}

[System.Serializable]
public class CharacterVisual
{
    [SerializeField] private Gender gender;
    [SerializeField] private int id;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 scale;

    public Gender Gender => gender;
    public int Id => id;
    public Sprite Sprite => sprite;
    public Vector3 Position => position;
    public Vector3 Scale => scale;
}
