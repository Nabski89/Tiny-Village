using UnityEngine;
using System.Collections;


public class EnemyController : MonoBehaviour
{
    public GameObject DamageSound; // Prefab to instantiate on trigger

    public float reEnableDelay = 0.5f; // Time T before enabling hitboxes again
    public EnemyDamageHitbox[] Hitboxes;

    private void Start()
    {
        Hitboxes = GetComponentsInChildren<EnemyDamageHitbox>();
    }

    public void TakeDamage(Transform DamageSource)
    {
        Instantiate(DamageSound);
        GetComponent<EnemyHealth>().TakeDamage();
        // Disable all hitbox colliders
        foreach (EnemyDamageHitbox hitbox in Hitboxes)
        {
            hitbox.transform.gameObject.SetActive(false);
        }
        StartCoroutine(MoveAway(DamageSource.position));
        // Invoke method to re-enable them after a delay
        StartCoroutine(ReEnableHitboxes());
    }

    private IEnumerator ReEnableHitboxes()
    {
        yield return new WaitForSeconds(reEnableDelay);

        Debug.Log("Re-enabling hitboxes...");
        foreach (EnemyDamageHitbox hitbox in Hitboxes)
        {
            hitbox.transform.gameObject.SetActive(true);
        }
    }
    public float moveDistance = 0.25f; // Distance to move away
    public float moveDuration = 0.1f; // Duration of movement
    private IEnumerator MoveAway(Vector3 otherPosition)
    {
        Vector3 direction = (transform.position - otherPosition).normalized; // Get direction away from the collider
        Vector3 targetPosition = transform.position + direction * moveDistance; // Calculate target position

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Ensure final position is exactly the target
    }
}