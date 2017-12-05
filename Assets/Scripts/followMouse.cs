using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class followMouse : MonoBehaviour {

    /// <summary>
    /// The Classes Rigidbody
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// The distance the object is from the screen.
    /// </summary>
    private float distance;

    /// <summary>
    /// The location of the mouse in game space.
    /// </summary>
    private Vector3 gameObjectScreenPoint;

    /// <summary>
    /// The public accessor for the rigidbody.
    /// </summary>
    public Rigidbody Rb
    {
        get
        {
            return rb;
        }

        set
        {
            rb = this.GetComponent<Rigidbody>();
        }
    }

    /// <summary>
    /// Backing field for game object screeen point.
    /// </summary>
    public Vector3 GameObjectScreenPoint
    {
        get
        {
            return gameObjectScreenPoint;
        }

        set
        {
            gameObjectScreenPoint = value;
        }
    }


    /// <summary>
    /// Inits the ridgidbody and sets initial CamToScreen point
    /// </summary>
    void Start () {
        Rb = GetComponent<Rigidbody>();
        distance = Rb.position.z;
        GameObjectScreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        distance = GameObjectScreenPoint.z;
    }
	
	/// <summary>
    /// Ran on each frame
    /// </summary>
	void Update () {
        
	}

    /// <summary>
    /// On mouse drag event,
    /// Disable Gravity,
    /// Set distance from screen,
    /// Work out the vector for where we are moving to,
    /// Lerp to position from current.
    /// </summary>
    void OnMouseDrag()
    {
        this.Rb.useGravity = false;
        Vector3 translatedVector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        this.Rb.position = Vector3.Lerp(this.Rb.position, Camera.main.ScreenToWorldPoint(translatedVector),  0.5f);
    }

    /// <summary>
    /// On Release of object,
    /// reenable gravity.
    /// </summary>
    private void OnMouseUp()
    {
        this.Rb.useGravity = true;
    }

    /// <summary>
    /// On Scroll bring the object closer or further away based on scroll direction
    /// </summary>
    public void OnScroll()
    {
        float scrollDifferential = Input.GetAxis("Mouse ScrollWheel");
        distance += (scrollDifferential * 10);
    }
}
