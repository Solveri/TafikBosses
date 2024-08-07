using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    [SerializeField] BrickType brickType;
    public bool isHit = false;
    public bool isHitTwice = false;
    [SerializeField] List<BrickScriptable> chooseableBricks = new List<BrickScriptable>();
    [SerializeField] List<Material> aviableColors = new List<Material>();
    BrickScriptable choosenBrick;
    SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {

        choosenBrick = chooseableBricks[UnityEngine.Random.Range(0, chooseableBricks.Count)];
        var chosenMat = aviableColors[UnityEngine.Random.Range(0, chooseableBricks.Count)];
        sr.material = chosenMat;
        sr.sprite = choosenBrick.Brick;
        onSpawn.Invoke(this);
    }
    public static event Action<Bricks> onDestory;
    public static event Action<Bricks> onSpawn;
    

    private void Update()
    {
        
    }

    public void BrickHit()
    {
       
            if (GameManager.instance.currentBall.IsUnstoppable)
            {
                AnimateBrickDestroy(gameObject, 1f);
                return;
            }
            switch (brickType)
            {
                case BrickType.Normal:
                isHit = true;
                AnimateBrickDestroy(gameObject, 1f);
                    break;
                case BrickType.DoubleHit:
                    if (!isHit)
                    {
                        isHitTwice= true;
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
  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        
       
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
            sr.sprite = choosenBrick.Broken;
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
