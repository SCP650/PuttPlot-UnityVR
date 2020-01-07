using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBallRamp : MonoBehaviour
{

    [SerializeField] float thrust = 5.0f;

 
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Collider>().GetType() == typeof(SphereCollider))
        {

            Vector3 upTheRamp = -10 * transform.parent.transform.right * thrust;

            other.GetComponent<Rigidbody>().AddForce(upTheRamp);
        }
    }

}
