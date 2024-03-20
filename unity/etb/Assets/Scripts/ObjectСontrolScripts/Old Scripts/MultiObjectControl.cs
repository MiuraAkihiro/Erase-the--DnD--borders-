using System.Collections.Generic;
using UnityEngine;

public class MultiObjectControl : MonoBehaviour
{
    public static GameObject container;
    private static MultiObjectControl _instance;

    public static MultiObjectControl Instance { get { return _instance; } }

    private void Awake()
    {
        container = GameObject.Find("Container");
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void AddToContainerWithShift(GameObject objectToAdd)
    {
        if (!ObjectSelections.Instance.objectSelected.Contains(objectToAdd))
        {
            objectToAdd.transform.SetParent(container.transform);
        }
        else
        {
            objectToAdd.transform.SetParent(null);
        }
    }
    public void AddToContainerWithClick(GameObject objectToAdd)
    {
        if (!ObjectSelections.Instance.objectSelected.Contains(objectToAdd))
        {
            ClearContainer();
            objectToAdd.transform.SetParent(container.transform);
        }
    }

    public void AddToContainerDragSelected(GameObject objectToAdd)
    {
        if (!ObjectSelections.Instance.objectSelected.Contains(objectToAdd))
        {
            objectToAdd.transform.SetParent(container.transform);
        }
    }
    public void ClearContainer()
    {
        List<Transform> children = new List<Transform>(); // fixed bug with pull out childs from container
        foreach (Transform objectToClear in container.transform)
        {
            children.Add(objectToClear);
        }
        foreach (Transform child in children)
        {
            child.SetParent(null);
        }
    }
}
