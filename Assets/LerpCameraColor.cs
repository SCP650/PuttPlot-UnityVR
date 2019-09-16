using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCameraColor : MonoBehaviour
{
    [SerializeField]
    Gradient cg;

    [SerializeField]
    Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cam.backgroundColor = cg.Evaluate(Mathf.PingPong(Time.time * .075f,1f));
    }
}
