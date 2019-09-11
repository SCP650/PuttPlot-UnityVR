using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float thrust;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(record());
    }

    IEnumerator record()
    {
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                thrust *= 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                thrust /= 2;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.gameObject.tag == "Golf Club")
        {
            ContactPoint contact = other.contacts[0];
            rb.AddForceAtPosition(other.contacts[0].normal * thrust,contact.point,ForceMode.Impulse);
        }    
    }
}
