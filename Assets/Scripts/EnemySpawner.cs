using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject newEnemyPrefab; // Prefab to instantiate

    private void Update()
    {
        if (transform.childCount == 0)
        {
            Instantiate(newEnemyPrefab, transform.position, Quaternion.identity, transform);
            Debug.Log("No children detected. New Enemy spawned.");
        }
    }

}
