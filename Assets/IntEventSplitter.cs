using System.Collections;
using System.Collections.Generic;
using TypeUtil;
using UnityEngine;
using UnityEngine.Events;

public class IntEventSplitter : MonoBehaviour
{
    [SerializeField] private List<UnityEvent> Events;

    [SerializeField] private IntEvent inEvent;
    // Start is called before the first frame update
    void Start()
    {
        inEvent.AddListener(i => Events[i].Invoke()); //oof bad out of bounds TODO
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
