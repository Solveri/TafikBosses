using System.Collections;
using UnityEngine;

public class UnstoppableBall : Ability
{
    
    private bool isUnstoppable = false;
    private void Start()
    {
        this.Cooldown = 8;
        this.duration = 3;
        
    }
    private void OnEnable()
    {
        PlayerInput.DoubleTap += Roza;
    }
    
    private void Roza()
    {
       Activate();
    }
    public override void Activate()
    {
        ball = GameManager.instance.currentBall;
        if (onCooldown)
        {
            Debug.Log("Its on cooldown");
            return;
        }
        if (ball != null && !isUnstoppable)
        {
            StartCoroutine(MakeBallUnstoppable());
        }
    }




    private IEnumerator MakeBallUnstoppable()
    {
        isUnstoppable = true;
        ball.SetUnstoppable(true);
        yield return new WaitForSeconds(duration);
        ball.SetUnstoppable(false);
        isUnstoppable = false;
        Debug.Log("Unstoppable Ball Deactivated");
        onCooldown = true;
        yield return new WaitForSeconds(Cooldown);
        onCooldown = false;

    }
}
