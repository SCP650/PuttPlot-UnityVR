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

    [SerializeField] float speed;

    [SerializeField] UnitEvent dialogueIn;

    [SerializeField] IntEvent HoleIn;

    [SerializeField] FrameRef currentFrame;

    [SerializeField] GameObject chatBoxLastQuestion;

    private GameObject currentBoard, lastQuestion;
    // Start is called before the first frame update
    void Start()
    {
        lastQuestion = Instantiate(chatBoxLastQuestion);
        lastQuestion.transform.parent = this.transform;
        lastQuestion.SetActive(false);
        if (eventObjectPairs.Count == 0) return;
        currentBoard = null;
        foreach(var pi in eventObjectPairs)
        {
            pi.Item2.SetActive(false);
            pi.Item2.transform.position = Vector3.up * -150;
            pi.Item1.AddListener(_ =>
            {
                foreach (var lr in FindObjectsOfType<LineRenderer>()) (lr).enabled = false;
                StartCoroutine(goIn(pi.Item2));
            });
        }

        HoleIn.AddListener(_ => StartCoroutine(goDown(currentBoard)));
    }

    IEnumerator goIn(GameObject board)
    {
        if (currentBoard != null && currentBoard != board) StartCoroutine(goDown(currentBoard));
        var choiceref = board.GetComponentInChildren<DialogueChoiceRef>();
        if(choiceref)
        {
            foreach (var text in choiceref.dialouges)
            {
                text.text = "";
            }
        }

        
        board.transform.localPosition = Vector3.up * 20;
        board.SetActive(true);
        while(board.transform.localPosition.y > 0)
        {
            board.transform.localPosition -= Vector3.up * Time.deltaTime * speed;
            yield return null;
        }
        currentBoard = board;

        lastQuestion.SetActive(true);

        if (currentFrame.val.lines.Count > 0)
        {
            if ((currentFrame.val.lines[currentFrame.val.lines.Count - 1].line == "..." || currentFrame.val.lines[currentFrame.val.lines.Count - 1].line == "") && currentFrame.val.lines.Count > 1)
            {
                lastQuestion.transform.GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>().text = currentFrame.val.lines[currentFrame.val.lines.Count - 2].line;
            }
            else
            {
                lastQuestion.transform.GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>().text = currentFrame.val.lines[currentFrame.val.lines.Count - 1].line;
            }
        }

        if (choiceref)
        {
            var l = board.GetComponentInChildren<DialogueChoiceRef>().dialouges;
            for (int i = 0; i < l.Count; i++)
            {
                l[i].text = currentFrame.val.visible_choices[i].three_words;
                //l[i].transform.localRotation = Quaternion.Euler(-5, 0, 0);
            }
        }
        foreach(var lr in board.GetComponentsInChildren<LineRenderer>())
        {
            lr.enabled = true;
        }
        
    }

    IEnumerator goDown(GameObject down)
    {
        foreach (var lr in down.GetComponentsInChildren<LineRenderer>())
        {
            lr.enabled = false;
        }
        lastQuestion.SetActive(false);
        print($"may go down: {down}");
        if(down == null) yield break;
        print($"went down with: {down}");
        while (down.transform.position.y > -30f)
        {
            down.transform.localPosition -= Vector3.up * Time.deltaTime * speed;
            yield return null;
        }
        down.SetActive(false);
        currentBoard = null;
    }

}
