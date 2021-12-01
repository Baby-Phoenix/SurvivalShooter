using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int waveNumber = 0;
    public int numberOfEnemiesSpawned = 0;
    public int enemiesKilled = 0;

    public GameObject enemy;
    public GameObject[] spawners;

    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;


    private void Start()
    {
        spawners = new GameObject[10];

        for(int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = transform.GetChild(i).gameObject;
        }

        StartWave();
    }

    private void Update()
    {
        if (enemiesKilled >= numberOfEnemiesSpawned)
        {
          NextWave();
        }

    }

    private void SpawnEnemy()
    {
        int spawnerNumber = Random.Range(0, spawners.Length);
        Instantiate(enemy, spawners[spawnerNumber].transform.position, spawners[spawnerNumber].transform.rotation);
    }

    private void StartWave()
    {
        waveNumber = 1;
        numberOfEnemiesSpawned = 5;
        enemiesKilled = 0;

        FindObjectOfType<EnemiesAlive>().enemiesAlive = numberOfEnemiesSpawned;

        for (int i = 0; i < numberOfEnemiesSpawned; i++)
        {
            SpawnEnemy();
        }
    }

    public void NextWave()
    {
        waveNumber++;
        numberOfEnemiesSpawned += 5;
        enemiesKilled = 0;

        FindObjectOfType<EnemiesAlive>().enemiesAlive = numberOfEnemiesSpawned;

        for (int i = 0; i < numberOfEnemiesSpawned; i++)
        {
            SpawnEnemy();
        }

    }

}
