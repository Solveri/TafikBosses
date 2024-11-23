using System.Collections;
using UnityEngine;

public class UnstoppableBall : Ability
{
    private bool isUnstoppable = false;
    // Track the current cooldown time remaining

    private void Start()
    {
        MaxCooldown = 8; // Set maximum cooldown time in seconds
        duration = 3; // Set ability duration in seconds
      isUnstoppable = false;
    }

    private void OnEnable()
    {
        PlayerInput.DoubleTap += ActivateOnDoubleTap;
    }

    private void OnDisable()
    {
        PlayerInput.DoubleTap -= ActivateOnDoubleTap;
    }

    private void ActivateOnDoubleTap()
    {
        Activate();
    }

    public override void Activate()
    {
        ball = GameManager.instance.currentBall;

        if (onCooldown)
        {
            Debug.Log("Ability is on cooldown");
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
        onCooldown = true;
        CurrentCooldown = MaxCooldown;

        ball.SetUnstoppable(true); // Set the ball to unstoppable
        yield return new WaitForSeconds(duration); // Wait for the duration of the ability

        ball.SetUnstoppable(false); // Revert the ball to its normal state
        isUnstoppable = false;

        Debug.Log("Unstoppable Ball Deactivated");

        // Start cooldown countdown
        while (CurrentCooldown > 0)
        {
            CurrentCooldown -= Time.deltaTime;
            yield return null;
        }

        CurrentCooldown = 0f; // Reset the current cooldown
        onCooldown = false;
    }

    // Returns a value from 1 (full) to 0 (empty) based on the cooldown progress
   
}
