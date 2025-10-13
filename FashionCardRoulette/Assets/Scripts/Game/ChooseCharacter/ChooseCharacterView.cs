using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class ChooseCharacterView : View
{
    [SerializeField] private ChooseCharacter chooseCharacter_Man;
    [SerializeField] private ChooseCharacter chooseCharacter_Woman;
    [SerializeField] private Button buttonSubmit;
    [SerializeField] private Transform transformContent;
    
    private List<ChooseCharacter> chooseCharacters = new();

    public void Initialize()
    {
        buttonSubmit.onClick.AddListener(() => OnSubmitChoice?.Invoke());
    }

    public void Dispose()
    {
        buttonSubmit.onClick.RemoveListener(() => OnSubmitChoice?.Invoke());
    }

    public void SetCharacters(List<PersonZero> personZeros)
    {
        if(chooseCharacters != null)
        {
            for (int i = chooseCharacters.Count - 1; i >= 0; i--)
            {
                if (chooseCharacters[i] != null)
                {
                    chooseCharacters[i].OnChooseCharacter -= Choose;
                    chooseCharacters[i].Dispose();

                    Destroy(chooseCharacters[i].gameObject);
                    chooseCharacters.RemoveAt(i);
                }
            }
        }

        for (int i = 0; i < personZeros.Count; i++)
        {
            ChooseCharacter chooseCharacter;

            if (personZeros[i].Gender == Gender.Man)
            {
                chooseCharacter = Instantiate(chooseCharacter_Man, transformContent);
            }
            else
            {
                chooseCharacter = Instantiate(chooseCharacter_Woman, transformContent);
            }

            chooseCharacter.OnChooseCharacter += Choose;
            chooseCharacter.SetData(personZeros[i].ID, personZeros[i].Sprite);
            chooseCharacter.Initialize();
            chooseCharacters.Add(chooseCharacter);
        }
    }

    public void Activate(Gender gender, int id)
    {
        chooseCharacters.FirstOrDefault(data => data.Id == id && data.Gender == gender).Activate();
    }

    public void Deactivate(Gender gender, int id)
    {
        chooseCharacters.FirstOrDefault(data => data.Id == id && data.Gender == gender).Deactivate();
    }

    #region Output

    public event Action OnSubmitChoice;
    public event Action<Gender, int> OnChoose;

    private void Choose(Gender gender, int id)
    {
        OnChoose?.Invoke(gender, id);
    }

    #endregion
}
