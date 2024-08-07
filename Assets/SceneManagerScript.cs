using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance;

    public static Action OnNewScene;
    public static Action newScene;
    

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
        OnNewScene += He;
       newScene += He;
       

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public void ResetScene()
    {
       var currentScene = SceneManager.GetActiveScene();
        
       if (currentScene != null)
        {
            SceneManager.LoadScene(currentScene.buildIndex);
            newScene.Invoke();
            
          
        }
    }
    
    public void LoadNewScene(string name)
    {
        
        SceneManager.LoadScene(name);
        
       
       
        


    }
    public void StartButton()
    {
        if (GameManager.instance.currentPaddle != null)
        {
            GameManager.instance.currentPaddle.SavePosition();
        }
        SceneManager.LoadScene("Level1");

        //for some reason after one round the the he is called twice
        OnNewScene?.Invoke();
    }
    
    public void LoadNextLevel()
    {
       SceneManager.LoadScene("Level" + GameManager.instance.currentLevel);
        OnNewScene?.Invoke();

    }
    void He()
    {
        Debug.Log("He");
    }
    
}
