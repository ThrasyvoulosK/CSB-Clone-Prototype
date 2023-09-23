using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : Person
{
    Transform exit;
    //[SerializeField]
    //Transform tableOrder;
    public enum State { Idle,GoingToOrder,Ordering,Eating,Paying,Leaving};
    public State state;

    bool orderPosition = false;

    TableScript tableScript;

    TableSlotScript seatTaken;

    bool haveWorker = false;

    public MachineScript foodMachine;

    MoneyScript moneyScript;

    // Start is called before the first frame update
    void Start()
    {
        //transform = GetComponent<Transform>;
        state = State.Idle;
        //tableOrder = GameObject.Find("Table").transform.GetChild(0).transform.Find("CustomerPosition");
        exit = transform.parent;

        tableScript = FindAnyObjectByType<TableScript>();
        FindTable();
        CheckTable();

        moneyScript = FindAnyObjectByType<MoneyScript>();
    }

    private void CheckTable()
    {
        if(seatTaken.slotTaken==true)
        {
            if (seatTaken.customer.transform.parent.name == transform.parent.name)
            {
                Debug.Log("Seat Assigned Correctly");
            }
            else
            {
                Debug.Log("Wrong Seat Assigned to " + seatTaken.customer.transform.parent.name);
                FindTable();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        StateHandler();
    }

    private void StateHandler()
    {
        //throw new NotImplementedException();
        switch(state)
        {
            case State.Idle:
                state = State.GoingToOrder;
                break;
            case State.GoingToOrder:
                //FindTable();
                CheckTable();
                MoveTo(tableOrder);
                break;
            case State.Ordering:
                //StartCoroutine(Wait(1));
                Order();
                break;
            case State.Eating:
                moneyScript.money += 1;
                state = State.Leaving;//temporary
                break;
            case State.Leaving:
                MoveTo(exit);
                seatTaken.slotTaken = false;
                break;
            default:
                Debug.Log("State Error!");
                break;
        }
            
    }

    private bool CheckGoingToOrder()
    {
        throw new NotImplementedException();
    }

    private void Order()
    {
        if (haveWorker == true)
            return;

        //choose food
        ChooseFood();

        //notify worker
        foreach(WorkerScript worker in tableScript.workers)
        {
            //contact worker only if they're idle
            if (worker.state == WorkerScript.State.Idle)
            {
                //set worker's tableslot
                //worker.tableOrder= seatTaken.transform.parent.transform.Find("WorkerPosition");
                worker.tableOrder = seatTaken.transform;
                Debug.Log("customer seat " + seatTaken.name);
                Debug.Log("seat parent" + seatTaken.transform.parent.name);
                Debug.Log("worker seat " + worker.tableOrder.name);
                worker.state = WorkerScript.State.GoingToTakeOrder;
                Debug.Log("Worker " + worker.name + " will work for " + transform.name);

                //state = State.Ordering;
                haveWorker = true;

                worker.currentCustomer = transform.GetComponent<CustomerScript>();

                return;
            }
            else
                Debug.Log("Worker not idle"); 
        }
        Debug.Log("Error! No Workers Available!");
        //WorkerScript worker = FindObjectOfType<WorkerScript>();
        //contact worker only if they're idle
        /*if (worker.state == WorkerScript.State.Idle)
            worker.state = WorkerScript.State.GoingToTakeOrder;
        else
            Debug.Log("Worker not idle");*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Customer Trigger"+collision.name);
        
        if (collision.transform.name.Contains("SpawnArea"))
        {
            if (state == State.Leaving)
            {
                Destroy(gameObject);
            }
            return;
        }
        else if (collision.transform.parent.name.Contains("TableSlot") && state == State.GoingToOrder)
        {
            Debug.Log("Customer " + transform.name + " collides with tableslot " + collision.name);
            Debug.Log("seat name " + seatTaken.name);
            if (collision.transform.parent.name == seatTaken.name)
                state = State.Ordering;//Order();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.name.Contains("SpawnArea"))
        {
            if (state == State.Leaving)
            {
                Destroy(gameObject);
            }
            return;
        }
        if (collision.transform.parent.name.Contains("TableSlot") && state == State.GoingToOrder)
            if (collision.transform.parent.name == seatTaken.name)
                state = State.Ordering;//Order();
    }

    void FindTable()
    {
        foreach(TableSlotScript tableSlot in tableScript.tableSlots)
        {
            if(tableSlot.slotTaken==false)
            {
                tableOrder = tableSlot.transform.Find("CustomerPosition");
                tableSlot.slotTaken = true;
                tableSlot.customer = GetComponent<CustomerScript>();
                seatTaken = tableSlot;
                Debug.Log("Seat taken " + tableSlot.transform.name);
                return;
            }
        }
        Debug.Log("Error! No empty seats found!");
        //tableOrder = GameObject.Find("Table").transform.GetChild(0).transform.Find("CustomerPosition");
    }

    void ChooseFood()
    {
        int foodChoice = UnityEngine.Random.Range(0, tableScript.machines.Count);
        Debug.Log(foodChoice);
        foodMachine = tableScript.machines[foodChoice];
        Debug.Log("Food Machine Chosen: " + foodMachine.transform.name);
    }
}
