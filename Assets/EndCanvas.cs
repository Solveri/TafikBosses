using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EndCanvas : MonoBehaviour
{
    public static Action OnNextLevel;
    
    [SerializeField] GameObject nextLevelButton;
    [SerializeField] GameObject winningPanel;
    [SerializeField] GameObject losingPanel;
    private void Start()
    {
        if (GameManager.instance.isGameOver)
        {
            nextLevelButton.gameObject.SetActive(false);
        }
        if (GameManager.instance.hasBeatenBoss)
        {
            winningPanel.gameObject.SetActive(true);
            losingPanel.gameObject.SetActive(false);

        }
        else
        {
            losingPanel.gameObject.SetActive(true);
            winningPanel.gameObject.SetActive(false);


        }
    }
    public void LoadNextLevel()
    {
        if (GameManager.instance.hasRoundFinished && !GameManager.instance.isGameOver)
        {
            OnNextLevel.Invoke();
            SceneManagerScript.instance.LoadNextLevel();
            
        }
       
    }
    public void ResetScene()
    {
        SceneManagerScript.instance.ResetScene();
       
        


    }
}
