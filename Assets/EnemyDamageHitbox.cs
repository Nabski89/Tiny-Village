using UnityEngine;

public class EnemyDamageHitbox : MonoBehaviour
{
    EnemyController Controller;
    void Start()
    {
        Controller = GetComponentInParent<EnemyController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WeaponHitbox>() != null)
        {
            Controller.TakeDamage();
        }
    }
}
