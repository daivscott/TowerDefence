using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    // public static flag to be accessed anywhere
    public static bool targeted = false;

    public bool isTargeted = targeted;

    // set enemy speed
    public float speed = 10f;
    
    // enemy breach count
    public static int enemyBreaches = 0;

    // create a transform variable for the enemy position
    public Transform target;
    // set waypoint index variable
    private int waypointIndex = 0;

    //public Text enemyDebug;

    private float getCurrentHealth;

    // Start is called before the first frame update
    void OnEnable()
    {
        // set the target transform to 1st position of static waypoints array
        target = WaypointsScript.points[0];
        // reset waypoint index
        waypointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        isTargeted = targeted;
        // get direction of target location
        Vector3 dir = target.position - transform.position;
        // move to target location
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // when close to next waypoint get next target
        if(Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
        //enemyDebug.text = waypointIndex.ToString();
        getCurrentHealth = gameObject.GetComponent<HealthScript>().currentHealth;///////////////////// dodgy

        if (getCurrentHealth < 0f)
        {
            //Debug.Log("Enemy Died");
            gameObject.SetActive(false);
            WaveSpawner2.enemiesAlive--;
            PlayerStats.enemyDeaths++;
        }
        //Debug.Log(getCurrentHealth);
    }

    void GetNextWaypoint()
    {
        if(waypointIndex >= WaypointsScript.points.Length - 1)
        {
            // destroy object at last waypoint
            //Destroy(gameObject);

            // set the target transform to 1st position of static waypoints array
            waypointIndex = 0;

            gameObject.SetActive(false);/////////////////////////////////////////
            WaveSpawner2.enemiesAlive--;
            enemyBreaches++;
            // wait til game object is destroyed before continuing
            return;
        }

        // increment waypooint index
        waypointIndex++;
        // set next target
        target = WaypointsScript.points[waypointIndex];
    }
}
