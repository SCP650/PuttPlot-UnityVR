using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using Valve.VR;
using Valve.VR.InteractionSystem;


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

    EndOfRampBoost carterisbad;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        carterisbad = FindObjectOfType<EndOfRampBoost>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Golf Club")
        {
               
            

            if (momentum.val.magnitude > threshold_for_power)
            {
                audio.PlayOneShot(audioClips[2]);
                rb.AddForce(thrust * new Vector3(momentum.val.x, 0, momentum.val.z), ForceMode.VelocityChange); //ignores vertical momentum for speed. maybe we should multiply by mag and noramilze the 2d vector
                vib(100);
            }
            else
            {
                rb.AddForce(thrust * new Vector3(momentum.val.x, 0, momentum.val.z) * .1f, ForceMode.VelocityChange);
                
            }
            if(momentum.val.magnitude > carterisbad.Momentum_high_threshold)
            {
                audio.PlayOneShot(audioClips[3]);
                vib(1000);
            }
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }

    }

    public void vib(float dur)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        HapticCapabilities capabilities;
        if (device.TryGetHapticCapabilities(out capabilities))
        {
            if (capabilities.supportsImpulse)
            {
                uint channel = 0;
                float amplitude = 0.5f;
                dur = 1.0f;
                device.SendHapticImpulse(channel, amplitude, dur);
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
