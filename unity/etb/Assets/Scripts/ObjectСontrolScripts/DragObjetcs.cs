/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjetcs : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    private Rigidbody selectedRb;
    private Collider myCollider;
    private float originalHigh = 10f;

    private float originalSpeed;

    // Parameters for speed control
    public float maxSpeed = 10f;

    void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mOffset = transform.position - GetMouseAsWorldPoint();

        foreach (var selectedObject in ObjectSelections.Instance.objectSelected)
        {
            selectedRb = selectedObject.GetComponent<Rigidbody>();
        }

    }

    private void OnMouseUp()
    {

        foreach (var selectedObject in ObjectSelections.Instance.objectSelected)
        {
            selectedRb = selectedObject.GetComponent<Rigidbody>();
        }
    }

    void OnMouseDrag()
    {
        Vector3 newPos = GetMouseAsWorldPoint() + mOffset;

        if (true)
        {
            foreach (var selectedObject in ObjectSelections.Instance.objectSelected)
            {
                selectedRb = selectedObject.GetComponent<Rigidbody>();
                if (selectedRb != null)
                {
                    
                    selectedObject.transform.rotation = Quaternion.identity;

                    Vector3 direction = (newPos - selectedObject.transform.position).normalized;

                    if (*//*растояние до колайдинг обжект с одной из сторон осей віделенного обьекта == какому-то числу*//*)
                    {
                        selectedRb.velocity.x = 0;
                    }
                    direction.y += 10f;
                    float distance = Vector3.Distance(newPos, selectedObject.transform.position);
                    float speed = 10f; // Adjust the speed as needed


                    selectedRb.velocity = direction * speed * distance; // Применение скорости к Rigidbody
                }
            }
        }
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
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
*/

using UnityEngine;

public class DragObjects : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private float defaultY;
    private bool isDragging = false;

    void OnMouseDown()
    {
        isDragging = true;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        defaultY = transform.position.y + 1.0f; // Increase the Y position when the mouse is clicked
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = new Vector3(curPosition.x, defaultY, curPosition.z);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.collider.gameObject.tag == "Ground")
            {
                transform.position = new Vector3(transform.position.x, defaultY + 0.01f, transform.position.z); // Increase the Y position
            }
            else
            {
                transform.position = new Vector3(transform.position.x, defaultY, transform.position.z); // Reset the Y position
            }
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object2" || collision.gameObject.tag == "Object3")
        {
            // Handle the collision with Object2 or Object3
        }
    }
}
