using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    [SerializeField] BrickType brickType;
    bool isHit = false;
    public static event Action<Bricks> onDestory;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.CompareTag("Ball"))
        {
            switch (brickType)
            {
                case BrickType.Normal:
                    AnimateBrickDestroy(gameObject, 1f);
                    break;
                case BrickType.DoubleHit:
                    if (!isHit)
                    {
                        isHit = true;
                        AnimateBrickHit(gameObject, 0.8f);
                    }
                    else
                    {
                        AnimateBrickDestroy(gameObject, 1f);
                    }
                    break;
                case BrickType.TripleHit:
                    // Implement similar logic for TripleHit bricks if needed
                    break;
                case BrickType.Unbreakable:
                    // No action for unbreakable bricks
                    break;
                default:
                    break;
            }
        }
    }

    private void AnimateBrickDestroy(GameObject brick, float scaleDownFactor)
    {
        float popDuration = UnityEngine.Random.Range(0.1f, 0.2f); // Random pop duration
        float scaleDownDuration = UnityEngine.Random.Range(0.1f, 0.2f); // Random scale down duration

        // Pop out animation
        LeanTween.scale(brick, brick.transform.localScale * 1.2f, popDuration).setEase(LeanTweenType.easeOutBounce).setOnComplete(() =>
        {
            // Scale down animation
            LeanTween.scale(brick, brick.transform.localScale * scaleDownFactor, scaleDownDuration).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
            {
                Destroy(brick);
                onDestory?.Invoke(this);
            });
        });
    }

    private void AnimateBrickHit(GameObject brick, float scaleFactor)
    {
        float popDuration = UnityEngine.Random.Range(0.1f, 0.2f); // Random pop duration
        float scaleDownDuration = UnityEngine.Random.Range(0.1f, 0.2f); // Random scale down duration

        // Pop out animation
        LeanTween.scale(brick, brick.transform.localScale * 1.1f, popDuration).setEase(LeanTweenType.easeOutBounce).setOnComplete(() =>
        {
            // Scale down animation (80% of original size)
            LeanTween.scale(brick, brick.transform.localScale * scaleFactor, scaleDownDuration).setEase(LeanTweenType.easeInBack);
        });
    }
}

public enum BrickType
{
    Normal,
    DoubleHit,
    TripleHit,
    Unbreakable
}
