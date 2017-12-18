using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

namespace Hephaestus.CamaeraManagement
{
    public class PersonalCameraController : MonoBehaviour
    {
        public PlayerCameraEnum CameraChoice;
        ////private Camera selectedCamera;
        private Camera[] childCameras;
        private Camera firstPersonCamera;
        private Camera thirdPersonCamera;

        /// <summary>
        /// Initialises the camera controller variables.
        /// </summary>
        /// <param name="player"></param>
        public PersonalCameraController(GameObject player)
        {
            GetAllChildCameras(player);
            AssignNamedPublicCameras(childCameras);
        }

        /// <summary>
        /// Public Accessor for FirstPersonCamera
        /// </summary>
        public Camera FirstPersonCamera
        {
            get { return firstPersonCamera; }
            set { firstPersonCamera = value; }
        }

        /// <summary>
        /// Public Accessor for Third PersonCamera
        /// </summary>
        public Camera ThirdPersonCamera
        {
            get { return thirdPersonCamera; }
            set { thirdPersonCamera = value; }
        }

        public void SetPlayerPerspective(PlayerCameraEnum cameraChoice)
        {
            switch (cameraChoice)
            {
                case PlayerCameraEnum.FPSCamera:
                    if (firstPersonCamera != null)
                    {
                        Debug.Log("FirstPersonCamera Selected.");
                        deactivateAllCameras(childCameras);
                        firstPersonCamera.enabled = true;
                    }
                    else
                    {
                        Debug.LogWarning("Selected FirstPersonCamera, but no FirstPersonCamera to assign to.");
                    }
                    break;

                case PlayerCameraEnum.ThirdPersonCamera:
                    if (firstPersonCamera != null)
                    {
                        Debug.Log("ThirdPersonCamera Selected.");
                        deactivateAllCameras(childCameras);
                        thirdPersonCamera.enabled = true;
                    }
                    else
                    {
                        Debug.LogWarning("Selected ThirdPersonCamera, but no ThirdPersonCamera camera to assign to.");
                    }
                    break;

                default:
                    ///TODO: Set default if no selection
                    ///TODO: Log No Camera Set
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angleToChangeTo"></param>
        /// <param name="camera"></param>
        public void SetCameraRotation(AxisEnum axis, float angleToChangeTo, PlayerCameraEnum camera)
        {
            Camera cameraToChange = GetCameraFromEnum(camera);
            switch (axis)
            {
                case AxisEnum.x:
                    cameraToChange.transform.Rotate(angleToChangeTo, 0, 0);
                    break;
                case AxisEnum.y:
                    cameraToChange.transform.Rotate(0, angleToChangeTo, 0);
                    break;
                case AxisEnum.z:
                    cameraToChange.transform.Rotate(0, 0, angleToChangeTo);
                    break;
            }
        }

        /// <summary>
        /// Sets the Camera Manager's Child Cameras to the 
        /// child cameras of the object handed to it.
        /// </summary>
        /// <param name="parent"></param>
        private void GetAllChildCameras(GameObject parent)
        {
            childCameras = parent.transform.GetComponentsInChildren<Camera>();
        }

        /// <summary>
        /// Deactivates all cameras handed into class.
        /// </summary>
        /// <param name="cameras"></param>
        private void deactivateAllCameras(Camera[] cameras)
        {
            foreach (Camera camera in cameras)
            {
                if (camera != null)
                {
                    camera.enabled = false;
                }
            }
        }

        /// <summary>
        /// Assigns the named cameras from the cameras in array handed to it
        /// </summary>
        private void AssignNamedPublicCameras(Camera[] cameras)
        {
            foreach (Camera camera in cameras)
            {
                switch (camera.name)
                {
                    case "FirstPersonCamera":
                        FirstPersonCamera = camera;
                        break;
                    case "ThirdPersonCamera":
                        ThirdPersonCamera = camera;
                        break;
                    //case "ExternalCamera":
                    //externalCamera = camera;
                    //break;
                    default:
                        Debug.Log("Can't assign camera, no enumerator or init check for name.");
                        break;
                }
            }
        }

        /// <summary>
        /// Returns a camera from the enum handed to it
        /// </summary>
        /// <param name="choice"></param>
        /// <returns></returns>
        private Camera GetCameraFromEnum(PlayerCameraEnum choice)
        {
            switch (choice)
            {
                case PlayerCameraEnum.FPSCamera:
                    return FirstPersonCamera;
                    break;
                case PlayerCameraEnum.ThirdPersonCamera:
                    return ThirdPersonCamera;
                    break;
                //case "ExternalCamera":
                //externalCamera = camera;
                //break;
                default:
                    Debug.Log("Can't assign camera, no enumerator or init check for name.");
                    return new Camera();
                    break;
            }
        }
    }
}
