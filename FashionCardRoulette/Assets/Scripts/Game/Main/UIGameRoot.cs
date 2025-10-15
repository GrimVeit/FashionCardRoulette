using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameRoot : UIRoot
{
    [Header("Start")]
    [SerializeField] private ChooseGenderPanel_Game chooseGenderPanel;
    [SerializeField] private ChooseCharacterPanel_Game chooseCharacterPanel;

    [Header("Main")]
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private CoinsPanel_Game coinsPanel;
    [SerializeField] private ExitPanel_Game exitPanel;
    [SerializeField] private ShopWardrobePanel_Game shopWardrobePanel;

    private ISoundProvider _soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        chooseGenderPanel.Initialize();
        chooseCharacterPanel.Initialize();

        mainPanel.Initialize();
        coinsPanel.Initialize();
        exitPanel.Initialize();
        shopWardrobePanel.Initialize();
    }

    public void Activate()
    {
        chooseGenderPanel.OnClickToContinue += HandleClickToContinue_ChooseGender;

        chooseCharacterPanel.OnClickToContinue += HandleClickToContinue_ChooseCharacter;
        chooseCharacterPanel.OnClickToBack += HandleClickToBack_ChooseCharacter;

        exitPanel.OnClickToExit += HandleClickToExit_Main;
        mainPanel.OnClickToCharacter += HandleClickToCharacter_Main;

        shopWardrobePanel.OnClickToBack += HandleClickToBack_ShopWardrobe;
        shopWardrobePanel.OnClickToShop += HandleClickToShop_ShopWardrobe;
        shopWardrobePanel.OnClickToWardrobe += HandleClickToWardrobe_ShopWardrobe;
    }


    public void Deactivate()
    {
        chooseGenderPanel.OnClickToContinue -= HandleClickToContinue_ChooseGender;

        chooseCharacterPanel.OnClickToContinue -= HandleClickToContinue_ChooseCharacter;
        chooseCharacterPanel.OnClickToBack -= HandleClickToBack_ChooseCharacter;

        exitPanel.OnClickToExit -= HandleClickToExit_Main;
        mainPanel.OnClickToCharacter -= HandleClickToCharacter_Main;

        shopWardrobePanel.OnClickToBack -= HandleClickToBack_ShopWardrobe;
        shopWardrobePanel.OnClickToShop -= HandleClickToShop_ShopWardrobe;
        shopWardrobePanel.OnClickToWardrobe -= HandleClickToWardrobe_ShopWardrobe;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);

        CloseChooseGenderPanel();
        CloseChooseCharacterPanel();
        CloseMainPanel();
        CloseCoinsPanel();
        CloseExitPanel();
        CloseShopWardrobePanel();
    }

    public void Dispose()
    {
        chooseGenderPanel.Dispose();
        chooseCharacterPanel.Dispose();

        mainPanel.Dispose();
        coinsPanel.Dispose();
        exitPanel.Dispose();
        shopWardrobePanel.Dispose();
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




    public void OpenMainPanel()
    {
        if(mainPanel.IsActive) return;

        OpenOtherPanel(mainPanel);
    }

    public void CloseMainPanel()
    {
        if (!mainPanel.IsActive) return;

        CloseOtherPanel(mainPanel);
    }






    public void OpenExitPanel()
    {
        if (exitPanel.IsActive) return;

        OpenOtherPanel(exitPanel);
    }

    public void CloseExitPanel()
    {
        if (!exitPanel.IsActive) return;

        CloseOtherPanel(exitPanel);
    }





    public void OpenCoinsPanel()
    {
        if(coinsPanel.IsActive) return;

        OpenOtherPanel(coinsPanel);
    }

    public void CloseCoinsPanel()
    {
        if(!coinsPanel.IsActive) return;

        CloseOtherPanel(coinsPanel);
    }




    public void OpenShopWardrobePanel()
    {
        if(shopWardrobePanel.IsActive) return;

        OpenOtherPanel(shopWardrobePanel);
    }

    public void CloseShopWardrobePanel()
    {
        if(!shopWardrobePanel.IsActive) return;

        CloseOtherPanel(shopWardrobePanel);
    }

    #endregion


    #region Output

    public event Action OnClickToContinue_ChooseGender;

    private void HandleClickToContinue_ChooseGender()
    {
        OnClickToContinue_ChooseGender?.Invoke();
    }

    //--------------------------------------------------------//

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

    //--------------------------------------------------------//

    public event Action OnClickToCharacter_Main;

    private void HandleClickToCharacter_Main()
    {
        OnClickToCharacter_Main?.Invoke();
    }

    //--------------------------------------------------------//

    public event Action OnClickToExit_Main;

    private void HandleClickToExit_Main()
    {
        OnClickToExit_Main?.Invoke();
    }

    //------------------------------------------------------//

    public event Action OnClickToBack_ShopWardrobe;
    public event Action OnClickToWardrobe_ShopWardrobe;
    public event Action OnClickToShop_ShopWardrobe;

    private void HandleClickToBack_ShopWardrobe()
    {
        OnClickToBack_ShopWardrobe?.Invoke();
    }

    private void HandleClickToWardrobe_ShopWardrobe()
    {
        OnClickToWardrobe_ShopWardrobe?.Invoke();
    }

    private void HandleClickToShop_ShopWardrobe()
    {
        OnClickToShop_ShopWardrobe?.Invoke();
    }

    #endregion
}
