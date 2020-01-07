using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    [SerializeField]
    private GameObject golfBallPrefab;


    [SerializeField] UnitEvent boardIn;

    [SerializeField] UnitEvent increase_dur;

    [SerializeField] IntEvent holechoosen;

    GameObject ball;
    private Vector3 ballSpawnPosition;
    bool respawn_in_5 = false;

    float wait_time = 3.25f;

    void Start()
    {
        ballSpawnPosition = transform.position;

        ball = Instantiate(golfBallPrefab, ballSpawnPosition, Quaternion.identity);
        ball.transform.parent = transform;
        ball.SetActive(false);
        StartCoroutine(spawner());

        holechoosen.AddListener(_ => wait_time = 3.25f);
        increase_dur.AddListener(() => wait_time = 15);
    }

    IEnumerator spawner()
    {
        while(true)
        {
            var routine = StartCoroutine(in5());
            yield return new WaitUntil(() =>
                !ball.active || ball.transform.position.y < -5 || respawn_in_5);
            respawn_in_5 = false;
            StopCoroutine(routine);
            if(!ball.active)
                boardIn.AddListenerOneTime(_ => SpawnABall());
            else
            {
                yield return new WaitForSeconds(.25f);
                SpawnABall();
            }
            yield return null;
            
        }
    }


    IEnumerator in5()
    {
        yield return new WaitUntil(() => ball.GetComponent<Rigidbody>().velocity.magnitude > .1f);
        yield return new WaitForSeconds(1);
        if((ball.transform.position - ballSpawnPosition).magnitude < 2)
            respawn_in_5 = true;
        yield return new WaitForSeconds(wait_time);
        respawn_in_5 = true;
    }
   
    void SpawnABall()
    {
        ball.SetActive(false);
        ball = Instantiate(golfBallPrefab, ballSpawnPosition, Quaternion.identity);
        /*ball.GetComponent<Light>().intensity = 0;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ball.transform.position = ballSpawnPosition;
        ball.SetActive(true);
        ball.GetComponent<TrailRenderer>()?.Clear();*/
        
 
     
    }


}
