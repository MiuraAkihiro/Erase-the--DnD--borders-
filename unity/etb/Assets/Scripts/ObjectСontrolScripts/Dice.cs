using UnityEngine;

public class Dice : MonoBehaviour
{
    public bool diceSleeping;
    public bool deterministicRoll;
    public int diceRoll;
    public int diceRollDeterministic;
    public diceTypeList diceType;
    private float topSide;
    [Range(0, 10000)]
    public float edgelandingforce = 1000f;
    public bool edgelandinginverter = false;
    public Rigidbody myRigidbody;

    public enum diceTypeList
    {
        D4 = 4,
        D6 = 6,
        D8 = 8,
        D10 = 10,
        D12 = 12,
        D16 = 16,
        D20 = 20,
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (myRigidbody.IsSleeping())
        {
            if (!diceSleeping)
            {
                topSide = -50000;

                for (int index = 0; index < (int)diceType; index++)
                {
                    var getChild = gameObject.transform.GetChild(index);
                    if (getChild.position.y > topSide)
                    {
                        topSide = getChild.position.y;
                        diceRoll = index + 1;

                        Debug.Log("Roll:" + diceRoll);
                    }
                }
            }
            diceSleeping = true;
        }
        else
        {
            diceSleeping = false;
        }

    }
}
