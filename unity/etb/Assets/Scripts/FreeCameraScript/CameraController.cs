using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 5.0f;
    public float maxYAngle = 80.0f;

    private Vector2 currentRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentRotation = new Vector2(transform.eulerAngles.y, -transform.eulerAngles.x);
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        currentRotation.x += mouseX * sensitivity;
        currentRotation.y -= mouseY * sensitivity;
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);

        Quaternion localRotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0.0f);
        transform.rotation = localRotation;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.Space))
            move += Vector3.up;
        if (Input.GetKey(KeyCode.LeftControl))
            move -= Vector3.up;

        // ƒополнительное перемещение вверх/вниз независимо от направлени€ камеры
        float verticalMovement = 0.0f;
        if (Input.GetKey(KeyCode.Space))
            verticalMovement = 1.0f;
        else if (Input.GetKey(KeyCode.LeftControl))
            verticalMovement = -1.0f;

        transform.position += (move * sensitivity * 0.5f + Vector3.up * verticalMovement) * Time.deltaTime;
    }
}
