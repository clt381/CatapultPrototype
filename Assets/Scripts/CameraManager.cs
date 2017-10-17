using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public Camera followBallCamera;
    public Camera defaultCamera;
    public Camera[] rotateCameras;  //array of rotating cameras

    void Start()
    {
        
        DefaultCamera();
    }

    //to follow the ball on ball instantiation
    public void FollowBallCamera()
    {
        followBallCamera.enabled = true;
    }

    //to return to default camera
    public void DefaultCamera()
    {    
        foreach (Camera camera in rotateCameras)        //disable all cameras in rotatecameras array
        {
            camera.enabled = false;
        }
        defaultCamera.enabled = true;
    }

    //to go to rotate camera after ball has hit target
    public void RotateCamera(int CameraNumber)
    {
        followBallCamera.enabled = false;
        defaultCamera.enabled = false;
        if (CameraNumber == 1)
        {
            rotateCameras[0].enabled = true;
        }
        else if (CameraNumber == 2)
        {
            rotateCameras[1].enabled = true;
        }
        else if (CameraNumber == 3)
        {
            rotateCameras[2].enabled = true;
        }
        else if (CameraNumber == 4)
        {
            rotateCameras[3].enabled = true;
        }
        
    }
}
