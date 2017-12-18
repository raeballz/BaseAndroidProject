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
        public PlayerCameraEnum CameraChoice;

        #region Private Fields
        private PersonalCameraController cameraController;
        private Rigidbody rb;
        private Vector3 targetVector;
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
        public void FixedUpdate()
        {
            GetInput();
            ///Set target position by adding targetVector to current vector
            Vector3 targetPosition = new Vector3(this.gameObject.transform.position.x + targetVector.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z + targetVector.z);

            ///Smooth between current position and target position
            gameObject.transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);

            ///Decelerate player
            targetVector = new Vector3(targetVector.x / 2, targetVector.y, targetVector.z / 2);
        }

        private void GetInput()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            targetVector = new Vector3(targetVector.x + horizontal, 0, targetVector.z + vertical);

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            gameObject.transform.Rotate(0, mouseX, 0);
            AxisEnum xAxis = AxisEnum.x;
            cameraController.SetCameraRotation(xAxis, mouseY, CameraChoice);
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
