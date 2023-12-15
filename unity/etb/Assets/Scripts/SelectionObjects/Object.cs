using UnityEngine;

public class Object : MonoBehaviour
{
    void Start()
    {
        ObjectSelections.Instance.objectList.Add(this.gameObject);
    }

    private void OnDestroy()
    {
        ObjectSelections.Instance.objectList.Remove(this.gameObject);
    }
}
