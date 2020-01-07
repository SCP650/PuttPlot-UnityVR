using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfRampBoost : MonoBehaviour
{

    [SerializeField]
    LaunchConfig launchConfig;

    [SerializeField]
    Vector3Ref Momentum;

    [SerializeField]
    float momentum_low_threshold;

    [SerializeField]
    float momentum_high_threshold;

    [SerializeField]
    Transform leftEdge;

    [SerializeField]
    Transform rightEdge;

    public float Momentum_high_threshold { get => momentum_high_threshold; set => momentum_high_threshold = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ball>() == null) return;
       // if(other.transform.position.z > transform.position.z)
        //only trigger if the ball is leaving the collider in the
        //correct direction
        {
            StartCoroutine(launch(other));
        }
    }

    float NormalizeX(Vector3 position)
    {
        return Mathf.Clamp01((position.x - leftEdge.position.x)
                          / (rightEdge.position.x - leftEdge.position.x));
    }

    float NormalizeMomentum()
    {
        return Mathf.Clamp01((Momentum.val.magnitude - momentum_low_threshold) 
                          / (momentum_high_threshold - momentum_low_threshold));
    }


    IEnumerator launch(Collider other)
    {
        yield return null;
        yield return null;
        if(NormalizeMomentum() > -.01f)
        {
            var rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.ResetInertiaTensor();
            rb.AddForce(launchConfig.CalcForce(
                NormalizeX(other.transform.position),
                NormalizeMomentum()), ForceMode.VelocityChange);
        }
    }
}
