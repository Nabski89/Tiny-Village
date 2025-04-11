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

    public void TakeDamage()
    {
        Instantiate(DamageSound);
        GetComponent<EnemyHealth>().TakeDamage();
        // Disable all hitbox colliders
        foreach (EnemyDamageHitbox hitbox in Hitboxes)
        {
            hitbox.transform.gameObject.SetActive(false);
        }

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

}