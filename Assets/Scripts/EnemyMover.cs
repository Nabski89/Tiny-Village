using UnityEngine;
using System.Collections;

public class EnemyMover : MonoBehaviour
{
    public float moveSpeed = 1f; // Speed of movement
    public float wanderRange = 5f; // Maximum distance from home position

    public Vector3 homePosition; // Stores the starting home location
    public Vector3 targetPosition; // Stores the random target position

    void Start()
    {
        // Set the home position at the start
        homePosition = transform.position;
        PickNewTargetPosition();
    }

    void Update()
    {
        targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        // Move toward the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // If close to target, pick a new random position. This only works on a plane
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(targetPosition.x, targetPosition.z)) < 0.1f)
        {
            PickNewTargetPosition();
        }
    }

    void PickNewTargetPosition()
    {
        // Select a random position within the wander range
        float randomX = Random.Range(-wanderRange, wanderRange);
        float randomZ = Random.Range(-wanderRange, wanderRange);

        targetPosition = homePosition + new Vector3(randomX, 0, randomZ);
        StartCoroutine(RotateTowardsTarget());

    }

    public float rotationSpeed = 0.25f;
    IEnumerator RotateTowardsTarget()
    {
        while (true)
        {
            // Get direction to the target position (ignoring z-axis)
            Vector3 direction = new Vector3(targetPosition.x, targetPosition.y, transform.position.z) - transform.position;

            // Calculate the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Stop rotating when close enough
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
                yield break;

            yield return null; // Wait until the next frame
        }
    }

}