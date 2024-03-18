using UnityEngine;

public class DragObjects : MonoBehaviour
{
    private GameObject panCameraObject;
    private PanCamera panCameraScript;

    private Rigidbody rb;

    private Vector3 mOffset;
    private Vector3 newPos;
    private Quaternion newRot;

    private float mZCoord;
    private float height = 6.25f;
    private float smoothSpeed = 10f;
    private float releaseSpeed = 10f;
    private float originalSpeed;
    private float currentRotationSpeed = 0f;
    private float rotationSpeed = 450f;
    private float rotateInput;
    private float toCameraSpeed = 500f;
    private float floor = 5.25f;
    private bool isRPressed = false;

    void Start()
    {
        panCameraObject = GameObject.Find("Virtual Camera 3");
        if (panCameraObject != null)
            panCameraScript = panCameraObject.GetComponent<PanCamera>();
        else
            Debug.LogError("Object with PanCamera component not found!");

        rb = GetComponent<Rigidbody>();
        originalSpeed = rb.velocity.magnitude;
        /*Physics.gravity = new Vector3(0, -20.0f, 0);*/
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && panCameraScript != null)
            panCameraScript.enabled = false;
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mOffset = transform.position - GetMouseAsWorldPoint();
        newRot = transform.rotation;
    }

    private void OnMouseUp()
    {
        panCameraScript.enabled = true;

        rb.isKinematic = true;
        rb.isKinematic = false; // fix problem with residual rotational motion

        rb.useGravity = true;
        currentRotationSpeed = 0f;
        newPos = GetMouseAsWorldPoint() + mOffset;
        Vector3 currPos = transform.position;
        currPos.y = height;
        newPos.y = height;
        Vector3 direction = (newPos - currPos);
        rb.velocity = direction * releaseSpeed;

    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        rb.useGravity = false;
        transform.rotation = newRot;
        rotateInput = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            panCameraScript.enabled = false;
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, rotateInput * rotationSpeed, smoothSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up, currentRotationSpeed, Space.World);
        }
        else if (!Input.GetKey(KeyCode.LeftControl))
        {
            height -= rotateInput;
            if (height < floor)
                height = floor;
        }
        if (Input.GetKeyDown(KeyCode.R) || isRPressed == true)
        {
            isRPressed = true;

            if (transform.rotation != Quaternion.Euler(0f, 0f, 0f))
            {
                Quaternion temp = Quaternion.Slerp(transform.rotation, Quaternion.identity, smoothSpeed * Time.deltaTime);

                transform.rotation = Quaternion.Euler(temp.x, temp.y, 0f);
                Debug.Log(transform.rotation + "True");

            }
            else
            {
                isRPressed = false;
                Debug.Log("false");
            }
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            panCameraScript.enabled = false;
            float curretnRotateInput = Input.GetAxis("Mouse ScrollWheel");
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();
            transform.position += cameraForward * curretnRotateInput * toCameraSpeed * Time.deltaTime;
        }
        else
        {
            newPos = GetMouseAsWorldPoint() + mOffset;
            newPos.y = height;

            Vector3 smoothedPos = Vector3.Lerp(transform.position, newPos, smoothSpeed * 3 * Time.deltaTime);
            rb.MovePosition(smoothedPos);
            rb.velocity = (smoothedPos - transform.position).normalized * originalSpeed;
            newRot = transform.rotation;
        }
    }
}