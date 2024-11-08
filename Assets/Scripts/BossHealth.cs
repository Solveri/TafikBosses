using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossHealth : MonoBehaviour, IDamageable
{
    public const int Stage_Change_Number = 5;

    public virtual bool canBeDamaged { get; protected set; }
    public virtual int Health { get; protected set; }
    public virtual int MaxHealth { get; protected set; }
    public virtual Stage BossStage { get; protected set; }
    public virtual int HitCount { get; protected set; }
    
    public static Action onHit;
    public static Action onEvent;
    public virtual void Start()
    {
        GameManager.instance.isFightingBoss = true;
    }
    public virtual void TakeDamage(float damage)
    {
        if (canBeDamaged)
        {
            HitCount += (int)damage;
            ChangeState();
            Debug.Log(HitCount);
            onHit?.Invoke();

        }
        
    }
    public virtual void Update()
    {
        
        
    }
    public virtual void ChangeState()
    {
        if (HitCount >= 0 && HitCount <5)
        {
            this.BossStage = Stage.Stage1;
        }
        else if (HitCount >= 5 && HitCount < 10)
        {
            this.BossStage = Stage.Stage2;

        }
        else if (HitCount >= 10 && HitCount <= 15)
        {
            this.BossStage = Stage.Stage3;
        }
    }

    public virtual void Death()
    {

    }

}


public enum Stage
{
    Stage1,
    Stage2,
    Stage3
}
