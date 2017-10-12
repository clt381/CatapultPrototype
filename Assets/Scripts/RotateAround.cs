using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

    Vector3 rotateY = new Vector3(0, 5f, 0);
    float speed = 10f;
    public Camera rotatingCamera;        //change for each new statue
    public Transform lookAt;        //change for each new statue
    public bool startRotate = false;
    GameObject player;
    CameraManager cameraManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraManager = player.GetComponent<CameraManager>();
        rotatingCamera = GetComponentInChildren<Camera>();
    }

	void Update () {
        if (startRotate)
        {
            rotatingCamera.gameObject.transform.LookAt(lookAt);
            rotatingCamera.gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))     //so that player can exit rotating mode
        {
            cameraManager.DefaultCamera();      
        }
	}
}
