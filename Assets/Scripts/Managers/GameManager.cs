using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]BallController currentBall;
    [SerializeField] BallController ballPrefab;
    [SerializeField] GameObject endCanvas;
    public bool isGameOver = false;
    PaddleScript currentPaddle;
     public int numberOfBallsSpawned = 0;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        currentPaddle = FindObjectOfType<PaddleScript>();
        BallController.onDestroy += SpawnNewBall;
        DontDestroyOnLoad(this);
    }
    
    /*Need to create Restart Button
     * Need to give 3 balls to the ball and after the player reachs the miss coliider need to Destory it and give a new one in the middle(so i need spawnPoint for the new one)
     * need to add a list/array to hold all the extra balls and check if its empty if not and he missed one ball then spawn another one in a sec
     * 
     * 
     */
    // Start is called before the first frame update
    void Start()
    {

        SpawnNewBall();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    
    public void RestGame()
    {
        SpawnNewBall();
        BallController.onDestroy += SpawnNewBall;
        numberOfBallsSpawned = 0;
        if (currentPaddle == null)
        {
            currentPaddle = FindObjectOfType<PaddleScript>();
            
        }
    }
   
    private void SpawnNewBall()
    {
        
        if (numberOfBallsSpawned <3)
        {
            numberOfBallsSpawned++;
            currentPaddle.SpawnBallInSpawnPoint(ballPrefab);
        }
        else
        {
            endCanvas.SetActive(true);
            BallController.onDestroy -= SpawnNewBall;
            
            
        }
       
        
    }
}
