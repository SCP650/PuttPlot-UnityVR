using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnitUnityEvent : UnityEvent<TypeUtil.Unit> {}

[CreateAssetMenu(menuName = "Framework/Events/Unit")]
public class UnitEvent : EventObject<TypeUtil.Unit,UnitUnityEvent> {}