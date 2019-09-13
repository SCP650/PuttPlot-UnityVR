using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    [SerializeField]
    private GameObject golfBallPrefab;

    [SerializeField]
    private int golfBallsLimit;

    private GameObject[] golfBalls;
    private Vector3 ballSpawnPosition;
    private int golfBallIndex;

    void Start()
    {
        golfBalls = new GameObject[golfBallsLimit];
        golfBallIndex = 0;
        ballSpawnPosition = golfBallPrefab.transform.position;

        InstantiateGolfBalls();
        StartCoroutine(spawner());
    }

    IEnumerator spawner()
    {
        while(true)
        {
            yield return new WaitUntil(() =>
            {
                return (Vector3.Distance(golfBalls[golfBallIndex].transform.position, ballSpawnPosition) > .5f);
            });
            yield return new WaitForSeconds(1f);
            SpawnABall();
            yield return null;
            
        }
    }


   
    void SpawnABall()
    {
        golfBallIndex++;
        if (golfBallIndex >= golfBallsLimit)
        {
            golfBallIndex = 0;
        }
        golfBalls[golfBallIndex].GetComponent<Rigidbody>().velocity = Vector3.zero;
        golfBalls[golfBallIndex].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        golfBalls[golfBallIndex].transform.position = ballSpawnPosition;
        golfBalls[golfBallIndex].SetActive(true);
        
 
     
    }

    void InstantiateGolfBalls()
    {
        for(int i = 0; i < golfBallsLimit; i++)
        {
            golfBalls[i] = Instantiate(golfBallPrefab, ballSpawnPosition, Quaternion.identity);
            golfBalls[i].transform.parent = this.gameObject.transform;
            golfBalls[i].SetActive(false);
        }

        golfBalls[0].SetActive(true);
    }
}
