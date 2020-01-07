using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class part : MonoBehaviour
{


    [SerializeField] private Transform tip;

    [SerializeField] private Transform floor;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(stretch());
    }

    IEnumerator stretch()
    {
        float length = transform.localScale.x;
        float delta;
        while (true)
        {
            delta = .05f;
            for (int i = 0; i < 1000 && tip.position.y < floor.position.y; i++)
            {
                delta *= 2;
                for (int counter = 0; counter < 20 && tip.position.y < floor.position.y; counter++)
                {
                    length -= delta;
                    transform.localScale = Vector3.up + Vector3.forward + Vector3.right * length;
                }
                yield return null;

            }
            yield return null;
            length += .5f * Time.deltaTime;
            length = Mathf.Clamp01(length);
            transform.localScale = Vector3.up + Vector3.forward + Vector3.right * length;
        }

    }
}