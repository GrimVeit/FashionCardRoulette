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
    [SerializeField] private NumbersSelectionPanel_Game numberSelectionPanel;
    [SerializeField] private RoulettePanel_Game roulettePanel;
    [SerializeField] private NumberPanel_Game numberPanel;
    [SerializeField] private TasksPanel_Game tasksPanel;
    [SerializeField] private TaskDescriptionPanel_Game taskDescriptionPanel;
    [SerializeField] private MoreCoinsPanel_Game moreCoinsPanel;
    [SerializeField] private CoinsPanel_Game coinsPanel;
    [SerializeField] private ExitPanel_Game exitPanel;
    [SerializeField] private ResultsPanel_Game resultPanel;

    [Header("Character")]
    [SerializeField] private ShopWardrobePanel_Game shopWardrobePanel;

    [SerializeField] private ShopTypePanel_Game shopTypePanel;
    [SerializeField] private ShopPanel_Game shopPanel;
    [SerializeField] private PaycheckPanel_Game paycheckPanel;
    [SerializeField] private NotCoinsPanel_Game notCoinsPanel;

    [SerializeField] private WardrobeTypePanel_Game wardrobeTypePanel;
    [SerializeField] private WardrobePanel_Game wardrobePanel;
    [SerializeField] private WardrobeFitClothesPanel_Game wardrobeFitClothesPanel;

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
        numberSelectionPanel.Initialize();
        roulettePanel.Initialize();
        numberPanel.Initialize();
        tasksPanel.Initialize();
        taskDescriptionPanel.Initialize();
        moreCoinsPanel.Initialize();
        coinsPanel.Initialize();
        exitPanel.Initialize();
        resultPanel.Initialize();

        shopWardrobePanel.Initialize();

        shopTypePanel.Initialize();
        shopPanel.Initialize();
        paycheckPanel.Initialize();
        notCoinsPanel.Initialize();

        wardrobeTypePanel.Initialize();
        wardrobePanel.Initialize();
        wardrobeFitClothesPanel.Initialize();
    }

    public void Activate()
    {
        chooseGenderPanel.OnClickToContinue += HandleClickToContinue_ChooseGender;

        chooseCharacterPanel.OnClickToContinue += HandleClickToContinue_ChooseCharacter;
        chooseCharacterPanel.OnClickToBack += HandleClickToBack_ChooseCharacter;

        exitPanel.OnClickToExit += HandleClickToExit_Exit;
        mainPanel.OnClickToCharacter += HandleClickToCharacter_Main;
        mainPanel.OnClickToSpin += HandleClickToSpin_Main;
        taskDescriptionPanel.OnClickToBack += HandleClickToBack_TaskDescription;

        resultPanel.OnClickToContinue += HandleClickToContinue_Result;

        shopWardrobePanel.OnClickToBack += HandleClickToBack_ShopWardrobe;
        shopWardrobePanel.OnClickToShop += HandleClickToShop_ShopWardrobe;
        shopWardrobePanel.OnClickToWardrobe += HandleClickToWardrobe_ShopWardrobe;

        shopTypePanel.OnClickToBack += HandleClickToBack_ShopType;
        shopPanel.OnClickToBack += HandleClickToBack_Shop;

        wardrobeTypePanel.OnClickToBack += HandleClickToBack_WardrobeType;
        wardrobePanel.OnClickToBack += HandleClickToBack_Wardrobe;
    }


    public void Deactivate()
    {
        chooseGenderPanel.OnClickToContinue -= HandleClickToContinue_ChooseGender;

        chooseCharacterPanel.OnClickToContinue -= HandleClickToContinue_ChooseCharacter;
        chooseCharacterPanel.OnClickToBack -= HandleClickToBack_ChooseCharacter;

        exitPanel.OnClickToExit -= HandleClickToExit_Exit;
        mainPanel.OnClickToCharacter -= HandleClickToCharacter_Main;
        mainPanel.OnClickToSpin -= HandleClickToSpin_Main;
        taskDescriptionPanel.OnClickToBack -= HandleClickToBack_TaskDescription;

        resultPanel.OnClickToContinue -= HandleClickToContinue_Result;

        shopWardrobePanel.OnClickToBack -= HandleClickToBack_ShopWardrobe;
        shopWardrobePanel.OnClickToShop -= HandleClickToShop_ShopWardrobe;
        shopWardrobePanel.OnClickToWardrobe -= HandleClickToWardrobe_ShopWardrobe;

        shopTypePanel.OnClickToBack -= HandleClickToBack_ShopType;
        shopPanel.OnClickToBack -= HandleClickToBack_Shop;

        wardrobeTypePanel.OnClickToBack -= HandleClickToBack_WardrobeType;
        wardrobePanel.OnClickToBack -= HandleClickToBack_Wardrobe;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);

        CloseChooseGenderPanel();
        CloseChooseCharacterPanel();
        CloseMainPanel();
        CloseNumbersSelectionPanel();
        CloseRoulettePanel();
        CloseNumberPanel();
        CloseTasksPanel();
        CloseTaskDescriptionPanel();
        CloseMoreCoinsPanel();
        CloseCoinsPanel();
        CloseExitPanel();

        CloseShopWardrobePanel();
        CloseShopTypePanel();
        CloseShopPanel();
        ClosePaycheckPanel();
        CloseNotCoinsPanel();

        CloseWardrobeTypePanel();
        CloseWardrobePanel();
        CloseWardrobeFitClothesPanel();
    }

    public void Dispose()
    {
        chooseGenderPanel.Dispose();
        chooseCharacterPanel.Dispose();

        mainPanel.Dispose();
        numberSelectionPanel.Dispose();
        roulettePanel.Dispose();
        numberPanel.Dispose();
        tasksPanel.Dispose();
        taskDescriptionPanel.Dispose();
        moreCoinsPanel.Dispose();
        coinsPanel.Dispose();
        exitPanel.Dispose();
        resultPanel.Dispose();

        shopWardrobePanel.Dispose();

        shopTypePanel.Dispose();
        shopPanel.Dispose();
        paycheckPanel.Dispose();
        notCoinsPanel.Dispose();

        wardrobeTypePanel.Dispose();
        wardrobePanel.Dispose();
        wardrobeFitClothesPanel.Dispose();
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
        Debug.Log("IS OPEN - " + mainPanel.IsActive);

        if(mainPanel.IsActive) return;

        OpenOtherPanel(mainPanel);
    }

    public void CloseMainPanel()
    {
        if (!mainPanel.IsActive) return;

        CloseOtherPanel(mainPanel);
    }








    public void OpenNumbersSelectionPanel()
    {
        if(numberSelectionPanel.IsActive) return;

        OpenOtherPanel(numberSelectionPanel);
    }

    public void CloseNumbersSelectionPanel()
    {
        if(!numberSelectionPanel.IsActive) return;

        CloseOtherPanel(numberSelectionPanel);
    }








    public void OpenRoulettePanel()
    {
        if (roulettePanel.IsActive) return;

        OpenOtherPanel(roulettePanel);
    }

    public void CloseRoulettePanel()
    {
        if(!roulettePanel.IsActive) return;

        CloseOtherPanel(roulettePanel);
    }







    public void OpenNumberPanel()
    {
        if (numberPanel.IsActive) return;

        OpenOtherPanel(numberPanel);
    }

    public void CloseNumberPanel()
    {
        if (!numberPanel.IsActive) return;

        CloseOtherPanel(numberPanel);
    }






    public void OpenTasksPanel()
    {
        if(tasksPanel.IsActive) return;

        OpenOtherPanel(tasksPanel);
    }

    public void CloseTasksPanel()
    {
        if(!tasksPanel.IsActive) return;

        CloseOtherPanel(tasksPanel);
    }






    public void OpenTaskDescriptionPanel()
    {
        if (taskDescriptionPanel.IsActive) return;

        OpenOtherPanel(taskDescriptionPanel);
    }

    public void CloseTaskDescriptionPanel()
    {
        if (!taskDescriptionPanel.IsActive) return;

        CloseOtherPanel(taskDescriptionPanel);
    }







    public void OpenMoreCoinsPanel()
    {
        if(moreCoinsPanel.IsActive) return;

        OpenOtherPanel(moreCoinsPanel);
    }

    public void CloseMoreCoinsPanel()
    {
        if(!moreCoinsPanel.IsActive) return;

        CloseOtherPanel(moreCoinsPanel);
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







    public void OpenPaycheckPanel()
    {
        if(paycheckPanel.IsActive) return;

        OpenOtherPanel(paycheckPanel);
    }

    public void ClosePaycheckPanel()
    {
        if(!paycheckPanel.IsActive) return;

        CloseOtherPanel(paycheckPanel);
    }







    public void OpenNotCoinsPanel()
    {
        if(notCoinsPanel.IsActive) return;

        OpenOtherPanel(notCoinsPanel);
    }

    public void CloseNotCoinsPanel()
    {
        if(!notCoinsPanel.IsActive) return;

        CloseOtherPanel(notCoinsPanel);
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





    public void OpenWardrobePanel()
    {
        if(wardrobePanel.IsActive) return;

        OpenOtherPanel(wardrobePanel);
    }

    public void CloseWardrobePanel()
    {
        if(!wardrobePanel.IsActive) return;

        CloseOtherPanel(wardrobePanel);
    }






    public void OpenWardrobeFitClothesPanel()
    {
        if (wardrobeFitClothesPanel.IsActive) return;

        OpenOtherPanel(wardrobeFitClothesPanel);
    }

    public void CloseWardrobeFitClothesPanel()
    {
        if(!wardrobeFitClothesPanel.IsActive) return;

        CloseOtherPanel(wardrobeFitClothesPanel);
    }







    public void OpenResultPanel()
    {
        if(resultPanel.IsActive) return;

        OpenOtherPanel(resultPanel);
    }

    public void CloseResultPanel()
    {
        if(!resultPanel.IsActive) return;

        CloseOtherPanel(resultPanel);
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
    public event Action OnClickToSpin_Main;

    private void HandleClickToCharacter_Main()
    {
        OnClickToCharacter_Main?.Invoke();
    }

    private void HandleClickToSpin_Main()
    {
        OnClickToSpin_Main?.Invoke();
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

    //-------------------------WARDROBE----------------------------//

    public event Action OnClickToBack_Wardrobe;

    private void HandleClickToBack_Wardrobe()
    {
        OnClickToBack_Wardrobe?.Invoke();
    }




    //-------------------------TASK_DESCRIPTION----------------------------//

    public event Action OnClickToBack_TaskDescription;

    private void HandleClickToBack_TaskDescription()
    {
        OnClickToBack_TaskDescription?.Invoke();
    }



    //-------------------------RESULT----------------------------//

    public event Action OnClickToContinue_Result;

    private void HandleClickToContinue_Result()
    {
        OnClickToContinue_Result?.Invoke();
    }

    #endregion
}
