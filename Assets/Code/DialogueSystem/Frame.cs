using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct line_of_dialogue
{
    [SerializeField] [TextArea] public string line;

    [SerializeField] public bool animated;

    [SerializeField] public Sprite bg;

    [SerializeField] public string animationTrigger;
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

    public DialogueChoice this[int index]
    {
        get
        {
            if(visible_choices == null)
                Debug.LogError($"{this} dialogue choices are null");

            if (visible_choices.Count <= index)
            {
                Debug.LogError($"Frame only has {visible_choices.Count} dialogue options, index: {index}");
                return null;
            }
            return visible_choices[index];
        }
    }
    

}
