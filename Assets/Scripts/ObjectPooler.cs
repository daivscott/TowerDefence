using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    // reference to object to pool
    public GameObject objectToPool;                         /////////////moved
    // size of pool 
    public int amountToPool;                                /////////////moved
    // boolean variable to toggle pool expansion on/off
    public bool shouldExpand = true;                        /////////////moved
}

public class ObjectPooler : MonoBehaviour
{
    // make available to other scripts
    public static ObjectPooler SharedInstance;

    // create a list data structure for pooled items
    public List<GameObject> pooledObjects;

    public List<ObjectPoolItem> itemsToPool;///////////////////////////////new

    void Start()
    {
        // define list
        pooledObjects = new List<GameObject>();
        foreach(ObjectPoolItem item in itemsToPool)
        {
            // iterate through pool objects
            for (int i = 0; i < item.amountToPool; i++)
            {
                // create object for pool
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                // de-activate the object
                obj.SetActive(false);
                // add object to the list
                pooledObjects.Add(obj);
            }
        }
    }

    void Awake()
    {
        // define the shared instance
        SharedInstance = this;
    }

    public GameObject GetPooledObject(string tag)
    {
        // iterate through list of objects
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // check if current object not active
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                // return the current object
                return pooledObjects[i];
            }
        }

        foreach(ObjectPoolItem item in itemsToPool)
        {
            if(item.objectToPool.tag == tag)
            {
                // check if should expand 
                if(item.shouldExpand)
                {
                    // create a new object for the pool
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    // de-activate the object
                    obj.SetActive(false);
                    // add object to the list
                    pooledObjects.Add(obj);
                    // return the object
                    return obj;
                }

            }
        }
        
        // return null
        return null;
    }
}

            