using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    private float paddleWidth; // Half the width of the paddle
    private float minX; // The minimum x position the paddle can move to
    private float maxX; // The maximum x position the paddle can move to
    private float lastTapTime = 0f; // Time of the last tap
    private float doubleTapThreshold = 0.3f; // Maximum time interval between taps to consider as a double-tap

    public static UnityAction DoubleTap;
    // Start is called before the first frame update
    void Start()
    {
        // Calculate half the width of the paddle
        paddleWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;

        // Calculate the left and right boundaries of the screen in world coordinates
        Vector3 leftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 rightBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.nearClipPlane));

        // Set the minimum and maximum x positions the paddle can move to, taking into account the paddle's width
        minX = leftBoundary.x + paddleWidth;
        maxX = rightBoundary.x - paddleWidth;
    }

    // Update is called once per frame
    void Update()
    {
        // If the round has finished, do not process input
        if (GameManager.instance.hasRoundFinished)
        {
            return;
        }

        // Check if there is any touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position); // Convert touch position to world coordinates
            Vector2 newPos = new Vector2(touchPosition.x, transform.position.y); // Create a new position for the paddle based on the touch position
            newPos.x = Mathf.Clamp(newPos.x, minX, maxX); // Clamp the new position to stay within the screen boundaries
            this.transform.position = newPos; // Update the paddle's position

            // Check if the touch has ended
            if (touch.phase == TouchPhase.Ended)
            {
                // Launch the ball if it hasn't been launched yet
                if (!GameManager.instance.currentBall.isLaunched)
                {
                    GameManager.instance.currentBall.Launch(Vector2.up);
                }

                // Check if the time interval between the current tap and the last tap is within the double-tap threshold
                float currentTime = Time.time;
                if (currentTime - lastTapTime < doubleTapThreshold)
                {
                    OnDoubleTap(); // Trigger the double-tap action
                }
                lastTapTime = currentTime; // Update the last tap time
            }
        }
    }

    // Method to handle double-tap actions
    void OnDoubleTap()
    {
        // Your double-tap action here
        DoubleTap.Invoke();
        // Add the logic for the double-tap action
    }
}
