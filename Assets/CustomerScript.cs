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

    //float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        //transform = GetComponent<Transform>;
        state = State.Idle;
        tableOrder = GameObject.Find("Table").transform.GetChild(0).transform.Find("CustomerPosition");
        exit = transform.parent;
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
                MoveTo(tableOrder);
                //CheckGoingToOrder();
                /*if (tableOrder == transform)
                    state++;*/
                break;
            case State.Ordering:
                Order();
                break;
            case State.Eating:
                state = State.Leaving;//temporary
                break;
            case State.Leaving:
                MoveTo(exit);
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
        //notify worker
        WorkerScript worker = FindObjectOfType<WorkerScript>();
        //contact worker only if they're idle
        if (worker.state == WorkerScript.State.Idle)
            worker.state = WorkerScript.State.GoingToTakeOrder;
        else
            Debug.Log("Worker not idle");
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Customer Collision");
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Customer Trigger"+collision.name);
        if(collision.transform.name=="SpawnArea")
        {
            if(state==State.Leaving)
            {
                Destroy(gameObject);
            }
            return;
        }
        else if(collision.transform.parent.name=="TableSlot"&&state==State.GoingToOrder)
            Order();
    }

    /*private void MoveTo(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position,speed*Time.deltaTime);
    }*/
}
