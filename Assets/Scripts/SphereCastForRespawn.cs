using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCastForRespawn : MonoBehaviour
{
    
    bool isEmpty()
    {
        RaycastHit hit;
        return Physics.SphereCast(Vector3.zero, 0.1f,Vector3.zero, out hit,0);
    }
}
