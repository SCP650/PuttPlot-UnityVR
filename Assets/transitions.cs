using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TypeUtil;

[System.Serializable]
public class EventObjectPair
{

    [SerializeField] public UnitEvent Item1;
    [SerializeField] public GameObject Item2;
}

public class transitions : MonoBehaviour
{
    [SerializeField] private List<EventObjectPair> eventObjectPairs;

    private GameObject currentBoard;
    // Start is called before the first frame update
    void Start()
    {
        if (eventObjectPairs.Count == 0) return;
        currentBoard = eventObjectPairs[0].Item2;
        foreach(var pi in eventObjectPairs)
        {
            pi.Item2.transform.position = Vector3.up * -150;
            pi.Item1.AddListener(_ =>
            {
                StartCoroutine(goDown(currentBoard,pi.Item2));
            });
        }
    }

    IEnumerator goDown(GameObject down, GameObject up)
    {
        while (down.transform.position.y > -150 && up.transform.position.y < 0)
        {
            down.transform.position -= Vector3.up * 4 * Time.deltaTime / (down.transform.position.y + 150f);
            up.transform.position = Vector3.up * (down.transform.position.y + 150f);
            yield return null;
        }
    }

}
