using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[System.Serializable]
public struct override_frame
{
    public UnitEvent trigger;
    public Frame frame;
}

public class DialogueController : MonoBehaviour
{
    [SerializeField] Frame startingFrame;

    [SerializeField] private IntEvent selectDialogue;

    [SerializeField] UnitEvent dialogueIn;

    [SerializeField] private IntRef Score;

    [SerializeField] private TMPro.TMP_Text mainDialogue;//bad TODO be a better human

    [SerializeField] private Image background;
    [SerializeField] GameObject screens;

    [SerializeField] private Animator characterAnimator;


    [SerializeField] private float textSpeed = 10;

    [SerializeField] BoolRef playerIsWatching;

    [SerializeField] FrameRef currentFrame;

    [SerializeField] Color playerColor;

    [SerializeField] Color dateColor;

    [SerializeField] Color dogColor;

    [SerializeField] Sprite blank;

    [SerializeField] AudioSource voiceGirl;

    [SerializeField] GameObject chatBox;

    [SerializeField] GameObject ChatHistoryRightScreenPrefab, ChatHistoryLeftScreenPrefab;

    [SerializeField] LineRenderer[] lrs;

    [SerializeField] override_frame[] override_Frames;

    [SerializeField] AudioSource drum_roll;

    [SerializeField] ParticleSystem[] confetti;

    [SerializeField] Woofer woofer;
    

    private GameObject ChatHistoryRightScreen, ChatHistoryLeftScreen;

    void Start()
    {
        Score.val = 0;
        currentFrame.val = startingFrame;
        if (ChatHistoryRightScreenPrefab != null)
            ChatHistoryRightScreen = Instantiate(ChatHistoryLeftScreenPrefab);
        else
            Debug.LogError("Right chat screen prefab not attached");

        if (ChatHistoryLeftScreenPrefab != null)
            ChatHistoryLeftScreen = Instantiate(ChatHistoryLeftScreenPrefab);
        else
            Debug.LogError("Left chat screen prefab not attached");
        
        StartCoroutine(ReadDialogue());
        foreach(var pi in override_Frames)
        {
            pi.trigger.AddListener(() =>
            {
                StopAllCoroutines();
                currentFrame.val = pi.frame;
                StartCoroutine(ReadDialogue());
            });
        }
    }

    IEnumerator blackhole_spewer(bool did_win, System.Action cont)
    {
        drum_roll.Play();
        yield return null;
        yield return new WaitUntil(() => !drum_roll.isPlaying);
        foreach(var confett in confetti)
        {
            if(true) confett.Play();
        }
        StopAllCoroutines();
        cont();
    }

    IEnumerator ReadDialogue()
    {
        yield return null;
        currentFrame.val.Enter();

        dialogueIn.Invoke();
        print($"entering current frame: {currentFrame.val}");
        string temp_string;
        screens.SetActive(true);
        yield return null;
        foreach (line_of_dialogue line in currentFrame.val.lines)
        {
            if (Score.val <= line.lowestScoreAllowed && line.lowestScoreAllowed != 0)
                continue;
            if (Score.val >= line.highestScoreAllowed && line.highestScoreAllowed != 0)
                continue;
            background.sprite = line.bg;
            if (characterAnimator != null)
                characterAnimator.SetTrigger(line.animationTrigger);

            Color tempColor;
            switch (line.speaker)
            {
                case Character.date:
                    tempColor = dateColor;
                    break;
                case Character.dog:
                    tempColor = dogColor;
                    break;
                case Character.player:
                    tempColor = playerColor;
                    break;
                default:
                    tempColor = Color.clear;
                    break;
            }
            yield return StartCoroutine(readText(line.line, tempColor, line.speaker,line.voiceLine, nop(),line.bg != blank));
            
        }
        currentFrame.val.Exit();

        if (!currentFrame.val.noBoard)
        {
            beginListening();
        } else {  //no board
            if(currentFrame.val.visible_choices.Count == 1)
            {
                changeFrame(0);
            } else
            {
                if (Score.val >= 2)
                {
                    StartCoroutine(blackhole_spewer(true, () => changeFrame(0)));
                    
                }
                else
                {
                    StartCoroutine(blackhole_spewer(false, () => changeFrame(1)));
                    
                }

            }
        }
    }

    IEnumerator nop()
    {
        yield return null;
    }



