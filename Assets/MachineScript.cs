using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MachineScript : MonoBehaviour
{
    int level;
    int upgradeCost = 1;
    MoneyScript moneyScript;
    int money;
    TextMeshPro upgradeText;
    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        moneyScript = FindAnyObjectByType<MoneyScript>();
        upgradeText = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        upgradeText.SetText(level.ToString());
        money = moneyScript.money;
    }

    void OnMouseDown()
    {
        Debug.Log("clicks "+this.gameObject.name);
        Upgrade();
    }

    private void Upgrade()
    {
        if(money>=upgradeCost)
        {
            Debug.Log("Current Money " + money);
            level++;
            //money -= upgradeCost;
            moneyScript.money--;
        }
    }
}
