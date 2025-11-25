using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class OtherSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private UIOtherSceneRoot sceneRootPrefab;

    private UIOtherSceneRoot sceneRoot;
    private BankPresenter bankPresenter;
    private ViewContainer viewContainer;
    private WebViewPresenter otherWebViewPresenter;
    private FirebaseDatabasePresenter firebaseDatabaseRealtimePresenter;
 
    public void Run(UIRootView uIRootView)
    {
        //Debug.Log("OPEN OTHER SCENE");

        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
        DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        sceneRoot = Instantiate(sceneRootPrefab);
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        bankPresenter.Initialize();

        otherWebViewPresenter = new WebViewPresenter(new WebViewModel(), viewContainer.GetView<WebViewView>());
        otherWebViewPresenter.Initialize();

        firebaseDatabaseRealtimePresenter = new FirebaseDatabasePresenter(new FirebaseDatabaseModel(firebaseAuth, databaseReference, bankPresenter));
        firebaseDatabaseRealtimePresenter.Initialize();

        ActivateActions();

        firebaseDatabaseRealtimePresenter.GetLink();
    }

    private void ActivateActions()
    {
        firebaseDatabaseRealtimePresenter.OnGetLink += GetURLBd;
        firebaseDatabaseRealtimePresenter.OnErrorGetLink += GoToMainMenu;

        otherWebViewPresenter.OnGetLinkFromTitle += GetUrl;
        otherWebViewPresenter.OnFail += GoToMainMenu;
    }

    private void DeactivateActions()
    {
        firebaseDatabaseRealtimePresenter.OnGetLink -= GetURLBd;
        firebaseDatabaseRealtimePresenter.OnErrorGetLink -= GoToMainMenu;

        otherWebViewPresenter.OnGetLinkFromTitle -= GetUrl;
        otherWebViewPresenter.OnFail -= GoToMainMenu;
    }

    private void GetURLBd(string link)
    {
        otherWebViewPresenter.GetLinkInTitleFromURL(link);
    }

    private void GetUrl(string URL)
    {
        if (URL == null)
        {
            GoToMainMenu();
            return;
        }

        otherWebViewPresenter.SetURL(URL);
        otherWebViewPresenter.Load();
    }

    private void GoToMainMenu()
    {
        //Debug.Log("NO GOOD, OPEN MAIN MENU");
        OnGoToMainMenu?.Invoke();
    }

    private void OnDestroy()
    {
        DeactivateActions();

        otherWebViewPresenter.Dispose();
    }

    #region Input

    public event Action OnGoToMainMenu;

    #endregion
}
