using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{

    [SerializeField] private IntEvent gotodo;
    [SerializeField] private IntEvent listener;
    
    // Start is called before the first frame update
    void Start()
    {
        listener.AddListener(gotodo.Invoke);
    }


}
