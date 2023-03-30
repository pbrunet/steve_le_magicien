using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public LevelInfo firstScene;
    public LevelInfo startAreaScene;
    public GameObject[] systemPrefabs;

    private List<GameObject> instancedSystemPrefabs = new List<GameObject>();
    private string currentLevelName = "";
    private List<AsyncOperation> loadOperations = new List<AsyncOperation>();

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
            TransitionToLevel(firstScene);
        } else
        {
            string sceneName = SceneManager.GetSceneAt(1).name;
            LevelInfo info = new LevelInfo();
            info.sceneName = sceneName;
            info.friendlyName = "Custom Level";
            SceneManager.UnloadScene(sceneName);

            TransitionToLevel(info);
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
            SceneManager.SetActiveScene(SceneManager.GetSceneByPath(currentLevelName));
        }

    }

    public void TransitionToLevel(LevelInfo levelName)
    {
        if (currentLevelName != "")
        {
            UnLoadLevel(currentLevelName);
        }
        LoadLevel(levelName.sceneName);
    }

    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
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
        TransitionToLevel(startAreaScene);
    }

    public void FadeOutCompleted(bool isFadeOut)
    {
        //if (!isFadeOut)
        //{
        //    UnLoadLevel(currentLevelName);
        //}
    }
}
