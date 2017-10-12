using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    GetAxisMovement movementScript;
    float verticalRotation = 0f;    //store vertical mouse eulerangle for x axis
    public bool mouseLook = false;
    public bool mouseRotateAround = true;
    public Transform defaultCameraPosition; //where the camera should default to when moving

    // Use this for initialization
    void Start() {
        movementScript = GetComponent<GetAxisMovement>();
    }

    // Update is called once per frame
    void Update() {

        float mouseSensitivity = 30f;
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (mouseLook)
        {
            //transform.Rotate(-mouseY, 0f, 0f);  //-mouseY to deinvert camera y rotation
            //transform.parent to move up inheritence hierarchy to access parent object
            transform.parent.Rotate(0f, mouseX * Time.deltaTime * mouseSensitivity, 0f);    //mouseY looks up and down but mouseX rotates cube

            //more complex mouse look vertical rotation that clamps rotation
            verticalRotation -= mouseY * Time.deltaTime * mouseSensitivity; //-= mouseY to deinvert
            verticalRotation = Mathf.Clamp(verticalRotation, -85f, 85);     //prevents player from looking straight up or down

            //correction pass to unroll camera every frame; manually override z eulerangle to 0
            //when applying changes to vector3, must use new vector3
            transform.localEulerAngles = new Vector3(verticalRotation, transform.localEulerAngles.y, 0f);

            //if user clicks in game window, lock the mouse
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.visible = false; //hide the mouse cursor
                Cursor.lockState = CursorLockMode.Locked;   //lock mouse cursor to center of the window
                //press escape to exit window lock
            }
        }

        ReturnToDefaultPosition();

        if (mouseRotateAround)
        {
            //setting this to look at cameramanager instead of player object prevents stuttering but and camera moving away
            transform.LookAt(transform.parent);     //always look at parent object camera manager
            transform.Rotate(0f, mouseX, 0f);
        }

        
    }

    void ReturnToDefaultPosition()
    {
        //to override mouselook camera control with directional key movement camera control
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)     //if there is any movement
        {
            mouseRotateAround = false;      //disable mouselook movement
            Transform curCameraPosition = GetComponentInChildren<Camera>().gameObject.transform;
            curCameraPosition.position = defaultCameraPosition.position;
            curCameraPosition.rotation = defaultCameraPosition.rotation;
        }
        else
        {
            mouseRotateAround = true;
        }
    }
}
