using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameRoot : UIRoot
{
    [SerializeField] private ChooseGenderPanel_Game chooseGenderPanel;
    [SerializeField] private ChooseCharacterPanel_Game chooseCharacterPanel;

    private ISoundProvider _soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        chooseGenderPanel.Initialize();
        chooseCharacterPanel.Initialize();
    }

    public void Activate()
    {
        chooseGenderPanel.OnClickToContinue += HandleClickToContinue_ChooseGender;

        chooseCharacterPanel.OnClickToContinue += HandleClickToContinue_ChooseCharacter;
        chooseCharacterPanel.OnClickToBack += HandleClickToBack_ChooseCharacter;
    }


    public void Deactivate()
    {
        chooseGenderPanel.OnClickToContinue -= HandleClickToContinue_ChooseGender;

        chooseCharacterPanel.OnClickToContinue -= HandleClickToContinue_ChooseCharacter;
        chooseCharacterPanel.OnClickToBack -= HandleClickToBack_ChooseCharacter;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);

        CloseChooseGenderPanel();
        CloseChooseCharacterPanel();
    }

    public void Dispose()
    {
        chooseGenderPanel.Dispose();
        chooseCharacterPanel.Dispose();
    }

    #region Input

    public void OpenChooseGenderPanel()
    {
        if(chooseGenderPanel.IsActive) return;

        OpenOtherPanel(chooseGenderPanel);
    }

    public void CloseChooseGenderPanel()
    {
        if (!chooseGenderPanel.IsActive) return;

        CloseOtherPanel(chooseGenderPanel);
    }



    public void OpenChooseCharacterPanel()
    {
        if (chooseCharacterPanel.IsActive) return;

        OpenOtherPanel(chooseCharacterPanel);
    }

    public void CloseChooseCharacterPanel()
    {
        if (!chooseCharacterPanel.IsActive) return;

        CloseOtherPanel(chooseCharacterPanel);
    }

    #endregion


    #region Output

    public event Action OnClickToContinue_ChooseGender;

    private void HandleClickToContinue_ChooseGender()
    {
        OnClickToContinue_ChooseGender?.Invoke();
    }



    public event Action OnClickToContinue_ChooseCharacter;
    public event Action OnClickToBack_ChooseCharacter;

    private void HandleClickToContinue_ChooseCharacter()
    {
        OnClickToContinue_ChooseCharacter?.Invoke();
    }

    private void HandleClickToBack_ChooseCharacter()
    {
        OnClickToBack_ChooseCharacter?.Invoke();
    }


    #endregion
}
