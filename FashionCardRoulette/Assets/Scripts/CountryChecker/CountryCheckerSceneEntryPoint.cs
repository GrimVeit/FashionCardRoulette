using System;
using UnityEngine;
using System.Collections.Generic;

public class CountryCheckerSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private UICountryCheckerSceneRoot sceneRootPrefab;

    private UICountryCheckerSceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private GeoLocationPresenter geoLocationPresenter;
    private InternetPresenter internetPresenter;

    private TimeServicePresenter timeServicePresenter;

    private List<string> allCountries = new() { "AT", "AU", "DE"}; 
    private string currentCountry;

    public void Run(UIRootView uIRootView)
    {
        //Debug.Log("OPEN COUNTRY CHECKER SCENE");

        sceneRoot = sceneRootPrefab;
        //uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        geoLocationPresenter = new GeoLocationPresenter(new GeoLocationModel());

        internetPresenter = new InternetPresenter(new InternetModel(), viewContainer.GetView<InternetView>());
        internetPresenter.Initialize();

        timeServicePresenter = new TimeServicePresenter(new TimeServiceModel());
        timeServicePresenter.Initialize();

        ActivateActions();

        timeServicePresenter.CheckDateTime();
    }

    public void Dispose()
    {
        DeactivateActions();

        internetPresenter?.Dispose();
    }

    private void ActivateActions()
    {
        timeServicePresenter.OnEventNotYet += TransitionToMainMenu;
        timeServicePresenter.OnEventReached += OnEventsReached;

        internetPresenter.OnInternetUnavailable += TransitionToMainMenu;
        internetPresenter.OnInternetAvailable += OnInternetAvailable;

        geoLocationPresenter.OnErrorGetCountry += TransitionToMainMenu;
        geoLocationPresenter.OnGetCountry += ActivateSceneInCountry;
    }

    private void DeactivateActions()
    {
        internetPresenter.OnInternetUnavailable -= TransitionToMainMenu;
        internetPresenter.OnInternetAvailable -= OnInternetAvailable;

        geoLocationPresenter.OnErrorGetCountry -= TransitionToMainMenu;
        geoLocationPresenter.OnGetCountry -= ActivateSceneInCountry;
    }

    private void OnEventsReached()
    {
        internetPresenter.StartCheckConnection();
    }

    private void OnInternetAvailable()
    {
        //Debug.Log("INTERNET CONNECTION = TRUE");
        geoLocationPresenter.GetUserCountry();
    }

    private void ActivateSceneInCountry(string country)
    {
        currentCountry = country;

        if (allCountries.Contains(currentCountry))
        {
            //Debug.Log("GOOD COUNTRY = TRUE");
            TransitionToOther();
        }
        else
        {
            //Debug.Log("GOOD COUNTRY = FALSE");
            TransitionToMainMenu();
        }
    }

    //private void CheckCountry(List<string> countries)
    //{
    //    //if (countries.Contains(currentCountry))
    //    //{
    //    //    //Debug.Log("GOOD COUNTRY = TRUE");
    //    //    TransitionToOther();
    //    //}
    //    //else
    //    //{
    //    //    Debug.Log("GOOD COUNTRY = FALSE");
    //    //    TransitionToMainMenu();
    //    //}
    //}

    #region Input

    public event Action GoToMainMenu;
    public event Action GoToOther;

    private void TransitionToMainMenu()
    {
        Dispose();
        Debug.Log("NO GOOD");
        GoToMainMenu?.Invoke();
    }

    private void TransitionToOther()
    {
        Dispose();
        Debug.Log("GOOD");
        GoToOther?.Invoke();
    }

    #endregion
}
