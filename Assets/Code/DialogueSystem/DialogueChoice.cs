using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Choice")]
public class DialogueChoice : ScriptableObject
{
    [SerializeField] public Frame destination;

    [SerializeField][TextArea] public string text;

    [SerializeField] public line_of_dialogue line;

    [SerializeField] public string three_words;

    [SerializeField] public int deltaScore;
}
