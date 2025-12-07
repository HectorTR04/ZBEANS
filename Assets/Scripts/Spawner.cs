using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;

    public int poolSize = 20;

    public int maxEnemies = 10;
    public float minSpawnDistance = 10f;
    public float maxSpawnDistance = 25f;
    public float checkInterval = 1f;

    private List<GameObject> pool = new List<GameObject>();

    #region Unity Methods
    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(enemyPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    #endregion

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            int active = GetActiveCount();

            if (active < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }
    private void SpawnEnemy()
    {
        GameObject enemy = GetEnemy();
        if (enemy == null) return;

        Vector3 spawnPos = RandomSpawnPointAroundPlayer();
        enemy.transform.position = spawnPos;
        enemy.SetActive(true);
    }

    private GameObject GetEnemy()
    {
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null; 
    }

    private int GetActiveCount()
    {
        int count = 0;
        foreach (var obj in pool)
            if (obj.activeInHierarchy) count++;
        return count;
    }

    private Vector3 RandomSpawnPointAroundPlayer()
    {
        Vector2 randomCircle = Random.insideUnitCircle.normalized;

        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);

        Vector3 offset = new Vector3(randomCircle.x, 0, randomCircle.y) * distance;

        return player.position + offset;
    }
}
