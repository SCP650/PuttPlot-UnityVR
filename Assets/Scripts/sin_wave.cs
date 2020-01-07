using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class sin_wave : MonoBehaviour
{
    [SerializeField]
    Transform p1;

    [SerializeField]
    Transform p2;

    [SerializeField]
    float amp = 1;

    [SerializeField]
    public float freq = 1;

    [SerializeField]
    float resolution = 1;

    //[SerializeField]
    //event_object death;

    [SerializeField]
    float speed = 1;
    
   


    LineRenderer lr;
 
    bool go = true;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    
    }

    // Update is called once per frame
    void Update()
    {
        //TODO check the center to see if it is a cross
        Vector3 delta = p1.position - p2.position;
        RaycastHit2D hit = Physics2D.Raycast(p2.position, delta, delta.magnitude, 1 << LayerMask.NameToLayer("Player"));
        if (hit.transform && go && (Vector2.Distance(hit.transform.position,hit.point) < .61f) || GetComponent<Animation>())
        {
            go = false;
            Invoke(nameof(go_true),.25f);
            //death.Invoke();
        }
        else
        {
            hit = Physics2D.CircleCast(p1.position, .5f, Vector2.zero,0,1 << LayerMask.NameToLayer("Player"));
            if(hit.transform && go)
            {
                //go = false;
                //death.Invoke();
            }
            hit = Physics2D.CircleCast(p1.position, .5f, Vector2.zero, 0, 1 << LayerMask.NameToLayer("Player"));
            if (hit.transform && go)
            {
                //go = false;
                //death.Invoke();
            }
        }
        int len = (int)(delta.magnitude * resolution);
        List<Vector3> pos = new List<Vector3>();
        for(int i = 0; i < len + 1; i++)
        {
            pos.Add(Vector3.Lerp(p1.position, p2.position, i / (float)len));
            Vector3 dis = new Vector3(-delta.y, delta.x, 0f).normalized *
                Mathf.Sin((i / (float)len) * freq  + speed * Time.time) * amp;
            pos[i] += dis;
            if (Mathf.Abs(.5f - i / (float)len) > .1f)
            {
                pos[i] -= dis * (Mathf.Abs(.5f - i / (float)len) / .5f);
            }
        }
        lr.positionCount = pos.Count;
        lr.SetPositions(pos.ToArray());
    }

    void go_true()
    {
        go = true;
    }
}
