using UnityEngine;
/* все хуйня   https://www.youtube.com/watch?v=sD0vNXQYY_U   нужно это попробовать переписать 
public class FlyingCamera : MonoBehaviour
{
    public float zoomSpeed = 10.0f;
    public float minZoom = 1.0f;
    public float maxZoom = 10.0f;
    public float panSpeed = 20f;
    public float rotationSpeed = 2f;
    public float moveSpeed = 5f;

    private Vector3 focusPoint = Vector3.zero; // Инициализация переменной focusPoint
    private Vector3 lastMousePosition;
    private float distanceFromFocus = 5f; // Начальное расстояние от фокуса
    private float xRotation = 0f;

    void Update()
    {
        UpdateFocusPoint(); // Обновляем точку фокуса при клике ЛКМ
        ZoomCamera();
        MoveCamera();
        RotateCamera();
    }

    void UpdateFocusPoint()
    {
        if (Camera.main != null && Input.GetMouseButtonDown(0)) // Проверка на наличие основной камеры
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                focusPoint = hit.point; // Устанавливаем точку фокуса на объект, по которому кликнули
            }
        }
    }


    void ZoomCamera()
    {
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        float increment = zoomInput * zoomSpeed;

        distanceFromFocus -= increment;
        distanceFromFocus = Mathf.Clamp(distanceFromFocus, minZoom, maxZoom);

        Vector3 dirToFocus = transform.position - focusPoint;
        Vector3 newPos = focusPoint + dirToFocus.normalized * distanceFromFocus;

        transform.position = newPos;
    }

    void MoveCamera()
    {
        if (Input.GetMouseButtonDown(2)) // Колесо мыши
        {
            focusPoint = transform.position; // Устанавливаем точку фокуса на текущую позицию камеры
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 move = -transform.right * (delta.x * panSpeed * Time.deltaTime) - transform.up * (delta.y * panSpeed * Time.deltaTime);

            transform.Translate(move, Space.World);
        }
    }

    void RotateCamera()
    {
        if (Input.GetMouseButton(1)) // ПКМ
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, transform.localRotation.eulerAngles.y + mouseX, 0f);
        }
    }
}
*/