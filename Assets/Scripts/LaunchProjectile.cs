using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour {

    //variables for launch force
    float launchForce = 0f;
    float calculatedForce = 0f;
    float launchIncreaseSpeed = 30f;
    float maxLaunchForce = 100f;
    float minLaunchForce = 20f;
    bool isLoading = false;     //to disable certain functions while catapult is readying to fire e.g. movement and angle change
    public bool isMoving = false;

    //variables for launch angle
    float launchRotIncreaseSpeed = 20f;     //degrees per second
    //the lower the angle, the higher the catapult fires
    float maxLaunchRot = -60f;
    float minLaunchRot = -10f;

    public Transform launchPoint;
    public GameObject Projectile;
    public GameObject TrailBall;

    CameraManager cameraManager;

    void Start()
    {
        cameraManager = GetComponent<CameraManager>();
    }

	void Update () {

        CalculateLaunch();
        //Debug.Log(launchForce);
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (calculatedForce > minLaunchForce && calculatedForce < maxLaunchForce)
            {
                launchForce = calculatedForce;
                calculatedForce = 0;        //reseting calculated force for next calculation
                //ADD catapult firing animation here
                FireProjectile();
            }
            else if (calculatedForce > minLaunchForce && calculatedForce > maxLaunchForce)
            {
                launchForce = maxLaunchForce;
                calculatedForce = 0;        //reseting calculated force for next calculation
                //ADD catapult firing animation here
                FireProjectile();
            }
            else if (calculatedForce < minLaunchForce)
            {
                launchForce = 0;
                calculatedForce = 0;
            }

            Debug.Log(launchForce);
            
        }

        //TrajectoryTrail();
	}

    void CalculateLaunch()
    {
        //launch force code
            if (Input.GetKey(KeyCode.Space))        //while space is held down
            {
                isLoading = true;
                calculatedForce += launchIncreaseSpeed * Time.deltaTime;         //increase launch force by launch increase speed per second
                                                                                 //ADD catapult loading up animation here
            }
            else
            {
                isLoading = false;
            }

        //launch rotation code
        if (isLoading == false)         //can't change angle of fire while moving or loading projectile
        {
            if (Input.GetKey(KeyCode.E))
            {
                //NOTE: don't use vector3.up or down shorthands; otherwise y and z rotations are affected when parent rotates
                launchPoint.transform.Rotate(new Vector3 (-1, 0, 0) * launchRotIncreaseSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                launchPoint.transform.Rotate(new Vector3(1, 0, 0) * launchRotIncreaseSpeed * Time.deltaTime);
            }
        }
        
    }

    void FireProjectile()           //specify type of projectile in parameters
    {
        GameObject projectileClone = Instantiate(Projectile, launchPoint.position, launchPoint.rotation) as GameObject;
        Rigidbody rb = projectileClone.GetComponent<Rigidbody>();
        rb.AddRelativeForce(0, 0, launchForce, ForceMode.Impulse);        //so force is applied in the rigidbody's local direction
        //NOTE: make sure to use addrelativeforce instead of transform.TransformDirection
        cameraManager.followBallCamera = projectileClone.GetComponentInChildren<Camera>();
        cameraManager.FollowBallCamera();
    }

    void TrajectoryTrail()
    {
        GameObject trailClone = Instantiate(TrailBall, launchPoint.position, launchPoint.rotation) as GameObject;

    }
}
