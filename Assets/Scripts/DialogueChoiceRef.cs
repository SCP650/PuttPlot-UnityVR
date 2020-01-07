using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChoiceRef: MonoBehaviour
{
    public
    List<TMPro.TMP_Text> dialouges;
    private void Start()
    {
        foreach(TMPro.TMP_Text text in dialouges) 
        {
        }
    }
}
