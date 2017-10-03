using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour {


    float force = 50f;
    float strength = 2f;
    Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(0, 0, force * strength, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void Update () {
        TrajectoryTrail();
		
	}

    void TrajectoryTrail()
    {

        
        rb.AddForce(Physics.gravity * (strength - 1));
    }
}
