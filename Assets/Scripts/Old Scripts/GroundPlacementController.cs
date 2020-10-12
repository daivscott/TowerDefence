using System;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeableObjectPrefabs;

    private GameObject currentPlaceableObject;

    private float mouseWheelRotation;
    private int currentPrefabIndex = -1;

    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
            // right-click mouse to destroy current selection ///////////////////////////////////////
            if(Input.GetMouseButtonDown(1))
            {
                DestroyCurrentPrefab();
            }
        }
    }

    private void HandleNewObjectHotkey()
    {
        for (int i = 0; i < placeableObjectPrefabs.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
            {
                // Call function //////////////////////////////////////////////////////////////////////
                PlacePrefab(i);

                break;
            }
        }
    }

    public void PlacePrefab(int i)
    {
        if (PressedKeyOfCurrentPrefab(i))
        {
            // call function //////////////////////////////////////////////////////
            DestroyCurrentPrefab();
            currentPrefabIndex = -1;
        }
        else
        {
            if (currentPlaceableObject != null)
            {
                Destroy(currentPlaceableObject);
            }

            currentPlaceableObject = Instantiate(placeableObjectPrefabs[i]);
            currentPrefabIndex = i;
        }
    }

    public void DestroyCurrentPrefab()
    {
        Destroy(currentPlaceableObject);
    }

    private bool PressedKeyOfCurrentPrefab(int i)
    {
        return currentPlaceableObject != null && currentPrefabIndex == i;
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void RotateFromMouseWheel()
    {
        Debug.Log(Input.mouseScrollDelta);
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableObject = null;
        }
    }
}