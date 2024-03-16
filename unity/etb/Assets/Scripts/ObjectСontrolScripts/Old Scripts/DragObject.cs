using UnityEngine;

public class DragObject : MonoBehaviour
{
    private GameObject panCameraObject; // Ссылка на объект с компонентом PanCamera
    private PanCamera panCameraScript; // Ссылка на компонент PanCamera

    private Vector3 mOffset;
    private float mZCoord;
    private Rigidbody rb;
    private Collider myCollider;

    // Additional variables for speed control
    private float originalSpeed;

    // Parameters for speed control
    public float maxSpeed = 10f;



    void Start()
    {
        // Находим объект с компонентом PanCamera
        panCameraObject = GameObject.Find("Virtual Camera 3"); // Замените "NameOfPanCameraObject" на имя объекта с PanCamera

        if (panCameraObject != null)
        {
            panCameraScript = panCameraObject.GetComponent<PanCamera>();
        }
        else
        {
            Debug.LogError("Object with PanCamera component not found!");
        }

        rb = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        originalSpeed = rb.velocity.magnitude; // Store the original speed
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mOffset = transform.position - GetMouseAsWorldPoint();
        // Выключаем скрипт PanCamera
        if (panCameraScript != null)
        {
            panCameraScript.enabled = false;
        }
    }

    private void OnMouseUp()
    {
        Vector3 newPos = GetMouseAsWorldPoint() + mOffset;
        Vector3 newVelocity = (newPos - transform.position) * Time.deltaTime * 4000;

        // Apply speed limit
        if (rb.velocity.magnitude + newVelocity.magnitude <= maxSpeed-5f) // Fixed problem with flying token after brake the maxSpeed throwing
        {
            rb.velocity += newVelocity;
        }

        // Re-enable gravity for all selected objects after dragging
        foreach (var selectedObject in ObjectSelections.Instance.objectSelected)
        {
            Rigidbody selectedRb = selectedObject.GetComponent<Rigidbody>();
            if (selectedRb != null)
            {
                selectedRb.useGravity = true;
            }
        }
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        Vector3 newPos = GetMouseAsWorldPoint() + mOffset;

        // Check if the collider is colliding with other objects
        if (!IsCollidingWithOtherObjects(newPos))
        {
            foreach (var selectedObject in ObjectSelections.Instance.objectSelected)
            {
                if (!Input.GetKey(KeyCode.LeftControl))
                {
                    selectedObject.transform.rotation = Quaternion.identity;
                }
                Vector3 offsetFromCurrentPosition = selectedObject.transform.position - transform.position;
                selectedObject.transform.position = newPos + offsetFromCurrentPosition;

                Rigidbody[] selectedRbs = selectedObject.GetComponents<Rigidbody>(); //Get rb from all objects

                // Disable gravity for selected objects during dragging
                foreach (var selectedRb in selectedRbs)
                {
                    if (selectedRb != null)
                    {
                        selectedRb.useGravity = false;
                    }
                }
                rb.velocity = rb.velocity.normalized * originalSpeed;
            }
        }
        else
        {
            newPos.y = transform.position.y-0.09f;
            foreach (var selectedObject in ObjectSelections.Instance.objectSelected)
            {
                Rigidbody selectedRb = selectedObject.GetComponent<Rigidbody>();
                Vector3 newVelocity = (newPos - transform.position) * Time.deltaTime * 100;
                // Apply speed limit
                if (selectedRb.velocity.magnitude + newVelocity.magnitude <= maxSpeed) // Fixed problem with flying token after brake the maxSpeed
                {
                    selectedRb.velocity += newVelocity;
                }
                else
                {
                    selectedRb.velocity = newVelocity;
                }
            }
        }
    }

    bool IsCollidingWithOtherObjects(Vector3 position)
    {
        // Check for overlap with other colliders
        Collider[] colliders = Physics.OverlapBox(position, myCollider.bounds.extents, Quaternion.identity);
        foreach (Collider collider in colliders)
        {
            if (collider != myCollider)
            {
                return true; // Object is colliding with others
            }
        }
        return false; // Object is not colliding
    }
}
