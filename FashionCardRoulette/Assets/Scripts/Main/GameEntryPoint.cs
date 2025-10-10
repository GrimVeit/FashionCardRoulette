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
        coroutines.StartCoroutine(LoadAndStartMainMenu());
    }

    private IEnumerator LoadAndStartMainMenu()
    {
        yield return rootView.ShowLoadingScreen(0);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MAIN_MENU);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<MenuEntryPoint>();

        sceneEntryPoint.Run(rootView);

        //sceneEntryPoint.OnGoToGame_Checkers += () => coroutines.StartCoroutine(LoadAndStartGameScene_Checkers());
        //sceneEntryPoint.OnGoToGame_Chess += () => coroutines.StartCoroutine(LoadAndStartGameScene_Chess());
        //sceneEntryPoint.OnGoToGame_Dominoes += () => coroutines.StartCoroutine(LoadAndStartGameScene_Dominoes());
        //sceneEntryPoint.OnGoToGame_Solitaire += () => coroutines.StartCoroutine(LoadAndStartGameScene_Solitaire());
        //sceneEntryPoint.OnGoToGame_Ludo += () => coroutines.StartCoroutine(LoadAndStartGameScene_Ludo());
        //sceneEntryPoint.OnGoToGame_Lotto += () => coroutines.StartCoroutine(LoadAndStartGameScene_Lotto());
        //sceneEntryPoint.OnGoToGame_Roulette += () => coroutines.StartCoroutine(LoadAndStartGameScene_Roulette());

        yield return rootView.HideLoadingScreen(0);
    }

    private IEnumerator LoadScene(string scene)
    {
        Debug.Log("Загрузка сцены - " + scene);
        yield return SceneManager.LoadSceneAsync(scene);
    }
}
