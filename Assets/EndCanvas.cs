using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCanvas : MonoBehaviour
{
    public static Action OnNextLevel;
    public static Action OnResetScene;
    public void LoadNextLevel()
    {
        if (GameManager.instance.hasRoundFinished)
        {
            OnNextLevel.Invoke();
            SceneManagerScript.instance.LoadNextLevel();
            
        }
       
    }
    public void ResetScene()
    {
        SceneManagerScript.instance.ResetScene();
        OnResetScene.Invoke();
        


    }
}
