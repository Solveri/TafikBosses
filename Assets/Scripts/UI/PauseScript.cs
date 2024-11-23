using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;

public class PauseScript : MonoBehaviour
{
    public static Action Paused;
    public static Action UnPaused;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        if (GameManager.instance.currentLevel/10 ==1)
        {
            textMeshProUGUI.text = "Boss";
        }
        textMeshProUGUI.text = "Level" + GameManager.instance.currentLevel;
    }
    public void Pause()
    {
        Time.timeScale = 0;
        //Paused.Invoke();
    }
    public void UnPause()
    {
        Time.timeScale = 1;
        //UnPaused.Invoke();
    }

    public void ResetLevel()
    {
        SceneManagerScript.instance.LoadNewScene("Level"+GameManager.instance.currentLevel);
        GameManager.instance.InitializeGame();
        
    }
}
