using UnityEngine;


public class CameraController : MonoBehaviour
{
    // camera zoom amount
    public float cameraZoomAmount = 60;
    // camera zoom sensitivity
    public float zoomSensitivity = 50;
    // camera zoom smoothnes
    public float zoomSmoothness = 5;

    // field of view range
    public float minFOV = 20;
    public float maxFOV = 60;

    // camera movement speed
    public float panSpeed = 30f;
    // mouse control border size
    public float panMouseControlBorderSize = 10f;

    // camera control toggle
    private bool cameraControl = false;

    // reference a camera component
    Camera cam;
    void Start()
    {
        // set camera component to the camera this script is attache to
        cam = GetComponent<Camera>();
        // set camer zoom equal to the field of view
        cameraZoomAmount = cam.fieldOfView;
    }


    void Update()
    {
        // allow camera zoom to be set with scroll wheel 
        cameraZoomAmount -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        // clamp the zoom to a fixed range
        cameraZoomAmount = Mathf.Clamp(cameraZoomAmount, minFOV, maxFOV);
        // use lerp to smooth camera zoom
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, cameraZoomAmount, Time.deltaTime * zoomSmoothness);

        // toggle camera control on/off
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // switch camera control
            cameraControl = !cameraControl;
        }

        // return if no need to control the camera
        if (!cameraControl)
        {
            return;
        }

        // move camera forward
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panMouseControlBorderSize)
        {            
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);           
        }

        // move camera backward
        if (Input.GetKey("s") || Input.mousePosition.y <= panMouseControlBorderSize)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        // move camera right
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panMouseControlBorderSize)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        // move camera left
        if (Input.GetKey("a") || Input.mousePosition.x <= panMouseControlBorderSize)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
    }
}