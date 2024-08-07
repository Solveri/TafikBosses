using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeperAbilities : MonoBehaviour, BossAbilities
{
    const float EFFECT_DURATION = 2f;
    const float CHANGE_TIME = 5f;

    bool isSpeedChanged = false;
    bool isSpeedingUp = true; // to alternate between speeding up and slowing down
    bool isAbilityActive = true; // to control the ability activation
    float ballOriginalSpeed = 0;
    BallScriptable controller;

    private void Awake()
    {
        
    }

    public void Ability()
    {
        if (controller != null && isAbilityActive)
        {
            ChangeBallSpeed(controller.ballController);
        }
    }

    public void ChangeBallSpeed(BallController ball)
    {
        if (ball != null && !isSpeedChanged)
        {
            ballOriginalSpeed = ball.speed;
            StartCoroutine(ChangeSpeed(ball));
        }
    }

    private IEnumerator ChangeSpeed(BallController ball)
    {
        float originalModifier = ball.CurrentModifier;
        isSpeedChanged = true;
        if (isSpeedingUp)
        {
            float newModifer = originalModifier += 0.25f;
            ball.ChangeModifier(newModifer);
            Debug.Log("Speeding up the ball");
        }
        else
        {
            float newModifer = originalModifier -= 0.25f;
            ball.ChangeModifier(newModifer);
            Debug.Log("Slowing down the ball");
        }

        yield return new WaitForSeconds(EFFECT_DURATION);

        isSpeedChanged = false;
        ball.ChangeModifier(originalModifier);
        isSpeedingUp = !isSpeedingUp; // alternate between speeding up and slowing down

        // Schedule the next speed change if the ability is still active
        if (isAbilityActive)
        {
            StartCoroutine(ScheduleNextSpeedChange());
        }
    }

    private IEnumerator ScheduleNextSpeedChange()
    {
        yield return new WaitForSeconds(CHANGE_TIME - EFFECT_DURATION);
        Ability();
    }

    // Method to stop the ability when the boss changes stages
    public void StopAbility()
    {
        isAbilityActive = false;
        StopAllCoroutines(); // Stop all coroutines to prevent further changes
    }

    // Method to start the ability when the boss is in the correct stage
    public void StartAbility()
    {
        if (!isAbilityActive)
        {
            isAbilityActive = true;
            StartCoroutine(ScheduleNextSpeedChange());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Start the ability alternation loop
        StartCoroutine(ScheduleNextSpeedChange());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
