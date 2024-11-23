using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance;

    public static Action OnNewScene;
    public static Action OnRestScene;

    private bool isResettingScene = false; // To track if a reset is already in progress

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnRestScene += He;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        Debug.Log($"Scene loaded: {scene.name}");
        

    }

    public void ResetScene()
    {
        if (isResettingScene) return; // Prevent multiple resets
        StartCoroutine(ResetSceneAsync());
        
    }

    private IEnumerator ResetSceneAsync()
    {
        isResettingScene = true;

        var currentScene = SceneManager.GetActiveScene();
        Debug.Log($"Resetting scene: {currentScene.name}");

        // Load the current scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(currentScene.buildIndex);
        asyncOperation.allowSceneActivation = true;

        // Wait for the scene to finish loading
        while (!asyncOperation.isDone)
        {
            Debug.Log($"Scene loading progress: {asyncOperation.progress * 100}%");
            yield return null; // Wait for the next frame
        }
        Debug.Log("Scene reset successfully!");
        OnRestScene?.Invoke();
        isResettingScene = false;
       
    }

    public void LoadNewScene(string name)
    {
        StartCoroutine(LoadSceneAsync(name));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        Debug.Log($"Loading new scene: {sceneName}");

        // Load the new scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = true;

        while (!asyncOperation.isDone)
        {
            Debug.Log($"Loading progress: {asyncOperation.progress * 100}%");
            yield return null;
        }
        
        Debug.Log($"Scene {sceneName} loaded successfully!");
    }

    public void LoadHomeScene()
    {
        StartCoroutine(LoadSceneAsync("HomeScreen"));
        GameManager.instance.currentLevel = 0;
    }

    public void StartButton()
    {
        if (GameManager.instance.currentPaddle != null)
        {
            GameManager.instance.currentPaddle.SavePosition();
        }
        StartCoroutine(LoadSceneAsync("Level1"));

        // Invoke event after scene load
        OnNewScene?.Invoke();
    }

    public void LoadNextLevel()
    {
        string nextLevel = "Level" + GameManager.instance.currentLevel;
        StartCoroutine(LoadSceneAsync(nextLevel));

        // Invoke event after scene load
        OnNewScene?.Invoke();
    }

    void He()
    {
        Debug.Log("He");
    }
}
