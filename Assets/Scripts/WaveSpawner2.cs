using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner2 : MonoBehaviour
{
    // count of enemies that are alive
    public static int enemiesAlive = 0;

    // reference to enemy prefab
    public Transform enemyPrefab;

    // refernce to spawn point
    public Transform spawnPoint;

    // time between waves
    public float timeBetweenWaves = 5f;

    // amount our countdown ticks from between waves initially set to time before 1st wave spawns
    private float countdown = 3f;
    
    // reference to GUI text field
    public Text waveCountdownText;
    
    // sent enemy count
    public static int enemiesSent = 0;

    // wave index identifier
    public static int waveIndex = 0;

    void Update()
    {
        // if no enemies left dont run the remaining update code
        if (enemiesAlive > 0)
        {
            return;
        }
         
        // if timer reaches 0
        if(countdown <= 0f)
        {
            // spawn a wave using a coroutine
            StartCoroutine(SpawnWave()); 
            // reset the countdown timer
            countdown = timeBetweenWaves;
            // wait til complete before contiuing
            return;
        }

        // reduce countdown timer by 1 every second
        countdown -= Time.deltaTime;

        // if countdown less than 1 stop displaying countdown
        if (countdown <= 0.5f)
        {
            waveCountdownText.text = ""; 
        }
        else
        {
            waveCountdownText.text = Mathf.Round(countdown).ToString();
        }
    }

    // spawn wave set on ienumerator to space out enemies
    IEnumerator SpawnWave()
    {
        // increment the wave index
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            // call spawn enemy method
            SpawnEnemy();
            // wait set time before continuing
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy ()
    {
        // instantciate an enemy prefab
        //Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        GameObject spawnedObject = ObjectPooler.SharedInstance.GetPooledObject("Enemy");
        // check object exists
        if (spawnedObject != null)
        {
            spawnedObject.transform.position = spawnPoint.position;
            spawnedObject.transform.rotation = spawnPoint.rotation;
            spawnedObject.SetActive(true);
        }
        // increment enemies alive count
        enemiesAlive++;
        // increment enmies sent value
        enemiesSent++;
    }
}
