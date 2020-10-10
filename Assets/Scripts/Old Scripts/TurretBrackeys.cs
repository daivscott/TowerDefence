using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBrackeys : MonoBehaviour
{
    // 
    private Transform target;

    [Header("Attributes")]
    public float range = 15;
    public float fireRate = 1f;
    public float shotCooldown = 0f;

    [Header("Attributes")]

    public string enemyTag = "Player";

    public Transform rotator;
    public float rotateSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        Collider[] enemies = Physics.OverlapSphere(transform.position, range);
        float shortestDistance = Mathf.Infinity;
        Collider nearestEnemy = null;
        foreach(Collider enemy in enemies)
        {
            if (enemy.gameObject.tag != enemyTag)
            {
                return;
            }
            Debug.Log("EnemyFound");
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
                Debug.Log("nearesy enemy set to " + nearestEnemy.gameObject.name);
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            Debug.Log("target aquired");
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }
        
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotator.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        rotator.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(shotCooldown <= 0f)
        {
            Shoot();
            shotCooldown = 1f / fireRate;
        }

        shotCooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
