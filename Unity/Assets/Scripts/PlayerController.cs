using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 60.0f;

    void start() { 
    
    }

    void Update()
    {
        // Get input for movement
        float moveInput = Input.GetAxis("Vertical");
        float rotateInput = Input.GetAxis("Horizontal");

        // Calculate movement vector
        Vector3 moveDirection = new Vector3(0, 0, moveInput) * moveSpeed * Time.deltaTime;

        // Apply rotation
        transform.Rotate(Vector3.up, rotateInput * rotateSpeed * Time.deltaTime);

        // Apply local movement
        transform.Translate(moveDirection, Space.Self);
    }
}
