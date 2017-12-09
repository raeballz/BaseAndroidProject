using UnityEngine;
using Assets.Scripts.Enums;
using Hephaestus.CamaeraManagement;
using System;
using System.Collections.Generic;


namespace Hephaestus.Player.Controller{

    [RequireComponent(typeof(Rigidbody))]
    public class RigidBodyPlayerController : MonoBehaviour
    {
        #region Fields
        public PlayerCameraEnum CameraChoice;
        private PersonalCameraController cameraController;
        private Rigidbody rb;
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
        }
    }
}
