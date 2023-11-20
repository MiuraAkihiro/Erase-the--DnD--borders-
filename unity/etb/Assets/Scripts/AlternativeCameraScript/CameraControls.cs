using UnityEngine;

public class CameraControls : MonoBehaviour
{
    float camSens = 100f;
    public Transform playerBody;
    public float xRotation = 0f;


    

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * camSens * Time.deltaTime * 10;
            float mouseY = Input.GetAxis("Mouse Y") * camSens * Time.deltaTime * 10;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up* mouseX);
        }
    }
}