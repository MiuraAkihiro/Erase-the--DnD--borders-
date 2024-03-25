using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RollDiceSystem : MonoBehaviour
{
    private Vector3 Force;
    private Vector3 Torque;
    public int totalNumber;
    private bool isTotalNumberOutput = false;
    private bool isNumberReturned = true;
    private bool isFirstRollMade = false;

    public List<GameObject> diceSelectedList = new List<GameObject>();
    public List<GameObject> diceSRolledList = new List<GameObject>();

    private static RollDiceSystem _instance;
    public static RollDiceSystem Instance { get { return _instance; } }
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

    void Update()
    {
        GetFirstRoll();
        GetToListSelectedDice();
        RemoveDeselectedDice();

        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var selectedDice in diceSelectedList)
            {
                RollDice(selectedDice.GetComponent<Dice>());
            }
        }

        if (AllDiceStopped() && !isTotalNumberOutput)
        {
            Debug.Log("Roll: " + totalNumber);
            isTotalNumberOutput = true;
            totalNumber = 0;
        }
    }

    public void RollDice(Dice selectedDice)
    {
        Force = new Vector3(0, 100, 0);
        Torque = new Vector3(100, 100, 100);
        totalNumber = 0;
        if (selectedDice != null)
        {
            selectedDice.GetComponent<Rigidbody>().AddForce(Force * 7);
            selectedDice.GetComponent<Rigidbody>().AddTorque(Torque * Random.Range(5, 100));
            selectedDice.GetComponent<Rigidbody>().WakeUp();

            selectedDice.diceSleeping = false;
            selectedDice.isResultOutput = false;
            isNumberReturned = false;
            isFirstRollMade = true;
        }
    }

    private bool AllDiceStopped()
    {
        foreach (var selectedDice in diceSRolledList)
        {
            if (!selectedDice.GetComponent<Dice>().diceSleeping)
            {
                return false; 
            }
        }
        if (!isNumberReturned)
        {
            isTotalNumberOutput = false;
            isNumberReturned = true;
        }
        return true; 
    }
    private void GetFirstRoll()
    {
        if (!isFirstRollMade)
        {
            totalNumber = 0;
        }
        else
            isFirstRollMade = true;
    }
    private void GetToListSelectedDice()
    {
        foreach (var selectedObject in ObjectSelections.Instance.objectSelected)
        {
            if (selectedObject.GetComponent<Dice>() != null)
            {
                if (!diceSelectedList.Contains(selectedObject))
                {
                    diceSelectedList.Add(selectedObject);
                }
            }
        }
    }
    private void RemoveDeselectedDice()
    {
        if (diceSelectedList.Count != 0)
        {
            diceSRolledList.AddRange(diceSelectedList);
            foreach (var selectedDice in diceSelectedList)
            {
                if (!ObjectSelections.Instance.objectSelected.Contains(selectedDice))
                {
                    diceSelectedList.Remove(selectedDice);
                    break;
                }
            }
        }
    }
}
