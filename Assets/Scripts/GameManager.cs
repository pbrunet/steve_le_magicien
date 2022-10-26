using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        UNKNOWN,
        TITLE_SCREEN,
        START_AREA,
        INGAME,
        END_LEVEL
    }

    public GameObject[] systemPrefabs;
    public EventGameState OnGameStateChanged;

    private List<GameObject> instancedSystemPrefabs = new List<GameObject>();
    private string currentLevelName = "";
    private List<AsyncOperation> loadOperations = new List<AsyncOperation>();
    private GameState currentGameState = GameState.UNKNOWN;

    public GameState CurrentGameState { get { return currentGameState; } }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        InstanciateSystemPrefabs();

        if (UIManager.Instance.OnNewSceneFadeOutCompleted == null)
        {
            UIManager.Instance.OnNewSceneFadeOutCompleted = new EventFadeOutCompleted();
        }

        UIManager.Instance.OnNewSceneFadeOutCompleted.AddListener(FadeOutCompleted);

        if (SceneManager.sceneCount < 2)
        {
            TransitionToLevel("TitleScreen");
        } else
        {
            string sceneName = SceneManager.GetSceneAt(1).name;
            SceneManager.UnloadScene(sceneName);

            TransitionToLevel(sceneName);
        }
    }

    protected override void OnDestroy()
    {
        DestroySystemPrefabs();
        base.OnDestroy();
    }

    private void InstanciateSystemPrefabs()
    {
        for (int i = 0; i < systemPrefabs.Length; i++)
        {
            instancedSystemPrefabs.Add(Instantiate(systemPrefabs[i]));
        }
    }

    private void DestroySystemPrefabs()
    {
        foreach (GameObject obj in instancedSystemPrefabs)
        {
            Destroy(obj);
        }
        instancedSystemPrefabs.Clear();
    }

    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        loadOperations.Remove(ao);

        if (loadOperations.Count == 0)
        {
            // To keep prefabs instance in this scene and auto remove them at the end
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentLevelName));
            if(currentLevelName == "TitleScreen")
            {
                UpdateGameState(GameState.TITLE_SCREEN);
            }
            else
            {
                UpdateGameState(GameState.INGAME);
            }
        }

    }

    public void TransitionToLevel(string levelName)
    {
        if (currentLevelName != "")
        {
            UnLoadLevel(currentLevelName);
        }
        LoadLevel(levelName);
    }

    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
    }

    private void UpdateGameState(GameState state)
    {
        GameState oldState = currentGameState;
        currentGameState = state;

        switch (currentGameState)
        {
            case GameState.INGAME:
                Time.timeScale = 1.0f;
                break;
            case GameState.TITLE_SCREEN:
                Time.timeScale = 1.0f;
                break;
            case GameState.START_AREA:
                TransitionToLevel("StartArea");
                Time.timeScale = 1.0f;
                break;
            case GameState.END_LEVEL:
                Time.timeScale = 0.0f;
                break;
            default:
                break;
        }
        OnGameStateChanged.Invoke(state, oldState);
    }

    private void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Can't load level " + levelName);
            return;
        }
        loadOperations.Add(ao);
        ao.completed += OnLoadOperationComplete;
        currentLevelName = levelName;
    }

    private void UnLoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Can't unload level " + levelName);
            return;
        }
        ao.completed += OnUnloadOperationComplete;
        currentLevelName = "";
    }

    public void EndLevel()
    {
        UpdateGameState(GameState.END_LEVEL);
    }

    public void Quit()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    public void Restart()
    {
        UpdateGameState(GameState.START_AREA);
    }

    public void FadeOutCompleted(bool isFadeOut)
    {
        //if (!isFadeOut)
        //{
        //    UnLoadLevel(currentLevelName);
        //}
    }
}
