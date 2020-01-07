using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ramp_activator : MonoBehaviour
{
    [SerializeField] UnitEvent dialogueIn;
    [SerializeField] UnitEvent boardIn;
    [SerializeField] AnimationCurve curve;
    // Start is called before the first frame update
    void Start()
    {
        boardIn.AddListener(() => StartCoroutine(go_up()));
        dialogueIn.AddListener(() => StartCoroutine(go_down()));
    }

    Vector3 set_y(Vector3 src, float y)
    {
        return new Vector3(src.x, y, src.z);
    }

    IEnumerator go_up()
    {
        float time = 0;
        while(transform.position.y < 0)
        {
            transform.position = set_y(transform.position, curve.Evaluate(time) -1);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

    }

    IEnumerator go_down()
    {
        while(transform.position.y > -.8f)
        {
            yield return null;
            transform.position += Vector3.down * Time.deltaTime / (1.2f + transform.position.y) * .25f;
        }

        transform.position = new Vector3(transform.position.x, -1, transform.position.z);
    }
    
}
