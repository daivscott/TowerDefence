//If anyone doesn't want every turret to have to store a potentially huge list of enemies, you can use rays and raycasting to avoid the need to save any list of potential targets, here's a script you might like to attach this TO YOUR PIVOT BASE (i.e at the origin pivot point of your turret head):
// sorry for typos! I had to type this by hand because google blocks copy-and-paste into YouTube when I use Firefox

using UnityEngine;

public class FindTargets : MonoBehaviour
{
    public float searchRadius = 10f;  // eyesight distance of turret
    public int shootRate = 10;  // number of "looks" per second when not targetting yet
    public float rotationSpeed = 45;  // number of degrees-per-second of rotation when searching

    Transform target;  // the enemy to follow, initially null of course
    float timeToShoot;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private bool shooting = false;


    void Start()
    {
        timeToShoot = 1 / shootRate;
        target = null;
    }

    void Update()
    {
        if (target == null)
        {
            transform.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        }
        else if(shooting)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
    }

    void FixedUpdate()
    {
        timeToShoot -= Time.fixedDeltaTime;
        if (target == null && timeToShoot <= 0)
        {
            timeToShoot = 1 / shootRate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, searchRadius))
        {
            if (hit.transform.tag == "Player")
            {                
                target = hit.transform;
                float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);
                if (distanceToEnemy <= searchRadius)
                {
                    shooting = true;
                    GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    Bullet bullet = bulletGameObject.GetComponent<Bullet>();

                    if (bullet != null)
                    {
                        bullet.Seek(target.gameObject);
                    }
                    target = null;
                }
            }
            // obviously in this code, you need to tag all your targettable objects with "Enemy"
        }
        else
        {
            target = null;
            shooting = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}