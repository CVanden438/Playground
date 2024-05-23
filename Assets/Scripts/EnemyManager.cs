using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    GameObject player;
    public bool canSpawn = false;
    public BiomeSO forest;
    public BiomeSO desert;
    public BiomeSO swamp;
    public BiomeSO taiga;
    public BiomeSO tundra;
    public BiomeSO ash;
    public BiomeSO currentBiome;
    public List<GameObject> currentEnemies = new();

    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        currentBiome = forest;
    }

    void Start()
    {
        StartCoroutine(Spawner());
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 playerPosition = player.transform.position;
        float minDistance = 5f;
        float maxDistance = 10f;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minDistance, maxDistance);

        Vector3 spawnPosition =
            playerPosition + new Vector3(randomDirection.x, randomDirection.y, 0) * randomDistance;

        return spawnPosition;
    }

    void SpawnEnemy()
    {
        var totalWeight = currentBiome.enemies.Sum(e => e.spawnRarity);
        var randomValue = Random.Range(0, totalWeight);
        float accumulatedRarity = 0;
        foreach (EnemySO enemy in currentBiome.enemies)
        {
            accumulatedRarity += enemy.spawnRarity;
            if (randomValue <= accumulatedRarity)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                var newEnemy = Instantiate(enemy.prefab, spawnPosition, Quaternion.identity);
                currentEnemies.Add(newEnemy);
                break;
            }
        }
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            if (currentEnemies.Count < currentBiome.maxSpawns && canSpawn)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(currentBiome.spawnRate);
        }
    }
}
