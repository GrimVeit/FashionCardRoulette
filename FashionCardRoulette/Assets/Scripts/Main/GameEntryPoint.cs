using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    private static GameEntryPoint instance;
    private UIRootView rootView;
    private Coroutines coroutines;
    public GameEntryPoint()
    {
        coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIRootView>("UIRootView");
        rootView = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(rootView.gameObject);

    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Autorun()
    {
        GlobalGameSettings();

        instance = new GameEntryPoint();
        instance.Run();

    }

    private static void GlobalGameSettings()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Run()  
    {
        coroutines.StartCoroutine(LoadAndStartCheck());
    }

    private IEnumerator LoadAndStartCheck()
    {
        yield return LoadScene(Scenes.CHECKER);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<CountryCheckerSceneEntryPoint>();

        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());
        sceneEntryPoint.GoToOther += () => coroutines.StartCoroutine(LoadAndStartOther());
    }

    private IEnumerator LoadAndStartOther()
    {
        yield return LoadScene(Scenes.OTHER);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<OtherSceneEntryPoint>();

        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());
    }

    private IEnumerator LoadAndStartMainMenu()
    {
        yield return rootView.ShowLoadingScreen(0);

        yield return new WaitForSeconds(0.4f);

        yield return LoadScene(Scenes.MAIN_MENU);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<MenuEntryPoint>();

        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnClickToGame += () => coroutines.StartCoroutine(LoadAndStartGame());


        yield return rootView.HideLoadingScreen(0);
    }

    private IEnumerator LoadAndStartGame()
    {
        yield return rootView.ShowLoadingScreen(1);

        yield return new WaitForSeconds(0.4f);

        yield return LoadScene(Scenes.GAME);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<GameSceneEntryPoint>();

        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnClickToMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());
        sceneEntryPoint.OnClickToGame += () => coroutines.StartCoroutine(LoadAndStartGame());

        yield return rootView.HideLoadingScreen(1);
    }

    private IEnumerator LoadScene(string scene)
    {
        Debug.Log("Загрузка сцены - " + scene);
        yield return SceneManager.LoadSceneAsync(scene);
    }
}
