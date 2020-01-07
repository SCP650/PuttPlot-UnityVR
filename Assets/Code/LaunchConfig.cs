using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AngleRange
{
    [SerializeField]
    public float low;

    [SerializeField]
    public float high;
}

[CreateAssetMenu(menuName ="Launch Config")]
public class LaunchConfig : ScriptableObject
{
    [SerializeField]
    AngleRange horizontal;

    [SerializeField]
    AngleRange vertical;


    [SerializeField]
    float lowMagnitude;

    [SerializeField]
    float highMagnitude;

    public Vector3 CalcForce(float normalizedX,float normalizedF)
    {
        var x = Mathf.Lerp(horizontal.low,horizontal.high,normalizedX);
        
        var y = Mathf.Lerp(vertical.low, vertical.high, normalizedF);

        var F = Mathf.Lerp(lowMagnitude, highMagnitude, normalizedF);
        Debug.Log($"x: {x}, y: {y}");

        var result = new Vector3(x, y, 1).normalized * F;
       
        return result ;
    }

    public Vector3 CalibrateLow()
    {
        return CalcForce(0, 0);
    }

    public Vector3 CalibateHigh()
    {
        return CalcForce(1, 1);
    }

    float ToRadians(float eulers) { return eulers * Mathf.Deg2Rad; }
}
