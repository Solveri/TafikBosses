using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] BallController ballPrefab;
    [SerializeField] GameObject endCanvas;
    [SerializeField] PaddleScript paddlePrefab; // Reference to the paddle prefab
    [SerializeField]bool hasRoundStarted = false;
    public bool hasRoundFinished = false;
    public bool isGameOver = false;
    public  PaddleScript currentPaddle;
    public int numberOfBallsSpawned = 0;
    public int currentLevel = 1;
    public bool isFightingBoss = false;
    public bool hasBeatenBoss = false;
    public BallController currentBall;
    List<Bricks> currentBricks = new List<Bricks>();

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
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        DontDestroyOnLoad(this);
       
    }

    void Start()
    {
      
    }
    private void OnEnable()
    {
        Bricks.onSpawn += RegistarBrick;

        Bricks.onDestory += RemoveBrick;
        BallController.OnDestroy += SpawnNewBall;
        EndCanvas.OnNextLevel += UpdateLevel;
        SceneManagerScript.OnNewScene += InitializeGame;
        SceneManagerScript.newScene += InitializeGame;
    }

    private void RegistarBrick(Bricks brick)
    {
        currentBricks.Add(brick);
        if (!hasRoundStarted)
        {
            
            InitRound();

        }
        
    }


    void UpdateLevel()
    {
        currentLevel++;
    }
    private void RemoveBrick(Bricks brick)
    {
        currentBricks.Remove(brick);
        if (currentBricks.Count == 0)
        {

            if (isFightingBoss)
            {
                return;
            }
            else
            {
            EndRound();
            }



        }
       
    }

    void Update()
    {

       
    }
    private void SetEndCanvas()
    {
        Instantiate(endCanvas);
        
    }
    public void InitRound()
    {
        hasRoundStarted = true;
        hasRoundFinished = false;
        isGameOver = false;
        
        Debug.Log("Init has been called");
       
    }
    public void EndRound()
    {
        hasRoundStarted = false;
        hasRoundFinished = true;
        
        SetEndCanvas();
    }
    public void InitializeGame()
    {
       
        numberOfBallsSpawned = 0;
        StartCoroutine(BallDelaySpawn());
       
        
        // Check if the paddle is already instantiated
        if (currentPaddle == null)
        {
            currentPaddle = Instantiate(paddlePrefab);
        }

        
    }
    private IEnumerator BallDelaySpawn()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Heyo");
        SpawnNewBall();
    }

    private void SpawnNewBall()
    {
        if (hasRoundFinished)
        {
            return;
        }
        if (numberOfBallsSpawned < 3)
        {
            numberOfBallsSpawned++;
            currentPaddle.SpawnBallInSpawnPoint(ballPrefab);
        }
        else
        {

            isGameOver = true;
            EndRound();
            
           
        }
        
       
    }
}
