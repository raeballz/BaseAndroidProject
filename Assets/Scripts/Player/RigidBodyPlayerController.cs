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
        /// <summary>
        /// Player choice of first or third person camera
        /// </summary>
        public PlayerCameraEnum CameraChoice;

        /// <summary>
        /// Represents if lerped or raw movement
        /// </summary>
        public MovementTypeEnum MovementType;

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
            ///Get input from player
            GetInput();

            ///Set target position by adding targetVector to current vector
            Vector3 targetPosition = new Vector3(this.gameObject.transform.position.x + targetVector.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z + targetVector.z);

            ///Smooth between current position and target position
            switch (MovementType)
            {
                ///Directly move transform to position
                case MovementTypeEnum.Raw:
                    transform.position = targetPosition;
                    break;
                ///Lerp into position, actually shite
                case MovementTypeEnum.Smoothed:
                    gameObject.transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
                    break;
            }           

            ///If the players movement magnitude is less than 1, but not 0, set it to zero for optimisation.
            if (targetVector.magnitude < 1 && targetVector.magnitude != 0)
            {
                targetVector.x = 0;
                targetVector.y = 0;
                targetVector.z = 0;
            }
            ///If it hasn't been set to zero, decelerate.
            else if (targetVector.magnitude != 0)
            {
                ///Decelerate player
                targetVector = new Vector3(targetVector.x / 2, targetVector.y, targetVector.z / 2);
            }
        }

        /// <summary>
        /// Takes input on Horizontal and Verical (keyboard)
        /// Takes input on X and Y (mouse)
        /// Positions camera and sets target position based on these inputs.
        /// </summary>
        private void GetInput()
        {
            ///Keyboard Input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            ///Multiply vector by rotation to move forward relative to where we are looking
            targetVector = gameObject.transform.rotation * new Vector3(targetVector.x + horizontal, 0, targetVector.z + vertical);

            ///Mouse Input
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
