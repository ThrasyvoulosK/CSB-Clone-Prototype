using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person :MonoBehaviour
{
    //public Transform transform;
    public Transform tableOrder;
    float speed = 10f;

    public void MoveTo(Transform target)
    {  
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public IEnumerator WaitThenMove(float time,Transform target)
    {
        yield return new WaitForSeconds(time);
        print("WaitAndPrint " + Time.time);
        MoveTo(target);
        //yield break;
    }
    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        //Debug.Log("Waiting");
    }
}
