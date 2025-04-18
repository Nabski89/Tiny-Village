using UnityEngine;
using System.Collections;

public class PlayerDamageController : MonoBehaviour
{
    public GameObject DamageSound;
    public bool iFrame;
    public float reEnableDelay = 0.25f;
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DealDamage>() != null && iFrame == false)
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        Instantiate(DamageSound);
        iFrame = true;
        // Invoke method to re-enable them after a delay
        StartCoroutine(ReEnableHitboxes());
    }

    private IEnumerator ReEnableHitboxes()
    {
        yield return new WaitForSeconds(reEnableDelay);

        Debug.Log("Re-enabling player damage...");
        iFrame = false;
    }
}
