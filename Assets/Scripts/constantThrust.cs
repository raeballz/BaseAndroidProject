using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Applies constant thrust in a randomised direction.
/// </summary>
public class constantThrust : MonoBehaviour
{
    private Rigidbody rb;
    private UnityEngine.Random rand;
    private int thrustDirection;
    private List<Vector3> directonList = new List<Vector3>();
    private bool isThrusting = false;
    public float thrustForce;

    /// <summary>
    /// Sets up the random direction list, then sets the ridgidbody reference.
    /// </summary>
    void Start () {
        setupRandomDirection();
        rb = this.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Initialises the random direction list
    /// </summary>
    private void setupRandomDirection()
    {
        directonList.Add(Vector3.forward);
        directonList.Add(Vector3.down);
        directonList.Add(Vector3.left);
        directonList.Add(Vector3.right);
        directonList.Add(Vector3.back);
        directonList.Add(Vector3.up);
    }

    /// <summary>
    /// Applies constant force in a direction specified within <see cref="onClick"/>
    /// </summary>
    void Update () {
        if (isThrusting)
        {
            rb.AddForce(directonList[thrustDirection] * thrustForce, ForceMode.Force);
        }
	}

    /// <summary>
    /// On Click Event, switch gravity on, toggle thrusting, and set thrust direction randomly. 
    /// </summary>
    public void onClick()
    {
        rb.useGravity = true;
        this.isThrusting = !isThrusting;
        thrustDirection = UnityEngine.Random.Range(0, 5);
    }
}
