using System.Collections;
using System.Collections.Generic;
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

    [FormerlySerializedAs("dailogueChoices")] [SerializeField] private GridLayoutGroup dialogueChoices;

    [SerializeField] private TMPro.TMP_Text dialogueChoicePrefab;

    private Frame currentFrame;
    
    void Start()
    {
        currentFrame = startingFrame;
        beginListening();
        StartCoroutine(ReadDialogue());
    }

    IEnumerator ReadDialogue()
    {
        foreach (Transform transform in dialogueChoices.transform)
        {
            Destroy(transform.gameObject);
        }
        yield return null;
        foreach (line_of_dialogue line in currentFrame.lines)
        {
            mainDialogue.text = line.line;
            background.sprite = line.bg;
            if(characterAnimator != null)    
                characterAnimator.SetTrigger(line.animationTrigger);
            
            yield return new WaitForSeconds(line.line.Length * .2f);
        }

        foreach (DialogueChoice choice in currentFrame.visible_choices)
        {
            GameObject.Instantiate(dialogueChoicePrefab, dialogueChoices.transform).text = choice.text;
        }
        beginListening();
    }

    private void beginListening()
    {
        selectDialogue.AddListenerOneTime(id =>
        {
            currentFrame = currentFrame[id].destination;
            StartCoroutine(ReadDialogue());
        });
    }
    
}
