using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BallController : MonoBehaviour, IAttackable
{
    public float speed = 10f; // Speed of the ball
    private Vector2 direction = Vector2.up; // Initial direction
    [SerializeField] Camera gameCam;
    bool isBallHitTheMiddle = false;
    bool isBallHitTheSide = false;
    public bool isBallHitThePaddle = false;
    float initSpeed;
    [SerializeField] private float modifier;
    [SerializeField]int AmountOfHits;
    [SerializeField]SpriteRenderer spriteRenderer;
    Color originalColor;
    public bool IsUnstoppable { get { return isUnstoppable; } }
    public float maxSpeed { get => initSpeed * 1.50f; }
    float maxModifier { get => modifier * 1.5f; }
    float minModifier { get => modifier * 0.5f; }
    public float minSpeed { get => initSpeed * 0.75f;}
    public static event System.Action OnDestroy;
    public bool isActive;
    private bool hasHitSomething = false;
    public float CurrentModifier { get { return modifier; } }
    private bool isUnstoppable = false;
    public bool isLaunched;

    private void Awake()
    {
        if (gameCam == null)
        {
            gameCam = Camera.main;
        }
      
    }
    private void Start()
    {
        initSpeed = speed;
        isActive = true;
        originalColor = spriteRenderer.color;
      
    }

    private void Update()
    {
        if (!isLaunched)
            return;

        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        if (speed < minSpeed)
        {
            speed = minSpeed;
        }

        // Move the ball
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        // Check for collisions
        //StartCoroutine(CheckColDelay());
        CheckCollisions();
        ChangeColor();
        if (AmountOfHits >= 5)
        {
            AmountOfHits = 0;
            //DirectBallToPaddle();
        }
    }
   
    private void ChangeColor()
    {
        if (IsUnstoppable)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = originalColor;
        }
    }
    public void Launch(Vector2 initialDirection)
    {
        direction = initialDirection.normalized;
        isLaunched = true;
    }

    public void ChangeModifier(float newModifier)
    {
        modifier = newModifier;
    }

    public void SetUnstoppable(bool value)
    {
        isUnstoppable = value;
        if (isUnstoppable)
        {
            Debug.Log("Ball is now unstoppable");
        }
        else
        {
            Debug.Log("Ball is no longer unstoppable");
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void CheckCollisions()
    {
        // Check for collisions with walls
        CheckWallCollisions();

        // Check for collisions with paddle
        CheckPaddleCollisions();

        // Check for collisions with bricks
        //CheckBrickCollisions();
    }

    private void CheckWallCollisions()
    {
        float halfScreenWidth = gameCam.orthographicSize * Camera.main.aspect;
        float halfScreenHeight = gameCam.orthographicSize;
        float minAngle = 15f; // Minimum angle in degrees to prevent repetitive bouncing

        if (transform.position.x <= -halfScreenWidth || transform.position.x >= halfScreenWidth)
        {
            direction.x = -direction.x; // Reflect horizontally
            AdjustDirection(minAngle);
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -halfScreenWidth, halfScreenWidth),
                transform.position.y,
                transform.position.z
            );
            AmountOfHits++;
        }

        if (transform.position.y >= halfScreenHeight)
        {
            direction.y = -direction.y; // Reflect vertically
            AdjustDirection(minAngle);
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Clamp(transform.position.y, -halfScreenHeight, halfScreenHeight),
                transform.position.z
            );
            AmountOfHits++;
        }

        if (transform.position.y <= -halfScreenHeight)
        {
            DestroyBall();
        }
    }

    private void AdjustDirection(float minAngle)
    {
        float minRadians = Mathf.Deg2Rad * minAngle; // Convert angle to radians
        if (Mathf.Abs(Mathf.Atan2(direction.y, direction.x)) < minRadians)
        {
            direction.y += Mathf.Sign(direction.y) * Mathf.Tan(minRadians);
        }
        else if (Mathf.Abs(Mathf.Atan2(direction.x, direction.y)) < minRadians)
        {
            direction.x += Mathf.Sign(direction.x) * Mathf.Tan(minRadians);
        }

        direction = direction.normalized; // Keep the direction normalized
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position,0.16f);
    }
    private void CheckPaddleCollisions()
    {
       
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position,0.15f);
        
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Paddle"))
            {
                
                isBallHitThePaddle = true;
                HandlePaddleCollision(hit);
                AmountOfHits = 0;
                break;
            }
            if (hit.CompareTag("Brick"))
            {
                // maybe add a parameter that takes in the hit aka block and then check if we already hit that block so we dont refaclted twice
                Debug.Log("Hit1");
                AmountOfHits = 0;
                HandleBrickCollision(hit);
            }
            else if (hit.CompareTag("Boss"))
            {

                HandleBossColision(hit);
            }
            else
            {
                isBallHitThePaddle = false;

            }
        }
    }
    private IEnumerator NextFrame(float duration)
    {
        yield return new WaitForSeconds(duration);
        hasHitSomething = false;
    }
   private void HandleBossColision(Collider2D collider2D)
    {
        //need to make it so it only hit once   
        //hasHitSomething might casue bug  
        if (collider2D.TryGetComponent(out BossHealth boss) && !hasHitSomething)
        {
            hasHitSomething = true;
            boss.TakeDamage(1);
            Vector2 normal = (transform.position - collider2D.transform.position).normalized;
            direction = Vector2.Reflect(direction, normal);
            StartCoroutine(NextFrame(0.5f));
        }
        
    }
    private void HandlePaddleCollision(Collider2D collider)
    {
        Vector2 paddlePosition = collider.transform.position; // Get the paddle's position
        Vector2 contactPoint = transform.position; // Approximate the contact point using the ball's position
        float paddleWidth = collider.bounds.size.x; // Get the width of the paddle

        // Calculate the hit position relative to the paddle's width (-0.5 to 0.5)
        float hitPosition = (contactPoint.x - paddlePosition.x) / paddleWidth;

        // Adjust hitPosition to a range of -1 to 1
        hitPosition *= 2;

        // Determine the reflection angle based on where the ball hits the paddle
        float angle;
        if (hitPosition < -0.40f)
        {
            // Ball hits the left side of the paddle (135 to 180 degrees)
            angle = 135 + (hitPosition + 1) * 45;
            isBallHitTheSide = true;
        }
        else if (hitPosition > 0.40f)
        {
            // Ball hits the right side of the paddle (0 to 45 degrees)
            angle = (hitPosition - 0.33f) * 45;
            isBallHitTheSide = true;
        }
        else
        {
            // Ball hits the middle part of the paddle (90 degrees)
            angle = 80;
            isBallHitTheMiddle = true;
        }

        Debug.Log("Hit Position: " + hitPosition + " Angle: " + angle);

        // Convert the angle to a direction vector
        direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        ChangeSpeed();
    }
    
    private void ChangeBallDirectionOnHit(Transform colider,Bricks brick)
    {
        Vector2 normal = (transform.position - colider.transform.position).normalized;
        direction = Vector2.Reflect(direction, normal);
        brick.BrickHit();
        //hasHitSomething = true;
        //StartCoroutine(NextFrame(0.1f));

    }
    private void HandleBrickCollision(Collider2D collider)
    {
        float distance = Vector2.Distance(this.gameObject.transform.position, collider.transform.position);
        Bricks brick = collider.gameObject.GetComponent<Bricks>();
        if (isUnstoppable)
        {
            brick.BrickHit();
            return; // Ignore collisions if the ball is unstoppable
        }

        if (distance < 0.4f && !brick.isHit)
        {
            brick.isHit = true;

            if (brick.hasBeenHitOnce && brick.isHit && !brick.isHitTwice)
            {
                Debug.Log("Hit Twice");
                ChangeBallDirectionOnHit(collider.transform, brick);
            }
            else if (brick.isHit && !brick.hasBeenHitOnce)
            {
                ChangeBallDirectionOnHit(collider.transform, brick);

            }




        }
        else if (brick.isHit && distance >= 0.3f){
            brick.isHit = false;

        }
           
         
        



        //if (!brick.isHitTwice && !brick.isDestroyed && !isUnstoppable)
        //{
        //    ChangeBallDirectionOnHit(collider.transform, brick);



        //}

    }

    private void DestroyBall()

    {
        Destroy(this.gameObject);
        OnDestroy.Invoke();
    }

    public void Attack(IDamageable target)
    {
        target.TakeDamage(1);
    }

    void ChangeSpeed()
    {
        if (isBallHitTheSide)
        {
            isBallHitTheSide = false;
            float currentSpeed = speed;

            if (speed < maxSpeed)
            {
                modifier += 0.25f;

                if (modifier >= maxModifier)
                {
                    modifier = maxModifier;
                }
                speed *= modifier;

                if (speed > maxSpeed)
                {
                    speed = maxSpeed;
                }
            }

            Debug.Log("Speed increased from " + currentSpeed + " to " + speed);
        }
        if (isBallHitTheMiddle)
        {
            isBallHitTheMiddle = false;
            if (speed > minSpeed)
            {
                modifier -= 0.15f;
                if (modifier < minModifier)
                {
                    modifier = minModifier;
                }
                Debug.Log($"modifier is {modifier}");
                speed *= modifier;
            }
            Debug.Log("Speed decreased to " + speed);
        }
    }
    private void DirectBallToPaddle()
    {
        Vector2 directionToPaddle = (GameManager.instance.currentPaddle.transform.position - transform.position).normalized;
        direction = directionToPaddle;
        
    }
}
