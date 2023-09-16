using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : MonoBehaviour
{
    Transform tableOrder;
    enum State { Idle, GoingToTakeOrder, TakingOrder, WaitingToEat, GettingPaid, WaitingToLeave };
    State state;

    float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    private void MoveTo(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
