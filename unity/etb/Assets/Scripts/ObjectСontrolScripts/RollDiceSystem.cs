using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class RollDiceSystem : MonoBehaviour
{
    private Dice SelectedDice;
    public Button Button;
    public TextMeshProUGUI Text;
    public Vector3 Force;
    public Vector3 Torque;
    public int DiceRoll;
    public bool DiceRolled;
    private bool isContainsScript;

    void ButtonClick()
    {
        Force = new Vector3(0, Random.Range(100, 120), 0);
        Torque = new Vector3(Random.Range(100, 1000), Random.Range(100, 200), Random.Range(100, 1000));
        if(SelectedDice != null)
        {
            SelectedDice.GetComponent<Rigidbody>().AddForce(Force * Random.Range(5, 10));
            SelectedDice.GetComponent<Rigidbody>().AddTorque(Torque * Random.Range(5, 100));
            SelectedDice.GetComponent<Rigidbody>().WakeUp();

            SelectedDice.diceSleeping = false;

            DiceRolled = true;
        }
        
    }

    void Start()
    {
        Button btn = Button.GetComponent<Button>();
        btn.onClick.AddListener(ButtonClick);
    }

    void Update()
    {
        // Проверяем, был ли сделан клик мышью
        if (Input.GetMouseButtonDown(0))
        {
            // Создаем луч, исходящий из точки, где кликнули по экрану
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Проверяем пересечение луча с объектами на слое LayerMask
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.GetComponent<Dice>())
                    SelectedDice = hit.collider.GetComponent<Dice>();

            }
        }

        if (DiceRolled == true && SelectedDice.diceSleeping == true)
        {
            DiceRoll = SelectedDice.diceRoll;
            Text.SetText("Dice Rolled: " + DiceRoll);
            DiceRolled = false;
        }
    }
}
