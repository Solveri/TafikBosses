using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeperHealth : BossHealth
{
    public bool IsIntEvent { get { return IsIntEvent; }  }
    bool isInEvent = false;
    
    [SerializeField] private Transform eventPoisiton;
    public List<Bricks> currentBricks = new List<Bricks>();
    public Bricks BrickPrefab;
    private BallController BallController;
    bool hasSpawned = false;
    BossAbilities abilities;
    
    // Start is called before the first frame update
    void Start()
    {

        GameManager.instance.InitRound();
        abilities = GetComponent<TimeKeeperAbilities>();
        
        canBeDamaged = true;
        Bricks.onDestory += Bricks_onDestory;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (BossStage == Stage.Stage2)
        {
            Debug.Log("Entered Stage2");
            canBeDamaged = false;
            isInEvent = true;
            

           
            
        }
        
        if (collision.transform.CompareTag("Ball"))
        {
            Debug.Log("Ball");
            if (BallController == null)
            {
                if (collision.gameObject.TryGetComponent(out BallController))
                {
                    Debug.Log("Scucsses");
                }
               
            }

        }
        
    }
    public override void Death()
    {
        GameManager.instance.EndRound();
    }
    private void Bricks_onDestory(Bricks bricks)
    {
        Destroy(bricks.gameObject);
        if (currentBricks.Contains(bricks))
        {
            currentBricks.Remove(bricks);
        }
    }
    

    // Update is called once per frame
    public override void Update()
    {
        if (isInEvent && BallController.isBallHitThePaddle)
        {
                Debug.Log("Entered Event");
                StartEvent();
            if (currentBricks.Count == 0)
            {
                isInEvent = false;
            }

        }
        if (!isInEvent)
        {
            canBeDamaged = true;
        }

    }
    public void LateUpdate()
    {
      
    }
   
    

    private void StartEvent()
    {

        if (!hasSpawned)
        {
            hasSpawned = true;
            isInEvent = true;
            SecondStageEvent(3, 2);
        }
       
    }
    
    private void SecondStageEvent(int row,int column)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Bricks brick = Instantiate(BrickPrefab, new Vector3(i-1*0.7f*1.5f,(j*0.7f)*1.5f,0), Quaternion.identity);
                currentBricks.Add(brick);
            }
        }
    }
    
}
