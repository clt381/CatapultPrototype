using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {

    CameraManager cameraManager;
    GameObject catapult;
    public RotateAround rotateScript;
    bool hit;       //to prevent one prefab from triggering multiple camera cuts
    bool canExit;

    void Start()
    {
        catapult = GameObject.FindGameObjectWithTag("Player");      //finding the cameramanager script on the player
        cameraManager = catapult.GetComponent<CameraManager>();
        canExit = true;
    }

    void Update()
    {
        OnClickExit();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (hit == false)
        {
            //if the tag on the hit collider gameobject is "Target", then disable the followball camera and enable the rotate camera
            if (coll.gameObject.CompareTag("Target"))
            {
                hit = true;     //so the ball can't trigger two things at once
                rotateScript = coll.gameObject.GetComponent<RotateAround>();        //getting the RotateAround component on the hit object
                rotateScript.startRotate = true;                                    //start rotating the gameobject camera is attached to
                if (coll.gameObject.name == "Statue1")      //passing an integer to CameraManager RotateCamera function to differentiate between cameras
                {
                    cameraManager.RotateCamera(1);
                }
                else if (coll.gameObject.name == "Statue2")
                {
                    cameraManager.RotateCamera(2);
                }
                else if (coll.gameObject.name == "Statue3")
                {
                    cameraManager.RotateCamera(3);
                }
                else if (coll.gameObject.name == "Statue4")
                {
                    cameraManager.RotateCamera(4);
                }

            }
            //if the tag on the hit collider gameobject is "Missed", then disable followball camera and enable the default camera
            else if (coll.gameObject.CompareTag("Missed"))
            {
                Debug.Log("I missed the target");
                hit = true;
                cameraManager.DefaultCamera();
            }
            else if (coll.gameObject.CompareTag("Ground"))
            {
                Debug.Log("I hit the ground");
                hit = true;
                cameraManager.DefaultCamera();
            }
        }
    }

    void OnClickExit()
    {
        if (canExit)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                canExit = false;
                Camera camera = GetComponentInChildren<Camera>();
                camera.enabled = false;
            }
        }
    }
}
