using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncy : MonoBehaviour {

    public float impulseAmount;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void activateRigidBody ()
    {
        rb.useGravity = true;
        rb.AddForce(transform.forward * impulseAmount);
    }
}
