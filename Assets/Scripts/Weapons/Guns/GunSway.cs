using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSway : MonoBehaviour
{
    Quaternion startRotation;

    public float swayAmount = 8f; // Velocidad de la rotacion.

    void Start()
    {
        startRotation = transform.localRotation;
    }


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Quaternion xAngle = Quaternion.AngleAxis(mouseX * -2f, Vector3.up); // Cuanto rota.

        Quaternion yAngle = Quaternion.AngleAxis(mouseY * 2f, Vector3.left);

        Quaternion targetRotation = startRotation * xAngle * yAngle; // Angulo afectado por la anterior modificacion.

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * swayAmount);
        // Lerp interpola dos angulos y suaviza la rotacion. Todo afectado por el ultimo valor que es de la velocidad con la que lo hace.
    }
}
