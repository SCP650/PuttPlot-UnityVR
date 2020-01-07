using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoardBlink : MonoBehaviour
{
    CanvasGroup group;
    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        
        StartCoroutine(BlinkingText());
    }

    private IEnumerator BlinkingText() {
        while (true) {
            group.alpha = Mathf.PingPong(Time.time * 1.5f, 0.8f)+0.2f;
            yield return null;
        }
        
    }
  
}
