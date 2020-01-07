using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CalibrateLauncher : MonoBehaviour
{
    [SerializeField]
    LaunchConfig launchConfig;

    [SerializeField]
    Rigidbody ball_prefab;

    [SerializeField]
    Transform leftEdge;

    [SerializeField]
    Transform rightEdge;

    [SerializeField]
    [Range(-1, 1)]
    float X;

    [SerializeField]
    [Range(0, 1)]
    float F;
    

    public Vector3 Ball_pos { get => Vector3.Lerp(leftEdge.position,rightEdge.position,(X + 1) / 2); }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(RapidLaunch(launchConfig.CalibrateLow,() => leftEdge.position));
        //StartCoroutine(RapidLaunch(launchConfig.CalibateHigh,() => rightEdge.position));
        StartCoroutine(RapidLaunch(() => launchConfig.CalcForce((X + 1) / 2,F),() =>  Ball_pos));
    }

    IEnumerator RapidLaunch(Func<Vector3> dir, Func<Vector3> startPos)
    {
        while(true)
        {
            yield return new WaitForSeconds(.333f);
            var g = GameObject.Instantiate(ball_prefab.gameObject, startPos(), Quaternion.identity);
            g.GetComponent<Rigidbody>().AddForce(dir(), ForceMode.VelocityChange);
            StartCoroutine(KillMeDotExe(g));
        }
    }

    IEnumerator KillMeDotExe(GameObject nani)
    {
        yield return new WaitForSeconds(3);
        Destroy(nani);
    }
}
