using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChooseGenderView : View
{
    [SerializeField] private List<ToggleCustom> toggleCustoms = new List<ToggleCustom>();
    [SerializeField] private Button buttonSubmit;

    public void Initialize()
    {
        toggleCustoms.ForEach(data => data.OnChooseToggle += ChooseToggle);

        buttonSubmit.onClick.AddListener(() => OnSubmit?.Invoke());
    }

    public void Dispose()
    {
        toggleCustoms.ForEach(data => data.OnChooseToggle -= ChooseToggle);

        buttonSubmit.onClick.RemoveListener(() => OnSubmit?.Invoke());
    }

    #region Output

    public event Action<int> OnChooseGender;
    public event Action OnSubmit;

    private void ChooseToggle(int id)
    {
        OnChooseGender?.Invoke(id);
    }

    #endregion

    #region Input

    public void Activate(int id)
    {
        toggleCustoms.FirstOrDefault(data => data.Id == id).Activate();
    }

    public void Deactivate(int id)
    {
        toggleCustoms.FirstOrDefault(data => data.Id == id).Deactivate();
    }

    #endregion
}
