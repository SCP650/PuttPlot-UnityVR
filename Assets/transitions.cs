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
                Debug.Log("event fired");
                StartCoroutine(goDown(currentBoard,pi.Item2));
            });
        }
        currentBoard.transform.localPosition = Vector3.zero;
    }

    IEnumerator goDown(GameObject down, GameObject up)
    {
        if (down == up) yield break;
        while (down.transform.position.y > -30f)
        {
            down.transform.localPosition -= Vector3.up * Time.deltaTime * (15f + Mathf.Abs(up.transform.localPosition.y - 15));
            up.transform.localPosition = Vector3.up * (down.transform.localPosition.y + 30f);
            yield return null;
        }

        up.transform.localPosition = Vector3.zero;
    }

}
