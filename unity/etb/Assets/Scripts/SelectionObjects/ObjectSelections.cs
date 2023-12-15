using System.Collections.Generic;
using UnityEngine;

public class ObjectSelections : MonoBehaviour
{
    public List<GameObject> objectList = new List<GameObject>();
    public List<GameObject> objectSelected = new List<GameObject>();

    private static ObjectSelections _instance;
    public static ObjectSelections Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ClickSelect(GameObject objectToAdd)
    {
        DeselectAll();
        objectSelected.Add(objectToAdd);
        objectToAdd.transform.GetComponent<Outline>().enabled = true;
    }
    public void ShiftClickSelect(GameObject objectToAdd)
    {
        if (!objectSelected.Contains(objectToAdd))
        {
            objectSelected.Add(objectToAdd);
            objectToAdd.transform.GetComponent<Outline>().enabled = true;
        }
        else
        {
            objectToAdd.transform.GetComponent<Outline>().enabled = false;
            objectSelected.Remove(objectToAdd);
        }
    }
    public void DragSelect(GameObject objectToAdd)
    {
        if (!objectSelected.Contains(objectToAdd))
        {
            objectSelected.Add(objectToAdd);
            objectToAdd.transform.GetComponent<Outline>().enabled = true;
        }
    }
    public void DeselectAll()
    {
        foreach (var _object in objectSelected)
        {
            _object.transform.GetComponent<Outline>().enabled = false;
        }
        objectSelected.Clear();
    }
}
