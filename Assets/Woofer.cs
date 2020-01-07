using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woofer : MonoBehaviour
{
    [SerializeField] private AudioClip[] barks;

 
    
    public IEnumerator woof(string line,AudioSource woofer)
    {
        if (line == null) yield break;
        
        var words = line.Split(' ');
        foreach (string word in words)
        {
            yield return new WaitForSeconds(.1f);
            if (Random.value > .33f)
            {
                woofer.pitch = 1 + Random.value * .05f - (.05f / 2);
                woofer.PlayOneShot(barks[Random.Range(0,barks.Length) % barks.Length]);
                woofer.pitch = 1;
            }
            yield return new WaitUntil(() => !woofer.isPlaying);
        }
    }
}
