using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : Person
{
    //Transform tableOrder;
    public enum State { Idle, GoingToTakeOrder, TakingOrder, Serving, GettingPaid, WaitingToLeave };
    public State state;

    Transform foodMachine;

    TableScript tableScript;

    public CustomerScript currentCustomer;
    // Start is called before the first frame update
    void Start()
    {
        //transform = transform;
        state = 0;

        //tableOrder = GameObject.Find("Table").transform.GetChild(0).transform.Find("WorkerPosition");

        //foodMachine= GameObject.Find("FoodMachine").transform.Find("WorkerPosition");

        tableScript = FindAnyObjectByType<TableScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(state);
        StateHandler();
    }
    private void StateHandler()
    {
        //throw new NotImplementedException();
        switch (state)
        {
            case State.Idle:
                tableOrder = null;
                break;
            case State.GoingToTakeOrder:
                //FindTable();
                //wait until table is found
                /*if (tableOrder == null)
                    return;*/
                MoveTo(tableOrder);
                break;
            case State.TakingOrder:
                foodMachine = currentCustomer.foodMachine.transform;
                MoveTo(foodMachine);
                break;
            case State.Serving:
                MoveTo(tableOrder);
                break;
            case State.GettingPaid://temporary
                state++;
                break;
            case State.WaitingToLeave:
                state=State.Idle;
                break;
            default:
                Debug.Log("State Error!");
                break;
        }

    }

    /*private void FindTable()
    {
        //tableOrder = null;
        foreach (TableSlotScript tableSlot in tableScript.tableSlots)
        {
            if (tableSlot.slotTaken == true)
            {
                tableOrder = tableSlot.transform.Find("WorkerPosition");
                //tableSlot.slotTaken = false;
                Debug.Log("Worker seat taken " + tableSlot.transform.name);
                return;
            }
        }
        Debug.Log("Error! No empty seats found! for "+transform.name);
        state = State.Idle;
        //Debug.Log(tableOrder.name);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Worker Trigger" + collision.name);
        //check parent of collision object
        if(state==State.Idle)
        {
            return;
        }
        if (collision.transform.parent.name.Contains("TableSlot") && state == State.GoingToTakeOrder)
        {
            state++;
        }
        else if (collision.transform.parent.name == foodMachine.transform.name && state == State.TakingOrder)//"FoodMachine"
        {
            state++;
        }
        else if (collision.transform.parent.name.Contains("TableSlot") && state == State.Serving)
        {
            state++;
            EndOrder();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if the worker is already inside their tableslot don't try to re-enter the collider
        if (collision.transform.parent.name.Contains("TableSlot") && state == State.GoingToTakeOrder)
        {
            state++;
        }
    }

    private void EndOrder()
    {
        //notify customer
        //CustomerScript customer = FindObjectOfType<CustomerScript>();
        CustomerScript customer = currentCustomer;
        Debug.Log("Customer's state is "+customer.state);
        if (customer.state == CustomerScript.State.Ordering)
            customer.state = CustomerScript.State.Eating;
        else
            Debug.Log("Wrong customer state: " + customer.state);
    }
}
