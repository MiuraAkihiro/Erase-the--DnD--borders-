using UnityEngine;

public class CamMovement : MonoBehaviour
{
    float camSens = 100f;
    public Transform playerBody;
    public float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * camSens * Time.deltaTime * 10;
        float mouseY = Input.GetAxis("Mouse Y") * camSens * Time.deltaTime * 10;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);


        /*Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //totalRun += Time.deltaTime;
            p = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 100f);
            p = p * mainSpeed;
        }

        p = p * Time.deltaTime;
        // Vector3 newPosition = transform.position;
        // ability to move up
        
        transform.Translate(p);*/
    }
    /*private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        playerBody.position = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            playerBody.position += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerBody.position += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerBody.position += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerBody.position += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            playerBody.position += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            playerBody.position += new Vector3(0, -1, 0);
        }


        return playerBody.position;
    }*/
}