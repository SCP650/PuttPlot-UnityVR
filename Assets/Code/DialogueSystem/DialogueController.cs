using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] Frame startingFrame;

    [SerializeField] private IntEvent selectDialogue;

    [SerializeField] private IntRef Score;

    [SerializeField] private TMPro.TMP_Text mainDialogue;//bad TODO be a better human

    [SerializeField] private Image background;

    [SerializeField] private Animator characterAnimator;

    [SerializeField] private GridLayoutGroup dialogueChoices;

    [SerializeField] private TMPro.TMP_Text dialogueChoicePrefab;

    [SerializeField] private float textSpeed = 10;

    private Frame currentFrame;
    
    void Start()
    {
        currentFrame = startingFrame;
        StartCoroutine(ReadDialogue());
    }

    IEnumerator ReadDialogue()
    {
        currentFrame.Enter();
        string temp_string;
        foreach (Transform transform in dialogueChoices.transform)
        {
            Destroy(transform.gameObject);
        }
        yield return null;
        foreach (line_of_dialogue line in currentFrame.lines)
        {
            background.sprite = line.bg;
            if(characterAnimator != null)    
                characterAnimator.SetTrigger(line.animationTrigger);
            temp_string = line.line;
            mainDialogue.text = "";
            while (temp_string.Length > 0)
            {
                mainDialogue.text += temp_string[0];
                temp_string = temp_string.Remove(0,1);
                yield return  new WaitForSeconds(1f / textSpeed);
            }
            
        }

        for(int i = 0; i < currentFrame.visible_choices.Count;i++)
        {
            var tmp = GameObject.Instantiate(dialogueChoicePrefab, dialogueChoices.transform);
            tmp.text = currentFrame.visible_choices[i].text;
            tmp.transform.GetComponentInChildren<Image>().color = i == 0 ? Color.blue : (i == 1 ? Color.red : i == 2 ? Color.green : i == 3 ? Color.white : Color.black);
            tmp.transform.GetComponentInChildren<Image>().color *= new Color(1, 1, 1, .35f);
        }
        currentFrame.Exit();
        beginListening();
    }

    int foo(string bar)
    {
        bar = "3";
        
        return 0;
    }
    
    
    private void beginListening()
    {
        
        selectDialogue.AddListenerOneTime(id =>
        {
            currentFrame = currentFrame[id].destination;
            Score.val += currentFrame[id].deltaScore;
            StartCoroutine(ReadDialogue());
        });
    }
    
}
