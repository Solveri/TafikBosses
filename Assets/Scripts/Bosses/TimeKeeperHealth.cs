using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TimeKeeperHealth : BossHealth
{
   
    
    
    [SerializeField] private Transform eventPoisiton;
    public List<Bricks> currentBricks = new List<Bricks>();
    [SerializeField] Sprite bossAbi;

    public Bricks BrickPrefab;
    private BallController BallController;
    bool hasSpawned = false;
    BossAbilities abilities;
    //public static Action onEventEnd;
    private void OnEnable()
    {
       
    }

    // Start is called before the first frame update
    public override void  Start()
    {
        base.Start();
        GameManager.instance.InitRound();
       
        abilities = GetComponent<TimeKeeperAbilities>();
        
     
        Bricks.onDestory += Bricks_onDestory;
        onEvent += StartEvent;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

       
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
                IsInEvent = false;
                if (!IsInEvent)
                {
                    onEventEnd.Invoke();
                    CanBeDamaged = true;
                    TakeDamage(5);
                }
            }
        }
    }
    

    // Update is called once per frame
    public override void Update()
    {

        if (HitCount >= 15)
        {
            Death();
        }
        

    }
    
   
   
    private void StartEvent()
    {

        if (!hasSpawned)
        {
            
            hasSpawned = true;
            IsInEvent = true;
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
