using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : Person
{
    //Transform tableOrder;
    public enum State { Idle, GoingToTakeOrder, TakingOrder, WaitingToEat, GettingPaid, WaitingToLeave };
    public State state;

    //float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        //transform = transform;
        state = 0;

        tableOrder = GameObject.Find("Table").transform.GetChild(0).transform.Find("WorkerPosition");
    }

    // Update is called once per frame
    void Update()
    {
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
            /*case State.Ordering:
                Order();
                break;*/
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
}
