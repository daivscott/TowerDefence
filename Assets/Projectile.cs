using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // declare and define speed variable
    public float speed = 100f;//420f;
    // declare and define amount of steps per frame variable
    public int predictionStepsPerFrame = 6;
    // declare velocity variable
    public Vector3 velocity;

    public float gravityMultiplier = 1000f;

    //public float debugSS = 1f;

    void OnEnable()
    {        
        // define velocity to always move forward at set speed
        velocity = this.transform.forward * speed;
    }

    // FixedUpdate used to handle physics calculations
    void FixedUpdate()
    {
        // send ray to centre of screen
        var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.height / 2, Screen.width / 2));

        RaycastHit hitPoint;

        // cast ray out set distance
        if (Physics.Raycast(ray, out hitPoint, 1000.0f))
        {
            // set gameObject face towards the hitpoint
            transform.LookAt(hitPoint.point);
        }

        // declare vector3 and define with gameObjects current position
        Vector3 point1 = this.transform.position;

        // declare and define stepSize
        float stepSize = 1f / predictionStepsPerFrame;

        // iterate through steps
        for (float step = 0; step < 1; step += stepSize)
        {
            // increment velocity with gravity
            velocity += (Physics.gravity / gravityMultiplier) * stepSize * Time.fixedDeltaTime;
            // declare and define predicted target location
            Vector3 point2 = point1 + velocity * stepSize * Time.fixedDeltaTime;

            Ray hitRay = new Ray(point1, point2 - point1);
            RaycastHit hitInfo;
            if (Physics.Raycast(hitRay, out hitInfo, (point2 - point1).magnitude))
            {
                // hitting something
                //if (hitInfo.transform.gameObject.tag != "Enemy")
                    //gameObject.SetActive(false);
            }

            // set point1 to new predicted location
            point1 = point2;
        }

        // set gameObjects position to predicted location
        this.transform.position = point1;
    }

    // check if othe object with collider enters this collider
    private void OnTriggerEnter(Collider col)
    {
        if(gameObject.transform.position.y < 0f)
        {
            gameObject.SetActive(false);
        }
        // check the tag matches an object tagged as "Enemy"
        if(col.gameObject.tag == "Enemy")
        {
            // reduce health
            col.GetComponentInChildren<HealthScript>().LoseHealth(35);
            // destroy this projectile
            gameObject.SetActive(false);
            // returm once destroy complete
            return;
        }   
        else
        {
            // destroy this projectile anyway
            gameObject.SetActive(false);
        }
    }
    void Update()
    {
      if(gameObject.transform.position.y < -10f)
        {
            // destroy this projectile anyway
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
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
