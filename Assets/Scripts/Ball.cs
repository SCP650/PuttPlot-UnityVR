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

    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.tag == "Board")
        {
            audio.PlayOneShot(audioClips[1]);
        }

        if (other.collider.gameObject.tag == "Golf Club")
        {
            audio.PlayOneShot(audioClips[3]);

            ContactPoint contact = other.contacts[0];
            rb.velocity = other.collider.transform.GetChild(0).GetComponent<BatVelocity>().Velocity * thrust;
        }
      
    }
}
