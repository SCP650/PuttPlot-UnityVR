using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Reference<T> : ScriptableObject
{
    [SerializeField] private T value;
    public T val
    {
        get { return value;}
        set { this.value = value; }
    }
    
}