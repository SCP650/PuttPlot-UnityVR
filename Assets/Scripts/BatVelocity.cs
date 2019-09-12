using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatVelocity : MonoBehaviour
{
    
    Vector3 oldPosition, newPosition, difference;
    public Vector3 Velocity { get; private set; }

    private void Start()
    {
        oldPosition = transform.position;
    }

    private void Update() 
    {
        newPosition = transform.position;
        difference =  (newPosition - oldPosition);
        Velocity = difference / Time.deltaTime;
        oldPosition = newPosition;
        newPosition = transform.position; //TODO check rigidbody instead
    }
}
