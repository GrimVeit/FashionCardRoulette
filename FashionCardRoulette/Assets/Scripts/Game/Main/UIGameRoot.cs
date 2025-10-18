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

    [Header("Character")]
    [SerializeField] private ShopWardrobePanel_Game shopWardrobePanel;

    [SerializeField] private ShopTypePanel_Game shopTypePanel;
    [SerializeField] private ShopPanel_Game shopPanel;

    [SerializeField] private WardrobeTypePanel_Game wardrobeTypePanel;

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

        shopTypePanel.Initialize();
        shopPanel.Initialize();

        wardrobeTypePanel.Initialize();
    }

    public void Activate()
    {
        chooseGenderPanel.OnClickToContinue += HandleClickToContinue_ChooseGender;

        chooseCharacterPanel.OnClickToContinue += HandleClickToContinue_ChooseCharacter;
        chooseCharacterPanel.OnClickToBack += HandleClickToBack_ChooseCharacter;

        exitPanel.OnClickToExit += HandleClickToExit_Exit;
        mainPanel.OnClickToCharacter += HandleClickToCharacter_Main;

        shopWardrobePanel.OnClickToBack += HandleClickToBack_ShopWardrobe;
        shopWardrobePanel.OnClickToShop += HandleClickToShop_ShopWardrobe;
        shopWardrobePanel.OnClickToWardrobe += HandleClickToWardrobe_ShopWardrobe;

        shopTypePanel.OnClickToBack += HandleClickToBack_ShopType;
        shopPanel.OnClickToBack += HandleClickToBack_Shop;

        wardrobeTypePanel.OnClickToBack += HandleClickToBack_WardrobeType;
    }


    public void Deactivate()
    {
        chooseGenderPanel.OnClickToContinue -= HandleClickToContinue_ChooseGender;

        chooseCharacterPanel.OnClickToContinue -= HandleClickToContinue_ChooseCharacter;
        chooseCharacterPanel.OnClickToBack -= HandleClickToBack_ChooseCharacter;

        exitPanel.OnClickToExit -= HandleClickToExit_Exit;
        mainPanel.OnClickToCharacter -= HandleClickToCharacter_Main;

        shopWardrobePanel.OnClickToBack -= HandleClickToBack_ShopWardrobe;
        shopWardrobePanel.OnClickToShop -= HandleClickToShop_ShopWardrobe;
        shopWardrobePanel.OnClickToWardrobe -= HandleClickToWardrobe_ShopWardrobe;

        shopTypePanel.OnClickToBack -= HandleClickToBack_ShopType;
        shopPanel.OnClickToBack -= HandleClickToBack_Shop;

        wardrobeTypePanel.OnClickToBack -= HandleClickToBack_WardrobeType;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);

        CloseChooseGenderPanel();
        CloseChooseCharacterPanel();
        CloseMainPanel();
        CloseCoinsPanel();
        CloseExitPanel();

        CloseShopWardrobePanel();
        CloseShopTypePanel();
        CloseShopPanel();
    }

    public void Dispose()
    {
        chooseGenderPanel.Dispose();
        chooseCharacterPanel.Dispose();

        mainPanel.Dispose();
        coinsPanel.Dispose();
        exitPanel.Dispose();

        shopWardrobePanel.Dispose();

        shopTypePanel.Dispose();
        shopPanel.Dispose();

        wardrobeTypePanel.Dispose();
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






    public void OpenShopTypePanel()
    {
        if(shopTypePanel.IsActive) return;

        OpenOtherPanel(shopTypePanel);
    }

    public void CloseShopTypePanel()
    {
        if(!shopTypePanel.IsActive) return;

        CloseOtherPanel(shopTypePanel);
    }







    public void OpenShopPanel()
    {
        if(shopPanel.IsActive) return;

        OpenOtherPanel(shopPanel);
    }

    public void CloseShopPanel()
    {
        if(!shopPanel.IsActive) return;

        CloseOtherPanel(shopPanel);
    }







    public void OpenWardrobeTypePanel()
    {
        if(wardrobeTypePanel.IsActive) return;

        OpenOtherPanel(wardrobeTypePanel);
    }

    public void CloseWardrobeTypePanel()
    {
        if(!wardrobeTypePanel.IsActive) return;

        CloseOtherPanel(wardrobeTypePanel);
    }

    #endregion


    #region Output

    public event Action OnClickToContinue_ChooseGender;

    private void HandleClickToContinue_ChooseGender()
    {
        OnClickToContinue_ChooseGender?.Invoke();
    }

    //------------------------------CHOOSE_CHARACTER--------------------------//

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

    //----------------------------------MAIN----------------------//

    public event Action OnClickToCharacter_Main;

    private void HandleClickToCharacter_Main()
    {
        OnClickToCharacter_Main?.Invoke();
    }

    //------------------------------EXIT--------------------------//

    public event Action OnClickToExit_Exit;

    private void HandleClickToExit_Exit()
    {
        OnClickToExit_Exit?.Invoke();
    }

    //----------------------------SHOP_WARDROBE--------------------------//

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

    //-------------------------SHOP_TYPE----------------------------//

    public event Action OnClickToBack_ShopType;

    private void HandleClickToBack_ShopType()
    {
        OnClickToBack_ShopType?.Invoke();
    }

    //-------------------------SHOP----------------------------//

    public event Action OnClickToBack_Shop;

    private void HandleClickToBack_Shop()
    {
        OnClickToBack_Shop?.Invoke();
    }

    //-------------------------WARDROBE_TYPE----------------------------//

    public event Action OnClickToBack_WardrobeType;

    private void HandleClickToBack_WardrobeType()
    {
        OnClickToBack_WardrobeType?.Invoke();
    }

    #endregion
}
