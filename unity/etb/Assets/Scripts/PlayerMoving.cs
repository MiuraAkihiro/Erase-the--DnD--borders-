using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerCamera;

    public float speed = 6f;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = playerCamera.TransformDirection(new Vector3(x, 0, z));

        if (Input.GetKey(KeyCode.Space))
            move += Vector3.up;
        if (Input.GetKey(KeyCode.LeftControl))
            move += Vector3.down;
        if (Input.GetKey(KeyCode.LeftShift))
            speed = 12;
        else
            speed = 6;

        controller.Move(move * speed * Time.deltaTime);    
    }
}
