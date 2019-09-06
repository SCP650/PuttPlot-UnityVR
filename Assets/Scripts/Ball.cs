using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float thrust;
    private Rigidbody rb;
    public bool applyForce;
    private Vector3 force, position;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        applyForce = false;
    }

    private void FixedUpdate()
    {
        if(applyForce)
        {
            rb.AddForceAtPosition(force * thrust, position);
            applyForce = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.gameObject.tag == "Golf Club")
        {
            applyForce = true;
            ContactPoint contact = other.contacts[0];
            force = other.contacts[0].normal;
            position = contact.point;
        }    
    }
}
