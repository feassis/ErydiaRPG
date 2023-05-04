using System;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCan : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerObj;
    [SerializeField] private Transform combatLookAt;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CameraStyle cameraStyle;
    [SerializeField] private List<DesiredCameraControler> cameraControllers;

    [Header("Movement Setup")]
    public float rotationSpeed;

    [Serializable]
    private struct DesiredCameraControler
    {
        public CameraStyle CameraStyle;
        public GameObject CinemachineCamera;
    }

    private void Awake()
    {
        foreach (var controller in cameraControllers)
        {
            controller.CinemachineCamera.SetActive(controller.CameraStyle == cameraStyle);
        }
    }

    private void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, 
            player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if(cameraStyle == CameraStyle.Basic || cameraStyle == CameraStyle.Topdown)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 inputDir = orientation.forward * verticalInput +
                orientation.right * horizontalInput;

            if (inputDir == Vector3.zero)
            {
                return;
            }

            playerObj.forward = Vector3.Slerp(playerObj.forward,
                inputDir.normalized, Time.deltaTime * rotationSpeed);
            return;
        }
        else if (cameraStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x,
            combatLookAt.position.y, transform.position.z);

            orientation.forward = dirToCombatLookAt.normalized;
            playerObj.forward = dirToCombatLookAt.normalized;
        }

    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        cameraStyle = newStyle;

        foreach (var controller in cameraControllers)
        {
            controller.CinemachineCamera.SetActive(controller.CameraStyle == cameraStyle);
        }
    }
}

public enum CameraStyle
{
    Basic = 0,
    Combat = 1,
    Topdown = 2
}