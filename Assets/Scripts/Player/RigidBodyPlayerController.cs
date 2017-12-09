using UnityEngine;
using Assets.Scripts.Enums;
using Hephaestus.CamaeraManagement;
using System;
using System.Linq;
using System.Collections.Generic;


namespace Hephaestus.Player.Controller{

    [RequireComponent(typeof(Rigidbody))]
    public class RigidBodyPlayerController : MonoBehaviour
    {
        #region Fields
        public PlayerCameraEnum CameraChoice;
        private PersonalCameraController cameraController;
        private Rigidbody rb;
        private Event e;

        private Vector3 targetVector;
        private bool left;
        private bool right;
        private bool forward;
        private bool backward;

        public float playerSpeed = 2;
        ////private Camera selectedCamera;
        ////private Camera externalCamera;
        #endregion

        /// <summary>
        /// Player Control Set Up
        /// </summary>
        public void Start () {
            InitaliseFields();
            cameraController.SetPlayerPerspective(CameraChoice);
	    }

        /// <summary>
        /// Called once a frame
        /// </summary>
        public void Update()
        {
            Vector3 targetPosition = new Vector3(this.gameObject.transform.position.x + (targetVector.x * 5), this.gameObject.transform.position.y, this.gameObject.transform.position.z + (targetVector.z * 5));
            gameObject.transform.position = Vector3.Lerp(transform.position, targetPosition, 0.01f);
            targetVector = new Vector3(targetVector.x / 2, targetVector.y, targetVector.z / 2);
        }
        
        private void OnGUI()
        {
            e = Event.current;
            if (e.isKey)
            {
                KeyCode input = e.keyCode;
                switch (input)
                {
                    case KeyCode.A:
                        this.targetVector = new Vector3(-3, targetVector.y, targetVector.z);
                        break;
                    case KeyCode.D:
                        this.targetVector = new Vector3(3, targetVector.y, targetVector.z);
                        break;
                    case KeyCode.W:
                        this.targetVector = new Vector3(targetVector.x, targetVector.y, 3);
                        break;
                    case KeyCode.S:
                        this.targetVector = new Vector3(targetVector.x, targetVector.y, -3);
                        break;
                }
            }
        }

        /// <summary>
        /// Gets all Cameras from parent,
        /// Sets rigidBody
        /// </summary>
        private void InitaliseFields()
        {
            rb = GetComponentInChildren<Rigidbody>();
            GameObject gameObjRef = this.gameObject;
            cameraController = new PersonalCameraController(gameObjRef);
            targetVector = gameObject.transform.position;
        }
    }
}
