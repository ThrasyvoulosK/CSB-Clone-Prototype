using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : Person
{
    Transform exit;
    //[SerializeField]
    //Transform tableOrder;
    enum State { Idle,GoingToOrder,Ordering,Eating,Paying,Leaving};
    State state;

    bool orderPosition = false;

    //float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        //transform = GetComponent<Transform>;
        state = State.Idle;
        tableOrder = GameObject.Find("Table").transform.GetChild(0).transform.Find("CustomerPosition");
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
                if (tableOrder == transform)
                    state++;
                break;
            case State.Ordering:
                Order();
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
        worker.state = WorkerScript.State.GoingToTakeOrder;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Customer Collision");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Customer Trigger");
        Order();
    }

    /*private void MoveTo(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position,speed*Time.deltaTime);
    }*/
}
