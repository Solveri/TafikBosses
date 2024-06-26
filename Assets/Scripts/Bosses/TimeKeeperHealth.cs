using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeperHealth : BossHealth
{
   
    bool isInEvent = false;
    [SerializeField] private Transform eventPoisiton;
    List<Bricks> currentBricks = new List<Bricks>();
    public Bricks BrickPrefab;
    private BallController BallController;
    bool shouldSpawn = false;
    BossAbilities abilities;
    
    // Start is called before the first frame update
    void Start()
    {
        abilities = GetComponent<TimeKeeperAbilities>();
        canBeDamaged = true;
        Bricks.onDestory += Bricks_onDestory;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        
        if (HitCount == 15)
        {
            Destroy(this.gameObject);
        }
       switch (BossStage)
        {
            case Stage.Stage1:

                abilities.Ability();
                break;
            case Stage.Stage2:
               this.canBeDamaged = false;
                Debug.Log("Entered Stage 2 "+ this.canBeDamaged);
                if (BallController == null)
                {
                    return;
                }
                if (!isInEvent)
                {
                    isInEvent = true;
                    

                }
                if (isInEvent)
                {
                    if (BallController.isBallHitThePaddle && !shouldSpawn)
                    {
                        shouldSpawn = true;
                        SecondStageEvent(2, 3);

                    }
                    else if (currentBricks.Count == 0)
                    {
                        HitCount = 10;
                        canBeDamaged = true;
                        isInEvent = false;

                    }
                }
               
                break;
            case Stage.Stage3:
              
                //Abilities.Ability2();
                break;
        }
    }
   

    private void SecondStageEvent(int row,int column)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Bricks brick = Instantiate(BrickPrefab, new Vector3(i,j,0), Quaternion.identity);
                currentBricks.Add(brick);
            }
        }
    }
    
}
