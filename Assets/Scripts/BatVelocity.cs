using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BatVelocity : MonoBehaviour
{
    
    Vector3 oldPosition, newPosition, difference;

    public Queue<Tuple<float,Vector3>> distance; //delta t * delta p

    float cur_vel;
    
    [SerializeField]
    public Vector3Ref Momentum;
    
    private Vector3 velocity;

    private void Start()
    {
        oldPosition = transform.position;
        distance = new Queue<Tuple<float,Vector3>>();
        StartCoroutine(calculate_momentum());
    }

    IEnumerator calculate_momentum()
    {
        Vector3 last_pos = transform.position;
        Vector3 last_vel;
        float time_in_queue = 0;
        while(true)
        {
            last_pos = transform.position;
            last_vel = velocity;
            yield return null;
            if(did_turn_around(last_vel,velocity))
            {
                Momentum.val = Vector3.zero;
                distance = new Queue<Tuple<float,Vector3>>();
                time_in_queue = 0;
            }
            var dist = (transform.position - last_pos);
            Momentum.val += dist;
            distance.Enqueue(Tuple.Create(Time.deltaTime,dist));
            time_in_queue += Time.deltaTime;
            while (time_in_queue > .3f)
            {
                (var dt, var dp) = distance.Dequeue();
                time_in_queue -= dt;
                Momentum.val -= dp;
                
            }
        }
    }


    bool did_turn_around(Vector3 old_angle, Vector3 new_angle)
    {
        return Vector3.Angle(old_angle,new_angle) > 90;
    }

    private void Update() 
    {
        newPosition = transform.position;
        difference =  (newPosition - oldPosition);
        velocity = difference / Time.deltaTime;
        oldPosition = newPosition;
        newPosition = transform.position; //TODO check rigidbody instead
        cur_vel = Momentum.val.magnitude;
    }
}
