using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownCameraMove : MonoBehaviour
{
    public CharacterController controller;
    public float verticalSpeed = 1f; // Скорость перемещения вверх и вниз

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        move *= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            move += Vector3.up * Time.deltaTime * verticalSpeed;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            move -= Vector3.up * Time.deltaTime * verticalSpeed;
        }

        controller.Move(move);
    }
}
