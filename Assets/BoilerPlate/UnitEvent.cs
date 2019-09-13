using TypeUtil;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnitUnityEvent : UnityEvent<TypeUtil.Unit> {}

[CreateAssetMenu(menuName = "Framework/Events/Unit")]
public class UnitEvent : EventObject<TypeUtil.Unit, UnitUnityEvent>
{
    public void Invoke()
    {
        this.Invoke(new Unit());
    }
}