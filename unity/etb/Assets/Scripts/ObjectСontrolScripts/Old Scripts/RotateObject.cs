using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private GameObject panCameraObject; // ������ �� ������ � ����������� PanCamera
    private PanCamera panCameraScript; // ������ �� ��������� PanCamera

    void Start()
    {
        // ������� ������ � ����������� PanCamera
        panCameraObject = GameObject.Find("Virtual Camera 3"); // �������� "NameOfPanCameraObject" �� ��� ������� � PanCamera

        // �������� ��������� PanCamera
        if (panCameraObject != null)
        {
            panCameraScript = panCameraObject.GetComponent<PanCamera>();
        }
        else
        {
            Debug.LogError("Object with PanCamera component not found!");
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && ObjectSelections.Instance.objectSelected.Count == 1)
        {
            // ��������� ������ PanCamera
            if (panCameraScript != null)
            {
                panCameraScript.enabled = false;
            }

            // ���������� �������� ������� ������ ��� ��� � ������� �������� ����
            float rotationSpeed = 500f;
            float rotateInput = Input.GetAxis("Mouse ScrollWheel");

            foreach (var selectedObject in ObjectSelections.Instance.objectSelected) // Fixed bug. Dont change object wtih direction ObjectSelections.Instance.objectSelected
            {
                Vector3 currentRotation = selectedObject.transform.eulerAngles;
                selectedObject.transform.rotation = Quaternion.Euler(0f, currentRotation.y, 0f);
                selectedObject.transform.Rotate(Vector3.up, rotateInput * rotationSpeed);
            }
        }
        else
        {
            if (panCameraScript != null)
            {
                panCameraScript.enabled = true;
            }
        }
    }
}
