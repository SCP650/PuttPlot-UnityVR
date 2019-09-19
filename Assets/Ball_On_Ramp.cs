using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball_On_Ramp : MonoBehaviour
{
    [SerializeField]
    float power = 1;

    private bool onRamp = false;
    private Vector3 dir = Vector3.zero;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(addForce());
    }

    IEnumerator addForce()
    {
        while(true)
        {
            while(onRamp)
            {
                rb.AddForce(dir * power,ForceMode.Acceleration);
                yield return null;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ramp")
        {
            dir = collision.transform.right;
            onRamp = true;
        }
    }
}
