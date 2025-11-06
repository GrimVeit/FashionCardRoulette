using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private NumberValues numberValues;
    [SerializeField] private ClothesAllGroup clothesAllGroup;
    [SerializeField] private PersonZeroGroup personZeroGroup;
    [SerializeField] private UIGameRoot menuRootPrefab;

    private UIGameRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;
    private BankTransactionHistoryPresenter bankTransactionHistoryPresenter;

    private ParticleEffectPresenter particleEffectPresenter;
    private ParticleEffectMaterialPresenter particleEffectMaterialPresenter;
    private SoundPresenter soundPresenter;

    private StoreCharacterPresenter storeCharacterPresenter;
    private ChooseGenderPresenter chooseGenderPresenter;
    private ChooseCharacterPresenter chooseCharacterPresenter;

    private StoreClothesPresenter storeClothesPresenter;
    private ChooseGenderClothesPresenter chooseGenderClothesPresenter;
    private CharacterVisualPresenter characterVisualPresenter;

    private ChooseShopClothesPresenter chooseShopClothesPresenter;
    private ShopClothesPresenter shopClothesPresenter;
    private ShopClothesVisualPresenter shopClothesVisualPresenter;

    private ChooseWardrobeClothesPresenter chooseWardrobeClothesPresenter;
    private WardrobeClothesVisualPresenter wardrobeClothesVisualPresenter;
    private WardrobeFitClothesPresenter wardrobeFitClothesPresenter;

    private ClothesVisualPresenter clothesVisualPresenter;

    private VideoPresenter videoPresenter;
    private ChooseNumberPresenter chooseNumberPresenter;
    private NumberTrashPresenter numberTrashPresenter;
    private TaskConditionStoragePresenter taskConditionStoragePresenter;
    private TaskVisualPresenter taskVisualPresenter;
    private TaskDescriptionPresenter taskDescriptionPresenter;
    private TaskVisualMovePresenter taskVisualMovePresenter;

    private RoulettePresenter roulettePresenter;
    private RouletteBallPresenter rouletteBallPresenter;
    private RouletteStatePresenter rouletteStatePresenter;
    private RouletteSpinCountPresenter rouletteSpinCountPresenter;



    private NumberSelectionPresenter numberSelectionPresenter;
    private NumberSelectionVisualPresenter numberSelectionVisualPresenter;
    private NumberBallVisualPresenter numberBallVisualPresenter;

    private StoreNumberPresenter storeNumberPresenter;
    private SectorArrowPresenter sectorArrowPresenter;

    private StateMachine_Game stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = menuRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter
                    (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
                    viewContainer.GetView<SoundView>());

        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());

        particleEffectMaterialPresenter = new ParticleEffectMaterialPresenter(new ParticleEffectMaterialModel(), viewContainer.GetView<ParticleEffectMaterialView>());

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        bankTransactionHistoryPresenter = new BankTransactionHistoryPresenter(new BankTransactionHistoryModel(bankPresenter), viewContainer.GetView<BankTransactionHistoryView>());

        storeCharacterPresenter = new StoreCharacterPresenter(new StoreCharacterModel(personZeroGroup));
        chooseGenderPresenter = new ChooseGenderPresenter(new ChooseGenderModel(storeCharacterPresenter, soundPresenter), viewContainer.GetView<ChooseGenderView>());
        chooseCharacterPresenter = new ChooseCharacterPresenter(new ChooseCharacterModel(storeCharacterPresenter, soundPresenter), viewContainer.GetView<ChooseCharacterView>());

        storeClothesPresenter = new StoreClothesPresenter(new StoreClothesModel(clothesAllGroup));
        chooseGenderClothesPresenter = new ChooseGenderClothesPresenter(new ChooseGenderClothesModel(chooseGenderPresenter));
        characterVisualPresenter = new CharacterVisualPresenter(new CharacterVisualModel(chooseCharacterPresenter), viewContainer.GetView<CharacterVisualView>());

        chooseShopClothesPresenter = new ChooseShopClothesPresenter(new ChooseShopClothesModel(chooseGenderClothesPresenter, storeClothesPresenter, soundPresenter), viewContainer.GetView<ChooseShopClothesView>());
        shopClothesPresenter = new ShopClothesPresenter(new ShopClothesModel(bankPresenter, storeClothesPresenter), viewContainer.GetView<ShopClothesView>());
        shopClothesVisualPresenter = new ShopClothesVisualPresenter(new ShopClothesVisualModel(storeClothesPresenter, shopClothesPresenter, shopClothesPresenter), viewContainer.GetView<ShopClothesVisualView>());

        chooseWardrobeClothesPresenter = new ChooseWardrobeClothesPresenter(new ChooseWardrobeClothesModel(chooseGenderClothesPresenter, storeClothesPresenter), viewContainer.GetView<ChooseWardrobeClothesView>());
        wardrobeClothesVisualPresenter = new WardrobeClothesVisualPresenter(new WardrobeClothesVisualModel(storeClothesPresenter, storeClothesPresenter), viewContainer.GetView<WardrobeClothesVisualView>());
        wardrobeFitClothesPresenter = new WardrobeFitClothesPresenter(new WardrobeFitClothesModel(storeClothesPresenter), viewContainer.GetView<WardrobeFitClothesView>());

        clothesVisualPresenter = new ClothesVisualPresenter(new ClothesVisualModel(chooseGenderClothesPresenter, storeClothesPresenter), viewContainer.GetView<ClothesVisualView>());


        videoPresenter = new VideoPresenter(new VideoModel(), viewContainer.GetView<VideoView>());
        chooseNumberPresenter = new ChooseNumberPresenter(new ChooseNumberModel(), viewContainer.GetView<ChooseNumberView>());
        numberTrashPresenter = new NumberTrashPresenter(new NumberTrashModel(), viewContainer.GetView<NumberTrashView>());
        taskConditionStoragePresenter = new TaskConditionStoragePresenter(new TaskConditionStorageModel());
        taskVisualPresenter = new TaskVisualPresenter(new TaskVisualModel(taskConditionStoragePresenter, chooseNumberPresenter, soundPresenter), viewContainer.GetView<TaskVisualView>());
        taskDescriptionPresenter = new TaskDescriptionPresenter(new TaskDescriptionModel(taskVisualPresenter, taskVisualPresenter, taskVisualPresenter, bankPresenter), viewContainer.GetView<TaskDescriptionView>());
        taskVisualMovePresenter = new TaskVisualMovePresenter(new TaskVisualMoveModel(), viewContainer.GetView<TaskVisualMoveView>());

        roulettePresenter = new RoulettePresenter(new RouletteModel(soundPresenter), viewContainer.GetView<RouletteView>());
        rouletteBallPresenter = new RouletteBallPresenter(new RouletteBallModel(soundPresenter), viewContainer.GetView<RouletteBallView>());
        rouletteStatePresenter = new RouletteStatePresenter(new RouletteStateModel(), viewContainer.GetView<RouletteStateView>());
        rouletteSpinCountPresenter = new RouletteSpinCountPresenter(new RouletteSpinCountModel(taskVisualPresenter), viewContainer.GetView<RouletteSpinCountView>());

        numberSelectionPresenter = new NumberSelectionPresenter(new NumberSelectionModel(), viewContainer.GetView<NumberSelectionView>());
        numberSelectionVisualPresenter = new NumberSelectionVisualPresenter(new NumberSelectionVisualModel(numberSelectionPresenter), viewContainer.GetView<NumberSelectionVisualView>());
        numberBallVisualPresenter = new NumberBallVisualPresenter(new NumberBallVisualModel(numberSelectionPresenter), viewContainer.GetView<NumberBallVisualView>());

        storeNumberPresenter = new StoreNumberPresenter(new StoreNumberModel(numberSelectionPresenter));
        sectorArrowPresenter = new SectorArrowPresenter(new SectorArrowModel(storeNumberPresenter), viewContainer.GetView<SectorArrowView>());

        stateMachine = new StateMachine_Game
            (sceneRoot, 
            particleEffectPresenter,
            storeClothesPresenter, 
            shopClothesPresenter, 
            wardrobeClothesVisualPresenter,
            numberValues,
            chooseNumberPresenter,
            chooseNumberPresenter,
            taskVisualPresenter,
            taskVisualPresenter,
            taskDescriptionPresenter,
            videoPresenter,
            roulettePresenter,
            rouletteBallPresenter,
            rouletteStatePresenter,
            numberTrashPresenter,
            rouletteSpinCountPresenter,
            taskVisualMovePresenter,
            taskVisualPresenter,
            numberSelectionPresenter,
            sectorArrowPresenter,
            sectorArrowPresenter,
            storeNumberPresenter);

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();

        soundPresenter.Initialize();
        particleEffectPresenter.Initialize();
        particleEffectMaterialPresenter.Initialize();
        particleEffectMaterialPresenter.Activate();
        sceneRoot.Initialize();
        bankPresenter.Initialize();
        bankTransactionHistoryPresenter.Initialize();

        storeCharacterPresenter.Initialize();
        chooseGenderPresenter.Initialize();
        chooseCharacterPresenter.Initialize();

        clothesVisualPresenter.Initialize();

        storeClothesPresenter.Initialize();
        chooseGenderClothesPresenter.Initialize();
        characterVisualPresenter.Initialize();

        chooseShopClothesPresenter.Initialize();
        shopClothesPresenter.Initialize();
        shopClothesVisualPresenter.Initialize();

        chooseWardrobeClothesPresenter.Initialize();
        wardrobeClothesVisualPresenter.Initialize();
        wardrobeFitClothesPresenter.Initialize();



        videoPresenter.Initialize();
        chooseNumberPresenter.Initialize();
        numberTrashPresenter.Initialize();
        taskConditionStoragePresenter.Initialize();
        taskDescriptionPresenter.Initialize();
        taskVisualMovePresenter.Initialize();
        taskVisualPresenter.Initialize();
        taskVisualPresenter.SetRandomTasks();


        roulettePresenter.Initialize();
        rouletteBallPresenter.Initialize();
        rouletteStatePresenter.Initialize();
        rouletteSpinCountPresenter.Initialize();


        numberSelectionVisualPresenter.Initialize();
        numberBallVisualPresenter.Initialize();
        numberSelectionPresenter.Initialize();

        storeNumberPresenter.Initialize();
        sectorArrowPresenter.Initialize();

        stateMachine.Initialize();
    }

    private void ActivateEvents()
    {
        ActivateTransitions();
    }

    private void DeactivateEvents()
    {
        DeactivateTransitions();
    }

    private void ActivateTransitions()
    {
        sceneRoot.OnClickToExit_Exit += HandleClickToMenu;
        sceneRoot.OnClickToExit_Finish += HandleClickToMenu;

        sceneRoot.OnClickToRestart_Finish += HandleClickToGame;
    }

    private void DeactivateTransitions()
    {
        sceneRoot.OnClickToExit_Exit -= HandleClickToMenu;
        sceneRoot.OnClickToExit_Finish -= HandleClickToMenu;

        sceneRoot.OnClickToRestart_Finish -= HandleClickToGame;
    }

    private void Deactivate()
    {
        particleEffectMaterialPresenter.Deactivate();

        sceneRoot.Deactivate();
        soundPresenter?.Dispose();
    }

    private void Dispose()
    {
        DeactivateEvents();

        soundPresenter?.Dispose();
        sceneRoot.Dispose();
        particleEffectPresenter?.Dispose();
        particleEffectMaterialPresenter?.Dispose();
        bankPresenter?.Dispose();
        bankTransactionHistoryPresenter?.Dispose();

        storeCharacterPresenter?.Dispose();
        chooseGenderPresenter?.Dispose();
        chooseCharacterPresenter?.Dispose();

        clothesVisualPresenter?.Dispose();

        storeClothesPresenter?.Dispose();
        chooseGenderClothesPresenter?.Dispose();
        characterVisualPresenter?.Dispose();

        chooseShopClothesPresenter?.Dispose();
        shopClothesPresenter?.Dispose();
        shopClothesVisualPresenter?.Dispose();

        chooseWardrobeClothesPresenter?.Dispose();
        wardrobeClothesVisualPresenter?.Dispose();
        wardrobeFitClothesPresenter?.Dispose();


        videoPresenter?.Dispose();
        chooseNumberPresenter?.Dispose();
        numberTrashPresenter?.Dispose();
        taskConditionStoragePresenter?.Dispose();
        taskDescriptionPresenter?.Dispose();
        taskVisualMovePresenter?.Dispose();
        taskVisualPresenter?.Dispose();


        roulettePresenter?.Dispose();
        rouletteBallPresenter?.Dispose();
        rouletteStatePresenter?.Dispose();
        rouletteSpinCountPresenter?.Dispose();



        numberSelectionPresenter?.Dispose();
        numberSelectionVisualPresenter?.Dispose();
        numberBallVisualPresenter?.Dispose();

        storeNumberPresenter?.Dispose();
        sectorArrowPresenter?.Dispose();

        stateMachine?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Output


    public event Action OnClickToMenu;
    public event Action OnClickToGame;

    private void HandleClickToMenu()
    {
        Deactivate();

        OnClickToMenu?.Invoke();
    }

    private void HandleClickToGame()
    {
        Deactivate();

        OnClickToGame?.Invoke();
    }

    #endregion
}
