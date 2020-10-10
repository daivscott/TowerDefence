using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    // set variable to store target gameobject
    private GameObject target;
    // referencing the Gun object
    public GameObject gun;
    // reference the sphere collider component
    public SphereCollider gunCollider;
    // refernce the CannonTurrets firepoint
    public Transform firePoint;
    // set range for turret
    public float range = 15f;
    // turret rotation speed
    public float rotateSpeed = 10f;
    // turret firerate
    public float fireRate = 1f;
    // turret shot cooldown time
    public float shotCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // set the collider radius equal to the range
        gunCollider.radius = range;
    }

    // Update is called once per frame
    void Update()
    {
        // set radius of sphere collider equal to range
        gunCollider.radius = range;

        // return out of update if not a target
        if(!target)
        {
            return;
        }
        else
        {
            // get distance to target
            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            if(distanceToTarget <= range && target.activeSelf) /////// added AND to stop firing at inactive tower /////////////////////////////////////////
            {
                // get direction to target 
                Vector3 dir = target.gameObject.transform.position - transform.position;
                // create quaternion lookrotation variable and set to to target
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                // convert rotataion to euler angle using lerp to turn Gun at set speed
                Vector3 rotation = Quaternion.Lerp(gun.gameObject.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
                // set Gun part of turret to converted rotation
                gun.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

                // only shoot if shotCoolDown less than or equal to 0
                if (shotCooldown <= 0f)// && target.gameObject.activeSelf == true)
                {
                    Shoot();
                    // reset shotCooldown in realtion to firerate
                    shotCooldown = 1f / fireRate;
                }
                // decrement the shotCooldown over time
                shotCooldown -= Time.deltaTime;                
            }            
        }        
    }

    void Shoot()
    {
        // define a game object and store the returned pool object with the supplied tag
        GameObject spawnedObject = ObjectPooler.SharedInstance.GetPooledObject("PlayerProjectile");

        // check the bulletGameObject exists
        if (spawnedObject != null)
        {
            // set bulletGameObject position
            spawnedObject.transform.position = firePoint.position;
            // set bulletGameObject rotation
            spawnedObject.transform.rotation = firePoint.rotation;
            // set bulletGameObject active
            spawnedObject.SetActive(true);
        }

        // access the bulletScript of the referenced bullet
        Bullet bulletScript = spawnedObject.GetComponent<Bullet>();

        // only complete if bulletScript exists
        if (bulletScript != null)
        {
            // call bulletScript function to set target
            bulletScript.Seek(target);
        }
    }

    private void OnDrawGizmos()
    {
        // set gizmo colour to red
        Gizmos.color = Color.red;
        // set gizmo radius to range
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void OnTriggerStay(Collider collidingObject)
    {
        // set the target to detected object
        target = collidingObject.gameObject;

        //Debug.Log("Enemy = " + collidingObject.name);
    }
}
