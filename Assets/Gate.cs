using UnityEngine;

public class Gate : MonoBehaviour
{
    public float moveDuration = 2f; // Time it takes to move
    private Vector3 targetPosition; // Target position
    private bool isMoved = false;

    void Start()
    {
        targetPosition = transform.position; // Store initial position
    }

    public void MoveDown()
    {
        if (!isMoved)
        {
            targetPosition = transform.position + new Vector3(0, -5f, 0);
            StartCoroutine(MoveToTarget());
        }
    }

    private System.Collections.IEnumerator MoveToTarget()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoved = true;
    }
}