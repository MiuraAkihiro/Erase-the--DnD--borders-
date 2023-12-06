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
        Cursor.lockState = CursorLockMode.Locked; // заблокировать курсор
        Cursor.visible = false; // Сделать курсор невидимым

        firstCameraTransform = firstCamera.transform;
        secondCameraTransform = secondCamera.transform;

        firstCamera.Priority = 1;
        secondCamera.Priority = 0;

        // Запоминаем начальное положение и вращение второй камеры
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
        // Включаем вторую камеру
        secondCamera.gameObject.SetActive(true);

        // Переключаем камеры
        firstCamera.Priority = 0;
        secondCamera.Priority = 1;

        // Выключаем первую камеру 
        firstCamera.gameObject.SetActive(false);

        // Устанавливаем позицию первой камеры второй
        secondCameraTransform.position = firstCameraTransform.position;

        // Изменяем вращение по оси X на 90 градусов
        Vector3 newRotation = defaultSecondCameraRotation.eulerAngles;
        newRotation.x += 90f;
        secondCameraTransform.rotation = Quaternion.Euler(newRotation);

        // Поднимаем вторую камеру
        Vector3 newPosition = secondCameraTransform.position;
        newPosition.y = 20f;
        secondCameraTransform.position = newPosition;

        Cursor.lockState = CursorLockMode.None; // Разблокировать курсор
        Cursor.visible = true; // Сделать курсор видимым
    }

    private void SwitchToFirstCamera()
    {
        // Включаем первую камеру
        firstCamera.gameObject.SetActive(true);

        // Переключаем камеры
        firstCamera.Priority = 1;
        secondCamera.Priority = 0;

        // Выключаем вторую камеру
        secondCamera.gameObject.SetActive(false);

        // Устанавливаем позицию второй камеры первой
        firstCameraTransform.position = secondCameraTransform.position;

        // Изменяем вращение первой камеры
        Vector3 newRotation = defaultFirstCameraRotation.eulerAngles;
        newRotation.x = 45f;
        newRotation.y = 90f;
        newRotation.z = 0f;
        firstCameraTransform.rotation = Quaternion.Euler(newRotation);

        // Поднимаем первую камеру
        Vector3 newPosition = firstCameraTransform.position;
        newPosition.y = 15f;
        firstCameraTransform.position = newPosition;

        Cursor.lockState = CursorLockMode.Locked; // заблокировать курсор
        Cursor.visible = false; // Сделать курсор невидимым
    }
}
