using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private Camera cam;

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // direção para qual o movimento foi executado
        Vector3 targetVector = new Vector3(horizontal, 0, vertical);

        // calcula a posição final do jogador
        Vector3 movementVector = MoveTowardTarget(targetVector);

        // rotaciona o player com base no movimento
        RotateTowardMovementVector(movementVector);
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        float speed = movementSpeed * Time.deltaTime;
        targetVector = Quaternion.Euler(0, cam.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
        Vector3 targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if (movementDirection.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
    }
}
