using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntUnityEvent : UnityEvent<int> {}

[CreateAssetMenu(menuName = "Framework/Events/Int")]
public class IntEvent : EventObject<int,IntUnityEvent> {}