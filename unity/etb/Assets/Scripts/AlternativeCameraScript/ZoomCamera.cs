using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    public float zoomSpeed = 10.0f;
    public float minZoom = 1.0f;
    public float maxZoom = 10.0f;

    public Transform parent;

    void Update()
    {
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        ZoomObject(zoomInput);
    }

    void ZoomObject(float increment)
    {
        if (parent != null)
        {
            float cameraRotationY = parent.eulerAngles.y;
            float cameraRotationX = parent.eulerAngles.x;

            float angleFactor = Mathf.Cos(cameraRotationX * Mathf.Deg2Rad);

            Vector3 forward = Quaternion.Euler(0, cameraRotationY, 0) * Vector3.forward;
            Vector3 right = Quaternion.Euler(0, cameraRotationY, 0) * Vector3.right;

            Vector3 zoom = forward * increment * zoomSpeed;
            zoom.y = increment * zoomSpeed * angleFactor;

            // «ум в глобальных координатах
            transform.Translate(zoom, Space.World);
        }
        else
        {
            Debug.LogWarning("Parent object is null.");
        }
    }
}

/* float cameraRotationY = parent.eulerAngles.y;

             Vector3 forward = Quaternion.Euler(0, cameraRotationY, 0) * Vector3.forward;

             Vector3 zoom = forward * increment * zoomSpeed;
             zoom.y = increment * zoomSpeed;  // ”честь ось Y

             transform.Translate(zoom, Space.World);

             transform.Translate(zoom, Space.World);*/
