using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AngleRange
{
    [SerializeField][Range(-180,0)]
    public float low;

    [SerializeField][Range(0,180)]
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
        var x = Mathf.LerpAngle(horizontal.low,horizontal.high,normalizedX);

        var y = Mathf.LerpAngle(vertical.low, vertical.high, normalizedF);

        var F = Mathf.Lerp(lowMagnitude, highMagnitude, normalizedF);

        var result = new Vector3(Mathf.Cos(ToRadians(x)), Mathf.Sin(ToRadians(y)), 1).normalized * F;
        

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
