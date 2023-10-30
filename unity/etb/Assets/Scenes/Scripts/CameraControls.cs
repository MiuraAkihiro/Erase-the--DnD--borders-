using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float panSpeed = 20.0f;
    public float zoomSpeed = 5000.0f;
    public float rotationSpeed = 50.0f;

    private Vector3 lastMousePosition;
    private Transform pivot;

    void Start()
    {
        pivot = new GameObject("Pivot").transform;
        pivot.position = transform.position + new Vector3(0,-10,0);
        transform.SetParent(pivot);
    }

    void Update()
    {
        // Зум камеры (с использованием колесика мыши)
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        Vector3 zoom = transform.forward * zoomInput * zoomSpeed * Time.deltaTime;
        transform.Translate(zoom, Space.World);

        // Вращение камеры (при зажатой левой кнопке мыши)
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            float deltaX = Input.mousePosition.x - lastMousePosition.x;
            float deltaY = Input.mousePosition.y - lastMousePosition.y;

            // Ограничение вращения по осям X и Y
            pivot.Rotate(Vector3.up, deltaX * rotationSpeed * Time.deltaTime, Space.World);
            pivot.localRotation *= Quaternion.Euler(-deltaY * rotationSpeed * Time.deltaTime, 0, 0);
        }

        // Перемещение камеры (при зажатой средней кнопке мыши)
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 move = new Vector3(-delta.x, -delta.y, 0) * panSpeed * Time.deltaTime;
            transform.Translate(move, Space.Self);
            pivot.Translate(move, Space.Self); // Перемещение точки вместе с камерой
        }

        lastMousePosition = Input.mousePosition;
    }
}
