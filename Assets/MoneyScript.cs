using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    public int money;
    TextMeshProUGUI moneyAmount;
    // Start is called before the first frame update
    void Start()
    {
        money = 0;
        moneyAmount = GetComponent<TextMeshProUGUI>();
        //moneyAmount.SetText(money.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        moneyAmount.SetText(money.ToString());
    }
}
