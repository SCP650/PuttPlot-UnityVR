﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public enum Character { player, date, dog}

[Serializable]
public struct line_of_dialogue
{
    [SerializeField] [TextArea] public string line;

    [SerializeField] public bool animated;

    [SerializeField] public Sprite bg;

    [SerializeField] public string animationTrigger;

    [SerializeField] public int lowestScoreAllowed;

    [SerializeField] public int highestScoreAllowed;//TODO incorporate
    
    [SerializeField] public Character speaker;

     public AudioClip voiceLine;
}

[CreateAssetMenu(menuName = "Dialogue/Frame")]
public class Frame : ScriptableObject
{
    /*
     * visible_choices is the list of dialogue choices in the order that they are presented
     * The holes will match up with visible_choices based of off their index here.
     * 
     */
    [SerializeField] public List<DialogueChoice> visible_choices;

    [SerializeField] public List<line_of_dialogue> lines;

    [SerializeField] private UnityEvent onEnter;

    [SerializeField] private UnityEvent onExit;

    [SerializeField] public bool noBoard;

    public DialogueChoice this[int index]
    {
        get
        {
            if(visible_choices == null)
                Debug.LogError($"{this} dialogue choices are null");

            if (visible_choices.Count <= index)
                Debug.LogError($"Frame only has {visible_choices.Count} dialogue options, index: {index}");
                
            return visible_choices[index];
        }
    }

    public void Enter()
    {
        onEnter.Invoke();
    }

    public void Exit()
    {
        onExit.Invoke();
    }
}
