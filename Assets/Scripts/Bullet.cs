using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Turret2))]
public class Bullet : MonoBehaviour
{
    private GameObject target;

    public float speed = 70f;
    public GameObject impactEffect;
    
    // enemy death count
    public static int enemyDeaths = 0;

    public void Seek(GameObject _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)// || target.activeSelf == false) // added OR to test if target is alive(active)
        {
            gameObject.SetActive(false); 
            //Destroy(gameObject);
            return;
        }
        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        // stop inactive turrets being shot ////////////////////////////////////
        if(!target.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }
        //GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(gameObject, 2f);
        WaveSpawner2.enemiesAlive--;
        enemyDeaths++;
        //Destroy(target.gameObject);
        target.gameObject.SetActive(false);
        //Turret2.targetLocked = false;
        //EnemyScript.targeted = false;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
