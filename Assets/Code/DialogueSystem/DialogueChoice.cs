using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Choice")]
public class DialogueChoice : ScriptableObject
{
    [SerializeField] public Frame destination;

    [SerializeField] public string text;

    [SerializeField] public int deltaScore;
}
