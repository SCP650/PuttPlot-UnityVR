using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isSeen : MonoBehaviour
{
    [SerializeField] BoolRef seen;

    [SerializeField] Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        seen.val = (isIn(cam.WorldToViewportPoint(transform.position, Camera.MonoOrStereoscopicEye.Left)));
    }

    bool isIn(Vector3 v)
    {
        return 0 < v.x && v.x < 1 && 0 < v.y && v.y < 1 && v.z > 0;
    }
}
