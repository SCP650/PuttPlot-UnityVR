using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperSendBall : MonoBehaviour
{
    [SerializeField] float thrust = 5.0f;
    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       

        if (collision.collider.GetType() == typeof(SphereCollider))
        {

            Vector3 ranRight = transform.right * Random.Range(-10f, 10f);
            Vector3 ranForward = transform.forward * Random.Range(2f, 10f);

             collision.collider.GetComponent<Rigidbody>().AddForce((ranRight + ranForward) * thrust);
            audio.Play();
        }
    }


}
