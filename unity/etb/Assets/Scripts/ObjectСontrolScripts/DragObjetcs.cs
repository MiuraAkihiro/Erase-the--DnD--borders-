using UnityEngine;

public class DragObjects : MonoBehaviour
{
    private GameObject panCameraObject;
    private PanCamera panCameraScript;

    private Vector3 screenPoint;
    private Vector3 offset;

    private float mZCoord;
    private float defaultY;

    public float throughPower = 1000;

    private void Start()
    {
        panCameraObject = GameObject.Find("Virtual Camera 3");
        if (panCameraObject != null)
            panCameraScript = panCameraObject.GetComponent<PanCamera>();
        else
            Debug.LogError("Object with PanCamera component not found!");
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            if (panCameraScript != null)
                panCameraScript.enabled = false;
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = transform.position - GetMouseAsWorldPoint();
        defaultY = transform.position.y + 0.5f;
    }

    void OnMouseUp()
    {
        foreach (var selectedObject in ObjectSelections.Instance.objectSelected)
        {
            Rigidbody selectedRb = selectedObject.GetComponent<Rigidbody>();
            if (selectedRb != null)
            {
                selectedRb.useGravity = true;
                /*selectedRb.velocity += ((GetMouseAsWorldPoint() + offset) - transform.position) * Time.deltaTime * throughPower;*/
            }
        }
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = new Vector3(curPosition.x, defaultY, curPosition.z);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.collider.gameObject.tag == "Table" || hit.collider.gameObject.tag == "Selectable")
            {
                float rayDistance = hit.distance;

                if (rayDistance < 0.5f)
                    defaultY += 0.5f - rayDistance;
                if (rayDistance > 0.5f)
                    defaultY -= rayDistance - 0.5f;
                /*Debug.Log("ƒлина луча: " + rayDistance + "высота" + defaultY);*/
                // ѕолучаем длину выпущенного луча до точки столкновени€ с землей

                foreach (var selectedObject in ObjectSelections.Instance.objectSelected)
                {
                    Rigidbody selectedRb = selectedObject.GetComponent<Rigidbody>();
                    if (selectedRb != null)
                        selectedRb.useGravity = false;
                    if (!Input.GetKey(KeyCode.LeftControl))
                    {
                        Vector3 currentRotation = selectedObject.transform.eulerAngles;
                        selectedObject.transform.rotation = Quaternion.Euler(0f, currentRotation.y, 0f);
                    }
                    
                }
                transform.position = new Vector3(transform.position.x, defaultY, transform.position.z);
            }
            else
                transform.position = new Vector3(transform.position.x, defaultY, transform.position.z);
        }
        Debug.DrawRay(transform.position, Vector3.down, color: Color.red);
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}