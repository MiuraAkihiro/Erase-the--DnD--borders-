using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RollDiceSystem : MonoBehaviour
{
    private Vector3 Force;
    private Vector3 Torque;

    public List<GameObject> diceSelectedList = new List<GameObject>();
    void Update()
    {
        foreach (var selectedObject in ObjectSelections.Instance.objectSelected)
        {
            if (selectedObject.GetComponent<Dice>() != null)
            {
                if (!diceSelectedList.Contains(selectedObject))
                    diceSelectedList.Add(selectedObject);
            }
        }
        if(diceSelectedList != null)
        {
            foreach (var selectedDice in diceSelectedList)
            {
                if (!ObjectSelections.Instance.objectSelected.Contains(selectedDice))
                {
                    diceSelectedList.Remove(selectedDice);
                    break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach(var selectedDice in diceSelectedList)
            {
                RollDice(selectedDice.GetComponent<Dice>());
            }
        }
    }

    public void RollDice(Dice selectedDice)
    {
        Force = new Vector3(0, 100, 0);
        Torque = new Vector3(100, 100, 100);
        if (selectedDice != null)
        {
            selectedDice.GetComponent<Rigidbody>().AddForce(Force * 7);
            selectedDice.GetComponent<Rigidbody>().AddTorque(Torque * Random.Range(5, 100));
            selectedDice.GetComponent<Rigidbody>().WakeUp();

            selectedDice.diceSleeping = false;

        }
    }
}
