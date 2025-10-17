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
    private ShopClothesVisualPresenter shopClothesVisualPresenter;

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

        chooseShopClothesPresenter = new ChooseShopClothesPresenter(new ChooseShopClothesModel(chooseGenderClothesPresenter), viewContainer.GetView<ChooseShopClothesView>());
        shopClothesVisualPresenter = new ShopClothesVisualPresenter(new ShopClothesVisualModel(storeClothesPresenter), viewContainer.GetView<ShopClothesVisualView>());

        stateMachine = new StateMachine_Game(sceneRoot);

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

        storeClothesPresenter.Initialize();
        chooseGenderClothesPresenter.Initialize();
        characterVisualPresenter.Initialize();

        chooseShopClothesPresenter.Initialize();
        shopClothesVisualPresenter.Initialize();

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

        storeClothesPresenter?.Dispose();
        chooseGenderClothesPresenter?.Dispose();
        characterVisualPresenter?.Dispose();

        chooseShopClothesPresenter?.Dispose();
        shopClothesVisualPresenter?.Dispose();

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
