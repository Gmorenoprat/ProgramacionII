using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public Rigidbody rb;
    public int speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(GetComponent<Transform>().forward * speed,ForceMode.Impulse);
    }

}
