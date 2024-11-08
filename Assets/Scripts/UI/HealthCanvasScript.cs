using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class HealthCanvasScript : MonoBehaviour
{
    
    private List<GameObject> secondList = new List<GameObject>();

    public float shrinkDuration = 0.3f; // Duration of the shrink effect
    private void OnEnable()
    {
        
    }
    private void Start()
    {
        BallController.OnDestroy += onBallDestroyed;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            secondList.Add(child.gameObject);
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnDisable()
    {
        BallController.OnDestroy -= onBallDestroyed;
    }



    private void onBallDestroyed()
    {
        
        GameObject currentLive =secondList.Count > 0 ? secondList[0] : null;
        LeanTween.scale(currentLive, Vector3.zero, shrinkDuration).setEase(LeanTweenType.easeInOutQuad);

        // Step 2: Fade out the object to make it disappear
        LeanTween.alpha(currentLive, 0f, shrinkDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            if (currentLive != null)
            {
                secondList.RemoveAt(0);
               
                currentLive.SetActive(false);
            }
        });
       
    }
}
