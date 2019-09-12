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

        foreach (DialogueChoice choice in currentFrame.visible_choices)
        {
            GameObject.Instantiate(dialogueChoicePrefab, dialogueChoices.transform).text = choice.text;
        }
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
