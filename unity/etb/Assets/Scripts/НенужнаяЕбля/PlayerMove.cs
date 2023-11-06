using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerCamera; // Добавление ссылки на объект камеры

    public float speed = 6f;

    private void Update()
    {     
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = playerCamera.TransformDirection(new Vector3(x, 0, z)); // Преобразование направления движения относительно камеры

        controller.Move(move * speed * Time.deltaTime);
    }
}
