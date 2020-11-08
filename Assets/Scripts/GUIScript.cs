using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour
{
    public Text enemyDeathsText;
    public Text enemiesSentText;
    public Text enemyBreachesText;
    public Text waveText;

    // Update is called once per frame
    void Update()
    {
        enemiesSentText.text = WaveSpawner2.enemiesSent.ToString();
        enemyDeathsText.text = PlayerStats.enemyDeaths.ToString();
        enemyBreachesText.text = EnemyScript.enemyBreaches.ToString();
        waveText.text = WaveSpawner2.waveIndex.ToString();
    }
}
