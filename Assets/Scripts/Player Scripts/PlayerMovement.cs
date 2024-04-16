using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 100f;
    public SocketManager socketManager; 

    void Update()
    {
        MovePlayer();
        RotateCamera();
    }

    void MovePlayer()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        transform.Translate(movement);

        socketManager.SendPlayerMovement(transform.position, transform.rotation);
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");

        float rotationAmount = mouseX * rotateSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, rotationAmount);

        socketManager.SendPlayerMovement(transform.position, transform.rotation);
    }
}
