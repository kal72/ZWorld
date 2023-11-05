using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float initialSpeed = 10f;
    public float gravity = 9.81f; // Gravity acceleration.
    public Transform target;

    private Vector3 initialPosition;
    private Vector3 initialVelocity;

    private void Start()
    {
        initialPosition = transform.position;

        if (target != null)
        {
            CalculateInitialVelocity();
        }
    }

    private void Update()
    {
        if (target != null)
        {
            PredictTargetPosition();

            // Move the projectile based on the calculated initial velocity.
            transform.position += initialVelocity * Time.deltaTime;
        }
    }

    private void CalculateInitialVelocity()
    {
        Vector3 targetPosition = target.position;

        // Calculate the direction to the target.
        Vector3 targetDirection = targetPosition - initialPosition;

        // Calculate the time of flight (time to reach the target).
        float timeToReachTarget = targetDirection.magnitude / initialSpeed;

        // Calculate the required horizontal velocity (ignore the vertical component for now).
        Vector3 horizontalVelocity = targetDirection / timeToReachTarget;

        // Calculate the required vertical velocity to compensate for gravity.
        float verticalVelocity = (targetDirection.y + 0.5f * gravity * timeToReachTarget * timeToReachTarget) / timeToReachTarget;

        // Set the initial velocity.
        initialVelocity = new Vector3(horizontalVelocity.x, verticalVelocity, horizontalVelocity.z);
    }

    private void PredictTargetPosition()
    {
        // Predict the new position of the target based on its current velocity.
        Vector3 predictedPosition = target.position + (target.GetComponent<Rigidbody>().velocity * Time.deltaTime);

        // Update the target's position.
        target.position = predictedPosition;
    }
}
