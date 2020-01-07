using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class no_outline : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(var tmp in GetComponentsInChildren<TMPro.TMP_Text>())
        {
            tmp.outlineWidth = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
