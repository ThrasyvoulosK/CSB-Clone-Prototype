using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField]
    GameObject customer;

    GameObject newCustomer;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        //respawn child if the last one has finished its cycle
        if (gameObject.transform.childCount == 0)
            Spawn();
    }

    void Spawn()
    {
        newCustomer = (GameObject)Instantiate(customer, transform);
    }
}
