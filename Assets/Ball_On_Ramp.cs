using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))][RequireComponent(typeof(Light))]
public class Ball_On_Ramp : MonoBehaviour
{
    [SerializeField]
    float power = 1;

    private bool onRamp = false;
    private Rigidbody rb;
    Coroutine cancel_boost;
    Light light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(addForce());
    }

    IEnumerator addForce()
    {
        while(true)
        {
            while(onRamp)
            {
                rb.AddForce(rb.velocity.normalized * power,ForceMode.Acceleration);
                GetComponent<TrailRenderer>().widthMultiplier = 0;
                yield return null;
            }
            GetComponent<TrailRenderer>().widthMultiplier = 1;
            yield return null;
        }
    }

    IEnumerator glow()
    {
        if (light.intensity > 0f);
        float dur = 1;
        while(dur > 0)
        {
            dur -= Time.deltaTime;
            yield return null;
            light.intensity += Time.deltaTime * 2;
        }
    }

   

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ramp")
        {
            onRamp = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ramp")
        {

            StartCoroutine(glow());
            onRamp = true;
        }
    }
}

