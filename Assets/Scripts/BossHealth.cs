using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossHealth : MonoBehaviour, IDamageable
{
    public const int Stage_Change_Number = 5;

    public virtual bool CanBeDamaged { get; protected set; }
    public virtual bool IsInEvent { get; protected set; }
    public virtual int Health { get; protected set; }
    public virtual int MaxHealth { get; protected set; }
    public virtual Stage BossStage { get; protected set; }
    public virtual int HitCount { get; protected set; }
    
    public static Action onHit;
    public static Action onEvent;
    public static Action onEventEnd;

    private bool hasEventStarted = false;
    private bool hasEventEnded = false;
    public virtual void Start()
    {
        GameManager.instance.isFightingBoss = true;
        CanBeDamaged = true;
    }
    public virtual void TakeDamage(float damage)
    {
        if (CanBeDamaged)
        {
            HitCount += (int)damage;
            onHit?.Invoke();
            Debug.Log(HitCount);

        }
        ChangeState();


    }
    public virtual void Update()
    {
        
        
    }
    public virtual void ChangeState()
    {
        if (HitCount >= 0 && HitCount <5)
        {
            CanBeDamaged = true;
            this.BossStage = Stage.Stage1;
        }
        else if (HitCount >= 5 && HitCount < 10)
        {
            this.BossStage = Stage.Stage2;
            CanBeDamaged = false;
            if (!hasEventStarted)
            {
               
                hasEventStarted = true;
                IsInEvent = true;
                onEvent.Invoke();
            }

        }
        else if (HitCount >= 10 && HitCount <= 15)
        {
            this.BossStage = Stage.Stage3;
           
            if (!hasEventEnded)
            {
             hasEventEnded = false;
                StartCoroutine(ChangeDamageState());
             onEventEnd?.Invoke();
            }
        }
    }

    public virtual void Death()
    {

    }

    public IEnumerator ChangeDamageState()
    {
        yield return new WaitForSeconds(2f);
        CanBeDamaged = true;
    }

}



public enum Stage
{
    Stage1,
    Stage2,
    Stage3
}
