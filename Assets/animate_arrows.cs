using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animate_arrows : MonoBehaviour
{
    List<SpriteRenderer> srs;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in transform) StartCoroutine(anim(t.GetComponent<SpriteRenderer>()));
    }

    IEnumerator anim(SpriteRenderer sr)
    {
        Vector3 hold = sr.transform.position;
        while(true)
        {
            sr.color = set_alpha(sr.color, Mathf.PingPong(Time.time / 4, .5f) + .4f);
            //sr.transform.localScale = warp(Mathf.PingPong(Time.time * .5f + 1, .05f) + .15f);
            //sr.transform.position = hold + Vector3.forward * .0125f * (Mathf.PingPong(Time.time + 1, .3f) + 7f);
            yield return null;
        }
    }

    Vector3 warp(float norm)
    {
        return new Vector3(norm,norm + 1.2f,1);
    }

    Color set_alpha(Color c, float a)
    {
        return new Color(c.r, c.g, c.b, a);
    }
}

