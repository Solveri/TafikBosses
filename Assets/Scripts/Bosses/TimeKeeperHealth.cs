using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TimeKeeperHealth : BossHealth
{
    public bool IsIntEvent { get { return isInEvent; }  }
    bool isInEvent = false;
    
    [SerializeField] private Transform eventPoisiton;
    public List<Bricks> currentBricks = new List<Bricks>();
    [SerializeField] Sprite bossAbi;
    SpriteRenderer sr;
    public Bricks BrickPrefab;
    private BallController BallController;
    bool hasSpawned = false;
    BossAbilities abilities;
    public static Action onEventEnd;
    private void OnEnable()
    {
        onHit += ChangeState;
    }

    // Start is called before the first frame update
    public override void  Start()
    {
        base.Start();
        GameManager.instance.InitRound();
       
        abilities = GetComponent<TimeKeeperAbilities>();
        
        canBeDamaged = true;
        Bricks.onDestory += Bricks_onDestory;
        onHit += On_Hit;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (BossStage == Stage.Stage2)
        {
            Debug.Log("Entered Stage2");
            canBeDamaged = false;
            isInEvent = true;




        }

       
    }
    public override void Death()
    {
        GameManager.instance.hasBeatenBoss = true;
        GameManager.instance.EndRound();
        Destroy(this.gameObject);
    }
    private void Bricks_onDestory(Bricks bricks)
    {
        if (currentBricks.Contains(bricks))
        {
            currentBricks.Remove(bricks);
            if (currentBricks.Count == 0)
            {
                isInEvent = false;
                if (!isInEvent)
                {
                    onEventEnd.Invoke();
                    canBeDamaged = true;
                    TakeDamage(5);
                }
            }
        }
    }
    

    // Update is called once per frame
    public override void Update()
    {

        if (HitCount >=15)
        {
            Death();
        }
        

    }
    private void On_Hit()
    {
       
        if (BossStage == Stage.Stage2 )
        {
            Debug.Log("Entered Event");
            StartEvent();


        }
    }
    public void LateUpdate()
    {
      
    }
  
    
   
    private void StartEvent()
    {

        if (!hasSpawned)
        {
            canBeDamaged = false;
            hasSpawned = true;
            isInEvent = true;
            SecondStageEvent(3, 2);
            onEvent.Invoke();
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
