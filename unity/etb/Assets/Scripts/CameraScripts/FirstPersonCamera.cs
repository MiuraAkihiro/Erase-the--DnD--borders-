using UnityEngine;
using Cinemachine;

public class FirstPersonCamera : MonoBehaviour
{
    public float sensitivity = 5.0f;
    public float maxYAngle = 80.0f;
    public float normalSpeed = 5.0f;
    public float sprintSpeed = 10.0f;

    private CinemachineVirtualCamera virtualCamera;
    private Transform cameraTransform;
    private Vector2 currentRotation;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = virtualCamera.transform;

        currentRotation = new Vector2(cameraTransform.rotation.eulerAngles.y, -cameraTransform.rotation.eulerAngles.x);
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        currentRotation.x += mouseX * sensitivity;
        currentRotation.y -= mouseY * sensitivity;
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);

        Quaternion localRotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0.0f);
        cameraTransform.rotation = localRotation;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : normalSpeed;

        Vector3 forward = cameraTransform.forward;
        forward.y = 0;

        Vector3 move = (forward * z + cameraTransform.right * x).normalized * speed;

        if (Input.GetKey(KeyCode.Space))
            move.y += speed;
        if (Input.GetKey(KeyCode.LeftControl))
            move.y -= speed;

        cameraTransform.position += move * Time.deltaTime;
    }
}