    IEnumerator readText(string line, Color color, Character speaker, AudioClip voiceLine, IEnumerator cont, bool bark)
    {
        var temp_string = line;

        mainDialogue.text = "";
        switch (speaker)
        {
            case Character.player:
                if (voiceLine)
                    voiceGirl.PlayOneShot(voiceLine);
                break;
            case Character.date:
                if(voiceLine)   
                    voiceGirl.PlayOneShot(voiceLine);
                break;
            case Character.dog:
                if(bark)
                    StartCoroutine(woofer.woof(temp_string,voiceGirl));
                break;
        }
        mainDialogue.color = color;        mainDialogue.GetComponentInChildren<Image>().enabled = true;
        mainDialogue.text = temp_string;
        mainDialogue.maxVisibleCharacters = 0;
        while (temp_string.Length > 0)
        {
            yield return new WaitUntil(() => playerIsWatching.val);
            if(temp_string[0].Equals("{"))
            {
                
                string dur = "";
                for(temp_string = temp_string.Remove(0, 1); !temp_string[0].Equals("}") ; temp_string = temp_string.Remove(0, 1))
                {
                    dur += temp_string[0];
                    if(temp_string.Equals(""))
                    {
                        Debug.Log($"unmatched curly brace }} at line: {line}");
                    }
                }
                temp_string = temp_string.Remove(0, 1);
                int actual_dur;
                if (int.TryParse(dur, out actual_dur))
                {
                    yield return new WaitForSeconds(.1f * actual_dur);
                } else
                {
                    Debug.Log($"Pause duration is not in the form of an int in line: {line}");
                }
                if (temp_string.Length == 0) break;
                mainDialogue.text = temp_string;
            }
            //mainDialogue.text += temp_string[0];
            mainDialogue.maxVisibleCharacters++;
            temp_string = temp_string.Remove(0, 1);
            yield return new WaitForSeconds(1f / textSpeed);
        }
        
        yield return new WaitForSeconds(1.75f);
        yield return new WaitUntil(() => !voiceGirl.isPlaying);

        if (ChatHistoryRightScreen != null)
            StartCoroutine(EnterChatHistory(line, color, speaker, ChatHistoryRightScreen, Vector3.zero));

        if (ChatHistoryLeftScreen != null)
            StartCoroutine(EnterChatHistory(line, color, speaker, ChatHistoryLeftScreen, new Vector3(-10.8f, 0, 0)));

        StartCoroutine(cont);
    }

    IEnumerator EnterChatHistory(string chatLine, Color colorOfBox, Character speaker, GameObject chatHistoryScreen, Vector3 offset)
    {
        int numberOfChats = chatHistoryScreen.transform.childCount;
        while (numberOfChats > 0)
        {
            chatHistoryScreen.transform.GetChild(numberOfChats - 1).position += new Vector3(0, 0.6f, 0);
            numberOfChats--;
        }

        GameObject temp = Instantiate(chatBox);

        if (offset != Vector3.zero)
        {
            temp.transform.position += offset;
            temp.transform.Rotate(0, -180, 0);
        }

        if (speaker != Character.player && offset != Vector3.zero)
        {
            temp.transform.position += new Vector3(0, 0, -0.6f);
        }
        else if (speaker == Character.player && offset == Vector3.zero)
        {
            temp.transform.position += new Vector3(0, 0, -0.6f);
        }

        temp.transform.parent = chatHistoryScreen.transform;
        temp.GetComponent<TMPro.TMP_Text>().text = chatLine;
        temp.GetComponent<TMPro.TMP_Text>().color = colorOfBox;
        yield return null;
    }

    private void beginListening()
    {
        mainDialogue.GetComponentInChildren<Image>().enabled = false;
        background.sprite = blank;
        screens.SetActive(false);
        mainDialogue.text = "";
        selectDialogue.AddListenerOneTime(changeFrame);
    }

    void changeFrame(int id)
    {
        print($"id: {id} source frame: {currentFrame.val.name}");
        var text = currentFrame.val[id].text;
        Score.val += currentFrame.val[id].deltaScore;
        var voice = currentFrame.val[id].line.voiceLine;
        currentFrame.val = currentFrame.val[id].destination;
        StartCoroutine(readText(text, playerColor,Character.player,voice, ReadDialogue(),false));
    }

}



