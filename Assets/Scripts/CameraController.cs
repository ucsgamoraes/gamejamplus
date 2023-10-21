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
  
    }

    void MoveCamera()
    {
        float rotateInput = Input.GetAxis("Rotate Cam");
        currentRotationAngle += rotateInput * rotationSpeed * Time.deltaTime;

        // Calcule a posição da câmera ao redor do jogador
        Vector3 cameraPosition = target.position + Quaternion.Euler(0, currentRotationAngle, 0) * targetOffset;

        // Mire a câmera no jogador.
        transform.LookAt(target.position);

        // Defina a posição da câmera.
        transform.position = cameraPosition;
    }

}
