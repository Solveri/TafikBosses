using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] string SceneName;
    [SerializeField] Button button;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        button.onClick.AddListener(LoadNewScene);
    }
    private void LoadNewScene()
    {
        SceneManagerScript.instance.LoadNewScene(SceneName);
    }
    

   
}
