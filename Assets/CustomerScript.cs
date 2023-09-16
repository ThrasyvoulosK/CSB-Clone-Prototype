using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    Transform exit;
    //[SerializeField]
    Transform tableOrder;
    enum State { Idle,GoingToOrder,Ordering,Eating,Paying,Leaving};
    State state;

    float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
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
                break;
            case State.Ordering:
                Order();
                break;
            default:
                Debug.Log("State Error!");
                break;
        }
            
    }

    private void Order()
    {
        //notify worker
    }

    private void MoveTo(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position,speed*Time.deltaTime);
    }
}
