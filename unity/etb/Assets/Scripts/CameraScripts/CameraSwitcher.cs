using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCameraBase firstCamera;
    public CinemachineVirtualCameraBase secondCamera;

    private Transform firstCameraTransform;
    private Transform secondCameraTransform;

    private Vector3 defaultSecondCameraPosition;
    private Quaternion defaultSecondCameraRotation;
    private Quaternion defaultFirstCameraRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ������������� ������
        Cursor.visible = false; // ������� ������ ���������

        firstCameraTransform = firstCamera.transform;
        secondCameraTransform = secondCamera.transform;

        firstCamera.Priority = 1;
        secondCamera.Priority = 0;

        // ���������� ��������� ��������� � �������� ������ ������
        defaultSecondCameraPosition = secondCameraTransform.position;
        defaultSecondCameraRotation = secondCameraTransform.rotation;
        defaultFirstCameraRotation = firstCameraTransform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (firstCamera.Priority > secondCamera.Priority)
            {
                SwitchToSecondCamera();
            }
            else
            {
                SwitchToFirstCamera();
            }
        }
    }

    private void SwitchToSecondCamera()
    {
        // �������� ������ ������
        secondCamera.gameObject.SetActive(true);

        // ����������� ������
        firstCamera.Priority = 0;
        secondCamera.Priority = 1;

        // ��������� ������ ������ 
        firstCamera.gameObject.SetActive(false);

        // ������������� ������� ������ ������ ������
        secondCameraTransform.position = firstCameraTransform.position;

        // �������� �������� �� ��� X �� 90 ��������
        Vector3 newRotation = defaultSecondCameraRotation.eulerAngles;
        newRotation.x += 90f;
        secondCameraTransform.rotation = Quaternion.Euler(newRotation);

        // ��������� ������ ������
        Vector3 newPosition = secondCameraTransform.position;
        newPosition.y = 20f;
        secondCameraTransform.position = newPosition;

        Cursor.lockState = CursorLockMode.None; // �������������� ������
        Cursor.visible = true; // ������� ������ �������
    }

    private void SwitchToFirstCamera()
    {
        // �������� ������ ������
        firstCamera.gameObject.SetActive(true);

        // ����������� ������
        firstCamera.Priority = 1;
        secondCamera.Priority = 0;

        // ��������� ������ ������
        secondCamera.gameObject.SetActive(false);

        // ������������� ������� ������ ������ ������
        firstCameraTransform.position = secondCameraTransform.position;

        // �������� �������� ������ ������
        Vector3 newRotation = defaultFirstCameraRotation.eulerAngles;
        newRotation.x = 45f;
        newRotation.y = 90f;
        newRotation.z = 0f;
        firstCameraTransform.rotation = Quaternion.Euler(newRotation);

        // ��������� ������ ������
        Vector3 newPosition = firstCameraTransform.position;
        newPosition.y = 15f;
        firstCameraTransform.position = newPosition;

        Cursor.lockState = CursorLockMode.Locked; // ������������� ������
        Cursor.visible = false; // ������� ������ ���������
    }
}
