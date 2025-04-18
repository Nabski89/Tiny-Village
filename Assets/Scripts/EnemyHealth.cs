using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int Health;
    public void TakeDamage()
    {
        Health -= 1;
        if (Health < 0)
            Destroy(gameObject);
    }
}
