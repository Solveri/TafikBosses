using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BallController))]
public class BallTrailController : MonoBehaviour
{
    private TrailRenderer trailRenderer;
    private BallController ballController;
    private float maxAlpha = 1.0f; // Maximum alpha (fully visible)
    private float minAlpha = 0.0f; // Minimum alpha (invisible)

    void Start()
    {
        // Get references to TrailRenderer and BallController
        trailRenderer = GetComponent<TrailRenderer>();
        ballController = GetComponent<BallController>();

        // Set initial TrailRenderer properties
        trailRenderer.time = 0.5f; // Duration of the trail
        trailRenderer.startColor = new Color(1f, 1f, 1f, maxAlpha); // Fully visible start color
        trailRenderer.endColor = new Color(1f, 1f, 1f, minAlpha); // Fades to invisible
    }

    void Update()
    {
        // Calculate alpha based on the ball's speed
        float normalizedSpeed = Mathf.InverseLerp(ballController.minSpeed, ballController.maxSpeed, ballController.speed);
        float currentAlpha = Mathf.Lerp(minAlpha, maxAlpha, normalizedSpeed);

        // Apply the same alpha to both the start and end colors to maintain a consistent opacity
        Color trailColor = new Color(1f, 1f, 1f, currentAlpha);
        trailRenderer.startColor = trailColor;
        trailRenderer.endColor = trailColor; // No gradual fading; entire trail has the same visibility
    }


}
