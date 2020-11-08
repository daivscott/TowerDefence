using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretMovementScript : MonoBehaviour
{
    public GameObject projectile;
    public GameObject firePoint;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float pitchRange = 90f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public int cooldown = 20;
    int currentCount = -1;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X") * Time.deltaTime;
        pitch -= speedV * Input.GetAxis("Mouse Y") * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, -pitchRange, pitchRange);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetMouseButton(0) && currentCount < 0)
        {
            Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
            currentCount = cooldown;
        }
        currentCount--;
    }
}
