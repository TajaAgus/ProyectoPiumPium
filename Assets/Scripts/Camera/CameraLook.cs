using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{

    public float mouseSensitivity = 80f;

    public Transform playerBody;

    float xRotation = 0;
   
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
 

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Bloqueo de angulo de rotación.

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // Rotacion de la camara solamente

        playerBody.Rotate(Vector3.up * mouseX); // Rotacion de todo el player
    }
}
