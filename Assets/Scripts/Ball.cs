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

    [SerializeField]
    private AudioClip[] audioClips; 

    private Rigidbody rb;
    private AudioSource audio;
   

    [SerializeField]
    float threshold_for_power = 0.075f;

    [SerializeField]
    Vector3Ref momentum;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Golf Club")
        {
            audio.PlayOneShot(audioClips[2]);

            if (momentum.val.magnitude > threshold_for_power)
            {
                rb.AddForce(thrust * new Vector3(momentum.val.x, 0, momentum.val.z), ForceMode.VelocityChange); //ignores vertical momentum for speed. maybe we should multiply by mag and noramilze the 2d vector
            }
            else
            {
                rb.AddForce(thrust * new Vector3(momentum.val.x, 0, momentum.val.z) * .2f, ForceMode.VelocityChange);
            }
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.tag == "Board")
        {
            audio.PlayOneShot(audioClips[0]);
        }

    }
}
