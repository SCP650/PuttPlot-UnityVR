using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adhoc_turn_off_collider : MonoBehaviour
{
    [SerializeField]
    IntEvent hithole;

    // Start is called before the first frame update
    void Start()
    {
        hithole.AddListener(_ => GetComponent<SphereCollider>().enabled = false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
