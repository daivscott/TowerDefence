using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 420f;
    public int predictionStepsPerFrame = 6;
    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity = this.transform.forward * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 point1 = this.transform.position;
        float stepSize = 1f / predictionStepsPerFrame;
        for (float step = 0; step < 1; step += stepSize)
        {
            velocity += Physics.gravity * stepSize * Time.deltaTime;
            Vector3 point2 = point1 + velocity * stepSize * Time.deltaTime;

            //Ray ray = new Ray(point1, point2 - point1);
            //RaycastHit hitInfo;
            //if (Physics.Raycast(ray, out hitInfo, (point2 - point1).magnitude))
            //{
            //    // hitting something
            //}
            point1 = point2;
        }
        this.transform.position = point1;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            //Destroy(col.gameObject);
            col.gameObject.SetActive(false);
            WaveSpawner2.enemiesAlive--;
            Bullet.enemyDeaths++;
            Debug.Log("Hitting");
        }
            
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 point1 = this.transform.position;
        Vector3 predictedVelocity = velocity;
        float stepSize = 0.1f;
        for (float step = 0; step < 1; step += stepSize)
        {
            predictedVelocity += Physics.gravity * stepSize;
            Vector3 point2 = point1 + predictedVelocity * stepSize;
            Gizmos.DrawLine(point1, point2);
            point1 = point2;
        }
    }
}
