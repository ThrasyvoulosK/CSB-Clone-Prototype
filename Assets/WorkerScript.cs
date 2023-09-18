using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : Person
{
    //Transform tableOrder;
    public enum State { Idle, GoingToTakeOrder, TakingOrder, Serving, GettingPaid, WaitingToLeave };
    public State state;

    //float speed = 10f;
    Transform foodMachine;
    // Start is called before the first frame update
    void Start()
    {
        //transform = transform;
        state = 0;

        tableOrder = GameObject.Find("Table").transform.GetChild(0).transform.Find("WorkerPosition");

        foodMachine= GameObject.Find("FoodMachine").transform.Find("WorkerPosition");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        StateHandler();
    }
    private void StateHandler()
    {
        //throw new NotImplementedException();
        switch (state)
        {
            case State.Idle:
                break;
            case State.GoingToTakeOrder:
                MoveTo(tableOrder);
                break;
            case State.TakingOrder:
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
    /*private void MoveTo(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Worker Collision");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Worker Trigger" + collision.name);
        //check parent of collision object
        if (collision.transform.parent.name == "TableSlot" && state == State.GoingToTakeOrder)
        {
            state++;
        }
        else if (collision.transform.parent.name == "FoodMachine" && state == State.TakingOrder)
        {
            state++;
        }
        else if (collision.transform.parent.name == "TableSlot" && state == State.Serving)
        {
            state++;
            EndOrder();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if the worker is already inside their tableslot don't try to re-enter the collider
        if (collision.transform.parent.name == "TableSlot" && state == State.GoingToTakeOrder)
        {
            state++;
        }
    }

    private void EndOrder()
    {
        //notify customer
        CustomerScript customer = FindObjectOfType<CustomerScript>();
        customer.state = CustomerScript.State.Eating;
    }
}
