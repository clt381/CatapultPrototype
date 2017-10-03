using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAxisMovement : MonoBehaviour {

    [HideInInspector]
    public Vector3 inputVector;
    [HideInInspector]
    public Vector3 inputVector2;
    LaunchProjectile launchScript;
    Rigidbody rb;
    float speed = 20f;
    float velocitySpeed = 5f;
    float rotateSpeed = 50f;

    public bool rbForceMovement = true;
    public bool rbVelocityMovement = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        launchScript = GetComponent<LaunchProjectile>();
    }
	
	void Update () {
        if (rbForceMovement)
        {
            float xInput = Input.GetAxis("Horizontal");
            float yInput = Input.GetAxis("Vertical");

            //use horizontalinput only for turning
            //transform.Rotate(0f, xInput * Time.deltaTime * rotateSpeed, 0f);

            //ADD code for reversing controls when in reverse
            if (yInput < 0)     //if player is going backward
            {
                transform.Rotate(0f, -xInput * Time.deltaTime * rotateSpeed, 0f);
            }
            else if (yInput >= 0)    //if player is moving forward or standing still
            {
                transform.Rotate(0f, xInput * Time.deltaTime * rotateSpeed, 0f);
            }


            inputVector = new Vector3(0f, 0f, yInput);
            if (inputVector.magnitude > 1)
            {
                inputVector = inputVector.normalized;       //so that moving diagonally doesn't speed up the player
            }
            //remember to freeze rotation constraints in unity inspector
            //freezing constraints in rigidbody only remove rigidbody forces, can still use transform.rotate
        }
        else if (rbVelocityMovement)
        {
            float xInput = Input.GetAxis("Horizontal");
            float yInput = Input.GetAxis("Vertical");

            //take local rotation into account when applying vector
            inputVector2 = transform.right * xInput + transform.forward * yInput;

            if (inputVector2.magnitude > 1f)
            {
                inputVector2 = Vector3.Normalize(inputVector2);
            }

        }

    }

    //physics should be applied in fixedupdate which runs at "fixed timestep"
    void FixedUpdate()
    {
        if (rbForceMovement)
        {
            //transform.TransformDirection takes the object's transform's direction into account effectively converting to world to local
            rb.AddForce(transform.TransformDirection(inputVector * speed));
            //same thing, adds force relative to local object's transform
            //rb.AddRelativeForce(InputVector * speed);
        }
        else if (rbVelocityMovement)
        {
            if (inputVector2.magnitude > 0.01f)     //overriding velocity directly if there is no movement input; effects gravity
            {
                float gravityStrength = 0.3f;
                //much more exact with less acceleration and deceleration
                rb.velocity = inputVector2 * velocitySpeed + Physics.gravity * gravityStrength;
            }
        }
    }
}
