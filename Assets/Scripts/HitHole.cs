using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHole : MonoBehaviour
{
    [SerializeField] int HoleId;
    [SerializeField] public IntEvent hitHole;
    //If you want your function to get the hole id when hole is hit 
    //Register the function you want to call back in your start() sth like this  
    // HitHole hit;
    // hit.OnHitListeners += yourfunction;
    //---Sebastian
    //--I'm carter
    //Imma wreck this sorry


    // Start is called before the first frame update
    void Start()
    {
        
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().GetType() == typeof(SphereCollider))
        {
            hitHole.Invoke(HoleId); //can't null check for UnityEvents :(
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log("It's not a sphere dumb ass");
        }

    }
}
