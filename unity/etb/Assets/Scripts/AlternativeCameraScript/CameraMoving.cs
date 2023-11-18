using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public float panSpeed = 20f; 

    private Vector3 lastMousePosition;

    /*private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }*/

    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;        
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            // �������� ���� �������� ������
            float cameraRotationY = transform.eulerAngles.y;

            // ����������� ������ ����������� ������������ ���� �������� ������
            Vector3 move = Quaternion.Euler(0, cameraRotationY, 0) * new Vector3(-delta.x, -delta.y, 0) * panSpeed * Time.deltaTime;

            transform.Translate(move, Space.World);

            lastMousePosition = Input.mousePosition;
        }
        
    }
}