using System.Collections;
using UnityEditor.XR;
using UnityEngine;

public class DragObjects : MonoBehaviour
{
    private GameObject panCameraObject;
    private PanCamera panCameraScript;

    private Rigidbody[] rbArray = new Rigidbody[0];

    private Vector3[] mOffsets;
    private Vector3[] newPos;

    private Quaternion[] newRots;

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
    private bool isMouseButtonDown = false;
    private bool isDragStarted = false;

    private Coroutine dragCoroutine;

    void Start()
    {
        panCameraObject = GameObject.Find("Virtual Camera 3");
        if (panCameraObject != null)
            panCameraScript = panCameraObject.GetComponent<PanCamera>();
        else
            Debug.LogError("Object with PanCamera component not found!");
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && panCameraScript != null)
            panCameraScript.enabled = false;
        if (ObjectSelections.Instance.objectSelected.Count == 0)
            panCameraScript.enabled = true;
        
    }

    private void OnMouseDown()
    {
        isDragStarted = false;
        dragCoroutine = StartCoroutine(WaitForDrag());
    }

    private IEnumerator WaitForDrag()
    {
        Vector3 initialMousePosition = Input.mousePosition;
        float timeElapsed = 0f;
        float waitFor = 0.5f;

        while (timeElapsed < waitFor && !isMouseButtonDown && Vector3.Distance(Input.mousePosition, initialMousePosition) < 0.1f)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        if (!isMouseButtonDown)
            isDragStarted = true;
    }

    private void OnMouseUp()
    {
        if (isDragStarted)
        {
            panCameraScript.enabled = true;
            for (int i = 0; i < rbArray.Length; i++)
            {
                rbArray[i].isKinematic = true;
                rbArray[i].isKinematic = false;
                rbArray[i].useGravity = true;

                Vector3 newPos = GetMouseAsWorldPoint() + mOffsets[i];
                newPos.y = height;

                Vector3 direction = (newPos - rbArray[i].transform.position);
                rbArray[i].velocity = direction * releaseSpeed;
            }
            isMouseButtonDown = false;
        }
    }

    void OnMouseDrag()
    {
        if (isDragStarted)
        {
            if (!isMouseButtonDown)
                GetArraySize();
            isMouseButtonDown = true;
            int i = 0;
            foreach (Rigidbody rb in rbArray)
            {
                rb.useGravity = false;
                rb.transform.rotation = newRots[i];
                i++;
            }

            rotateInput = Input.GetAxis("Mouse ScrollWheel");
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                panCameraScript.enabled = false;
                currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, rotateInput * rotationSpeed, smoothSpeed * Time.deltaTime);
                foreach (Rigidbody rb in rbArray)
                    rb.transform.Rotate(Vector3.up, currentRotationSpeed, Space.World);
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
                foreach (Rigidbody rb in rbArray)
                {
                    if (rb.transform.rotation != Quaternion.Euler(0f, 0f, 0f))
                    {
                        Quaternion temp = Quaternion.Slerp(rb.transform.rotation, Quaternion.identity, Time.deltaTime);
                        rb.transform.rotation = Quaternion.Euler(temp.x, temp.y, 0f);
                    }
                    else
                        isRPressed = false;
                }
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                panCameraScript.enabled = false;
                float currentRotateInput = Input.GetAxis("Mouse ScrollWheel");
                Vector3 cameraForward = Camera.main.transform.forward;
                cameraForward.y = 0f;
                cameraForward.Normalize();
                foreach (Rigidbody rb in rbArray)
                    rb.transform.position += cameraForward * currentRotateInput * toCameraSpeed * Time.deltaTime;
            }
            else
            {
                Vector3[] smoothedPos = new Vector3[rbArray.Length];
                for (i = 0; i < rbArray.Length; i++)
                {
                    newPos[i] = GetMouseAsWorldPoint() + mOffsets[i];
                    newPos[i].y = height;
                    smoothedPos[i] = Vector3.Lerp(rbArray[i].transform.position, newPos[i], smoothSpeed * 10 * Time.deltaTime);
                }

                i = 0;
                foreach (Rigidbody rb in rbArray)
                {
                    rb.MovePosition(smoothedPos[i]);
                    rb.velocity = (smoothedPos[i] - rb.transform.position).normalized * originalSpeed;
                    newRots[i] = rb.transform.rotation;
                    i++;
                }
            }
        }
    }

    public void GetArraySize()
    {
        int objectCount = ObjectSelections.Instance.objectSelected.Count;
        rbArray = new Rigidbody[objectCount];
        int i = 0;
        foreach (var obj in ObjectSelections.Instance.objectSelected)
        {
            rbArray[i] = obj.GetComponent<Rigidbody>();
            i++;
        }
        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mOffsets = new Vector3[rbArray.Length];
        newRots = new Quaternion[rbArray.Length];
        newPos = new Vector3[rbArray.Length];

        for (i = 0; i < rbArray.Length; i++)
        {
            mOffsets[i] = rbArray[i].transform.position - GetMouseAsWorldPoint();
            newRots[i] = rbArray[i].transform.rotation;
            newPos[i] = rbArray[i].transform.position;
        }
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
