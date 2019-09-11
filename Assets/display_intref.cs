using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TMP_Text))]
public class display_intref : MonoBehaviour
{

    [SerializeField] private IntRef i;

    private TMPro.TMP_Text txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TMPro.TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = i.val.ToString();
    }
}
