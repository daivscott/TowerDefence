using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    // create GameObject to store reference to placeable prefab
    [SerializeField]
    private GameObject placeablePrefab;

    // create a new hotkey object
    [SerializeField]
    private KeyCode newObjectHotkey = KeyCode.Alpha1;

    // creat temporary GameObject store 
    private GameObject currentPlaceableObject;
    // variable to store mouse wheel rotation
    private float mouseWheelRotation;

    void Update()
    {
        // call function
        HandleNewObjectHotKey();

        // check if  current placeable object is active
        if(currentPlaceableObject != null)
        {
            // call function if active
            MoveCurrentPlaceableObject();
            // call function if active
            RotateFromMouseWheel();
            // call function if active
            ReleaseIfClicked();
        }
    }

    // place the object
    private void ReleaseIfClicked()
    {
        // check if left mouse button is pressed
        if(Input.GetMouseButtonDown(0))
        {
            // set to null
            currentPlaceableObject = null;
        }
    }

    // rotate the object with scroll wheel
    private void RotateFromMouseWheel()
    {
        // set varable to be incremented by the amount the scroll wheel turns
        mouseWheelRotation += Input.mouseScrollDelta.y;
        // rotate the object to match the scroll wheel rotation
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    // set placeable object at cursor location
    private void MoveCurrentPlaceableObject()
    {
        // cast a ray from the mouse pointer into the world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // get the raycast hit information
        RaycastHit hitInfo;

        // check if raycast hits anything
        if(Physics.Raycast(ray, out hitInfo))
        {
            // set placeable objects position equal to the raycast hit location
            currentPlaceableObject.transform.position = hitInfo.point;
            // set the placeable objects rotation in relation to the normal of placed location
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    // hotkey functionality
    private void HandleNewObjectHotKey()
    {
        // check if hotkey is pressed
        if(Input.GetKeyDown(newObjectHotkey))
        {
            PlacePrefab();
        }
    }

    public void PlacePrefab()
    {
        // check if the placeable prefab is active
        if (currentPlaceableObject == null)
        {
            // if not active create a placeable object
            currentPlaceableObject = Instantiate(placeablePrefab);
        }
        else
        {
            // if active destroy the current placeable object
            Destroy(currentPlaceableObject);
        }
    }
}
