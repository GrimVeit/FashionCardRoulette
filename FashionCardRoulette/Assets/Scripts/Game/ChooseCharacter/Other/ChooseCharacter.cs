using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCharacter : MonoBehaviour
{
    public int Id => toggle.Id;
    public Gender Gender => gender;

    [SerializeField] private ToggleCustom toggle;
    [SerializeField] private Image imageCharacter;
    [SerializeField] private Gender gender;

    public void Initialize()
    {
        toggle.OnChooseToggle += ChooseToggle;
    }

    public void Dispose()
    {
        toggle.OnChooseToggle -= ChooseToggle;
    }

    public void SetData(int id, Sprite sprite)
    {
        imageCharacter.sprite = sprite;
        toggle.SetData(id);
    }

    public void Activate()
    {
        toggle.Activate();
    }

    public void Deactivate()
    {
        toggle.Deactivate();
    }

    #region Output

    public event Action<Gender, int> OnChooseCharacter;

    private void ChooseToggle(int id)
    {
        OnChooseCharacter?.Invoke(gender, toggle.Id);
    }

    #endregion
}
