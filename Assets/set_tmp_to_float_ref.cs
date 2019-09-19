using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TMP_Text))]
public class set_tmp_to_float_ref : MonoBehaviour
{
    [SerializeField]
    FloatRef float_ref;

    void Update()
    {
        var text = GetComponent<TMPro.TMP_Text>();
        text.text = float_ref.val.ToString();
    }
}
