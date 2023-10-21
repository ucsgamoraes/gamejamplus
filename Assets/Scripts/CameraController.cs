using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 targetOffset;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float rotationSpeed;

    private float currentRotationAngle;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        MoveCamera();
        HandleRotationInput();
    }

    void MoveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, movementSpeed * Time.deltaTime);
    }

    void HandleRotationInput()
    {
        float rotateInput = Input.GetAxis("Rotate Cam");
        currentRotationAngle += rotateInput * rotationSpeed * Time.deltaTime;

        // Calcule a posi��o da c�mera ao redor do jogador usando trigonometria.
        Vector3 cameraPosition = target.position + Quaternion.Euler(0, currentRotationAngle, 0) * targetOffset;

        // Mire a c�mera no jogador.
        transform.LookAt(target.position);

        // Defina a posi��o da c�mera.
        transform.position = cameraPosition;
    }
}
