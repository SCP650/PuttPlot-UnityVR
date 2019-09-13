using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float thrust;

    [SerializeField]
    private IntEvent checkBallHit;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.gameObject.tag == "Golf Club")
        {
            ContactPoint contact = other.contacts[0];
            rb.velocity = other.collider.transform.GetChild(0).GetComponent<BatVelocity>().Velocity * thrust;
        }
    }
}
