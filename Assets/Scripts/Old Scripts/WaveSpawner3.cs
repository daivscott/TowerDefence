using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner3 : MonoBehaviour
{
    // count of enemies that are alive
    public static int enemiesAlive = 0;

    // reference the enemy prefab
    public Transform enemyPrefab;

    // reference to spawn point
    public Transform spawnPoint;

    // time between waves
    public float timeBetweenWaves = 5f;

    // start countdown 
    private float countdown = 3f;

    // reference the wavecountdown text
    public Text waveCountdownText;

    // wave index identifier
    private int waveIndex = 0;

    void Update()
    {
        // if no enemies alive dont run update
        if(enemiesAlive > 0)
        {
            return;
        }

        // if timer reaches zero
        if (countdown <= 0)
        {
            SpawnWave();
        }

        void SpawnWave()
        {
            waveIndex++;
            SpawnEnemy();
        }

        void SpawnEnemy()
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemiesAlive++;
        }
    }
}
