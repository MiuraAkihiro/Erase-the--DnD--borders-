using UnityEngine;

public class PanCamera : MonoBehaviour
{
    public float panSpeed = 0.5f;
    public float zoomSpeed = 15f;
    public float minZoom = 10f;
    public float maxZoom = 120f;
    public float rotationSpeed = 2f;

    private Vector3 lastPanPosition;
    private bool isRotating;
    private bool isPanning;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastPanPosition = Input.mousePosition;
            isRotating = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (Input.GetMouseButtonDown(2))
        {
            lastPanPosition = Input.mousePosition;
            isPanning = true;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            isPanning = false;
        }

        if (isRotating)
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed;
            transform.Rotate(Vector3.up, -rotationX, Space.World);
            transform.Rotate(Vector3.right, rotationY, Space.Self);
        }

        if (isPanning)
        {
            Vector3 delta = Input.mousePosition - lastPanPosition;
            Vector3 pan = new Vector3(-delta.x, 0, -delta.y) * panSpeed * Time.deltaTime; // Панорамирование по горизонтали и вертикали (X и Z)

            Quaternion panRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            Vector3 panDirection = panRotation * pan;

            transform.Translate(panDirection, Space.World);

            lastPanPosition = Input.mousePosition;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 zoom = transform.forward * scroll * zoomSpeed;
        transform.position += zoom;

        float clampedY = Mathf.Clamp(transform.position.y, minZoom, maxZoom);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }
}
