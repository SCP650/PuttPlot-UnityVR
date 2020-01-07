using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Debug/EventTester")]
public class EventTester : ScriptableObject
{
    [SerializeField]
    UnityEvent e;

    public void Invoke()
    {
        e.Invoke();
    }
}
