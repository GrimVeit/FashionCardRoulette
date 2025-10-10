using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class MenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIMainMenuRoot menuRootPrefab;

    private UIMainMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;

    private NicknamePresenter nicknamePresenter;
    private FirebaseAuthenticationPresenter firebaseAuthenticationPresenter;
    private FirebaseDatabasePresenter firebaseDatabasePresenter;
    private LeaderboardPresenter leaderboardPresenter;

    private StateMachine_Menu stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = menuRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
                FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
                DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

                soundPresenter = new SoundPresenter
                    (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
                    viewContainer.GetView<SoundView>());

                particleEffectPresenter = new ParticleEffectPresenter
                    (new ParticleEffectModel(),
                    viewContainer.GetView<ParticleEffectView>());

                bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());

                nicknamePresenter = new NicknamePresenter(new NicknameModel(PlayerPrefsKeys.NICKNAME, soundPresenter), viewContainer.GetView<NicknameView>());
                firebaseAuthenticationPresenter = new FirebaseAuthenticationPresenter(new FirebaseAuthenticationModel(firebaseAuth, soundPresenter), viewContainer.GetView<FirebaseAuthenticationView>());

                firebaseDatabasePresenter = new FirebaseDatabasePresenter(new FirebaseDatabaseModel(firebaseAuth, databaseReference, bankPresenter));
                leaderboardPresenter = new LeaderboardPresenter(new LeaderboardModel(firebaseDatabasePresenter), viewContainer.GetView<LeaderboardView>());

                stateMachine = new StateMachine_Menu
                (sceneRoot,
                nicknamePresenter,
                firebaseAuthenticationPresenter,
                firebaseDatabasePresenter);

                sceneRoot.SetSoundProvider(soundPresenter);
                sceneRoot.Activate();

                ActivateEvents();

                Debug.Log("LOL");

                soundPresenter.Initialize();
                Debug.Log("LOL");
                particleEffectPresenter.Initialize();
                Debug.Log("LOL");
                sceneRoot.Initialize();
                Debug.Log("LOL");
                bankPresenter.Initialize();
                Debug.Log("LOL");
                nicknamePresenter.Initialize();
                Debug.Log("LOL");
                leaderboardPresenter.Initialize();
                Debug.Log("LOL");
                firebaseAuthenticationPresenter.Initialize();
                Debug.Log("LOL");
                firebaseDatabasePresenter.Initialize();

                Debug.Log("LOL");

                stateMachine.Initialize();
            }
            else
            {
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
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

    }

    private void DeactivateTransitions()
    {

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
        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        bankPresenter?.Dispose();

        nicknamePresenter?.Dispose();
        leaderboardPresenter?.Dispose();
        firebaseAuthenticationPresenter?.Dispose();
        firebaseDatabasePresenter?.Dispose();

        stateMachine?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Output


    #endregion
}
