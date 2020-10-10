using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret2 : MonoBehaviour
{
    // 
    private GameObject target;

    [Header("Attributes")]
    public float range = 15;
    public float fireRate = 1f;
    public float shotCooldown = 0f;

    [Header("Attributes")]

    public string enemyTag = "Enemy";

    //public Transform rotator;
    public float rotateSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public static bool targetLocked;
    public GameObject gun;
    public SphereCollider gunCollider;

    //ObjectPooler objectPooler;

    // Start is called before the first frame update
    void Start()
    {
        gunCollider.radius = range;
        //objectPooler = ObjectPooler.objectPoolInstance;
    }



    // Update is called once per frame
    void Update()
    {
        gunCollider.radius = range;

        // if not a valid target return out of update
        if (!target)
        { 
            return;
        
        //if (!targetLocked && !EnemyScript.targeted)
        //{
        //    return;
        }
        else
        {
            //// point to target
            //gun.transform.LookAt(target.transform);

            // get direction to target 
            Vector3 dir = target.gameObject.transform.position - transform.position;
            // create quaternion variable and set to to target
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            // convert rotataion to euler angle using lerp to turn Gun at set speed
            Vector3 rotation = Quaternion.Lerp(gun.gameObject.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
            // set Gun part of turret to rotation
            gun.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
        }        

        if (shotCooldown <= 0f)
        {
            Shoot();
            shotCooldown = 1f / fireRate;
        }

        shotCooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        //GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //GameObject spawnedObject = objectPool.SpawnFromPool("Bullet", firePoint.position, firePoint.rotation);
        GameObject spawnedObject = ObjectPooler.SharedInstance.GetPooledObject("PlayerProjectile");
        if (spawnedObject != null)
        {
            spawnedObject.transform.position = firePoint.position;
            spawnedObject.transform.rotation = firePoint.rotation;
            spawnedObject.SetActive(true);
            Bullet bullet = spawnedObject.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.Seek(target);
            }
        }

        if (spawnedObject)
        {
            
        }
    }

    private void OnDrawGizmos()
    {
        // set gizmo colour
        Gizmos.color = Color.red;
        // set gizmo equal to range
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void OnTriggerStay(Collider collisionObject)
    {
        //Debug.Log("triggered");
        if (collisionObject.tag == enemyTag || !collisionObject.gameObject.activeSelf)
        {
            //  set targetLocked variable to lock on the target
            if(target) 
            {
                targetLocked = true;
            }
            else
            {
                targetLocked = false;
            }

            // if target not locked
            if((targetLocked == false))// && (!EnemyScript.targeted))
            {
                // set the target to detected object
                target = collisionObject.gameObject;

                // set targeted to true so only one turret can target
                //EnemyScript.targeted = true;
                ////Debug.Log("Enemy = " + target.gameObject.name);
            }            
        }
    }
}

