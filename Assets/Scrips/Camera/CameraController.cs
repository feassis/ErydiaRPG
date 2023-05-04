using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float cameraSpeed = 5f;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, 
            playerTransform.position, cameraSpeed * Time.deltaTime);
    }
}
