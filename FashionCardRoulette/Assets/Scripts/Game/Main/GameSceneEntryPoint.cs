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
    private ParticleEffectPresenter particleEffectPresenter;
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

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());

        storeCharacterPresenter = new StoreCharacterPresenter(new StoreCharacterModel(personZeroGroup));
        chooseGenderPresenter = new ChooseGenderPresenter(new ChooseGenderModel(storeCharacterPresenter), viewContainer.GetView<ChooseGenderView>());
        chooseCharacterPresenter = new ChooseCharacterPresenter(new ChooseCharacterModel(storeCharacterPresenter), viewContainer.GetView<ChooseCharacterView>());

        storeClothesPresenter = new StoreClothesPresenter(new StoreClothesModel(clothesAllGroup));
        chooseGenderClothesPresenter = new ChooseGenderClothesPresenter(new ChooseGenderClothesModel(chooseGenderPresenter));
        characterVisualPresenter = new CharacterVisualPresenter(new CharacterVisualModel(chooseCharacterPresenter), viewContainer.GetView<CharacterVisualView>());

        chooseShopClothesPresenter = new ChooseShopClothesPresenter(new ChooseShopClothesModel(chooseGenderClothesPresenter, storeClothesPresenter), viewContainer.GetView<ChooseShopClothesView>());
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
        taskVisualPresenter = new TaskVisualPresenter(new TaskVisualModel(taskConditionStoragePresenter, chooseNumberPresenter), viewContainer.GetView<TaskVisualView>());
        taskDescriptionPresenter = new TaskDescriptionPresenter(new TaskDescriptionModel(taskVisualPresenter, taskVisualPresenter, taskVisualPresenter, bankPresenter), viewContainer.GetView<TaskDescriptionView>());
        taskVisualMovePresenter = new TaskVisualMovePresenter(new TaskVisualMoveModel(), viewContainer.GetView<TaskVisualMoveView>());

        roulettePresenter = new RoulettePresenter(new RouletteModel(soundPresenter), viewContainer.GetView<RouletteView>());
        rouletteBallPresenter = new RouletteBallPresenter(new RouletteBallModel(soundPresenter), viewContainer.GetView<RouletteBallView>());
        rouletteStatePresenter = new RouletteStatePresenter(new RouletteStateModel(), viewContainer.GetView<RouletteStateView>());
        rouletteSpinCountPresenter = new RouletteSpinCountPresenter(new RouletteSpinCountModel(taskVisualPresenter), viewContainer.GetView<RouletteSpinCountView>());

        stateMachine = new StateMachine_Game
            (sceneRoot, 
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
            taskVisualMovePresenter);

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();

        soundPresenter.Initialize();
        particleEffectPresenter.Initialize();
        sceneRoot.Initialize();
        bankPresenter.Initialize();

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
    }

    private void DeactivateTransitions()
    {
        sceneRoot.OnClickToExit_Exit -= HandleClickToMenu;
    }

    private void Deactivate()
    {
        sceneRoot.Deactivate();
        soundPresenter?.Dispose();
    }

    private void Dispose()
    {
        DeactivateEvents();

        soundPresenter?.Dispose();
        sceneRoot.Dispose();
        particleEffectPresenter?.Dispose();
        bankPresenter?.Dispose();

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

        stateMachine?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Output


    public event Action OnClickToMenu;

    private void HandleClickToMenu()
    {
        Deactivate();

        OnClickToMenu?.Invoke();
    }

    #endregion
}
