using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person :MonoBehaviour
{
    //public Transform transform;
    public Transform tableOrder;
    public float speed = 1f;

    public void MoveTo(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
