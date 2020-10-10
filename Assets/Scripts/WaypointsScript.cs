using UnityEngine;

public class WaypointsScript : MonoBehaviour
{
    // create a public static array of waypoints 
    public static Transform[] points;

    // awake is called before the first frame update
    void Awake()
    {
        // create an array for all objects within the Waypoints object
        points = new Transform[transform.childCount];

        // iterate through the array
        for (int i = 0; i < points.Length; i++)
        {
            // set each object found to the current location in the array
            points[i] = transform.GetChild(i);
        }
    }
}
