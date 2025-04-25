using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject newEnemyPrefab; // Prefab to instantiate
    public int EnemiesKilled;
    public bool enemies3 = false;
    private void Update()
    {
        if (transform.childCount == 0)
        {
            Instantiate(newEnemyPrefab, transform.position, Quaternion.identity, transform);
            Debug.Log("No children detected. New Enemy spawned.");
            EnemiesKilled += 1;
            if (EnemiesKilled > 3)
                EnemiesKill();
        }
    }
    public void EnemiesKill()
    {
        enemies3 = true;
    }

    public bool Enemies3(string node, int lineIndex)
    {
        if (EnemiesKilled > 3)
            return false;
        else
            return true;
    }
}
