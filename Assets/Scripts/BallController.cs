using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour,IAttackable
{
    public float speed = 10f; // Speed of the ball
    private Rigidbody2D rb;
    
    bool isBallHitTheMiddle = false;
    bool isBallHitTheSide = false;
    public bool isBallHitThePaddle = false;
    float initSpeed;
    [SerializeField]private float modifer;
    float maxSpeed { get => initSpeed * (150/100); }
    float maxModifer { get => modifer * (150/100); }
    float minModifier { get => modifer *0.50f; }
    float minSpeed { get => initSpeed *0.50f; }
    public static event System.Action onDestroy;
    public bool isActive;
    public float CurrentModifer { get { return modifer; } }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }
    void Start()
    {
      
        rb.velocity = Vector2.up * speed; // Initial velocity to start the ball moving
        initSpeed = speed;
        isActive = true;

    }

    public void ChangeModifer(float newModifer)
    {
        modifer = newModifer;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the paddle
        if (collision.gameObject.CompareTag("Paddle"))
        {
            isBallHitThePaddle = true;
            HandlePaddleCollision(collision);
            ChangeSpeed();
        }
        else
        {
            isBallHitThePaddle = false;
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            Attack(collision.gameObject.GetComponent<IDamageable>());
        }
        if (collision.gameObject.CompareTag("Miss"))
        {
            DestoryBall();
        }
    }
    private void Update()
    {
        if (speed > maxSpeed)
        {
            speed = maxSpeed;

        }
        if (speed < minSpeed)
        {
            speed = minSpeed;
        }
    }
    // make a function that changes the modifer to make it more decoupled
    void ChangeSpeed()
    {
        if (isBallHitTheSide)
        {
            isBallHitTheSide = false;
            float currentSpeed = speed;

            
            if (speed < maxSpeed)
            {

                modifer += 0.25f;

                if (modifer > maxModifer)
                {
                    modifer = maxModifer;
                }
                speed *= modifer;

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
             
                modifer -=0.15f;
                if(modifer < minModifier)
                {
                    modifer = minModifier;
                }
                Debug.Log($"modifer is {modifer}");
                speed *= modifer;
               
            }
           Debug.Log("Speed decreased to " + speed);
            
        }
    }

    void HandlePaddleCollision(Collision2D collision)
    {
        Vector2 paddlePosition = collision.transform.position; // Get the paddle's position
        Vector2 contactPoint = collision.contacts[0].point; // Get the contact point of the collision
        float paddleWidth = collision.collider.bounds.size.x; // Get the width of the paddle

        // Calculate the hit position relative to the paddle's width (-0.5 to 0.5)
        float hitPosition = (contactPoint.x - paddlePosition.x) / paddleWidth;

        // Adjust hitPosition to a range of -1 to 1
        hitPosition *= 2;

        // Determine the reflection angle based on where the ball hits the paddle
        float angle;
        if (hitPosition < -0.40f)
        {
            // Ball hits the left side of the paddle (135 to 180 degrees)
            angle = 100 + (hitPosition + 1) * 45;
            isBallHitTheSide = true;
        }
        else if (hitPosition > 0.40f)
        {
            // Ball hits the right side of the paddle (0 to 45 degrees)
            angle = (hitPosition - 0.33f) * 80;
            isBallHitTheSide = true;
        }
        else
        {
            // Ball hits the middle part of the paddle (90 degrees)
            angle = 90;
            isBallHitTheMiddle = true;
        }

        Debug.Log("Hit Position: " + hitPosition + " Angle: " + angle);

        // Convert the angle to a direction vector
        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        rb.velocity = direction * speed; // Set the new velocity of the ball
    }

    private void DestoryBall()
    {
        onDestroy.Invoke();
        Destroy(this.gameObject);
    }
    public void Attack(IDamageable target)
    {
        target.TakeDamage(1);
    }
   
}
