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
        float dur = 0f;
        while(true)
        {
            yield return null;
            dur = 1f;
            while(isEmpty() && dur >= 0)
            {
                yield return null;
                dur -= Time.deltaTime;
            }
            if(isEmpty())
            {
                SpawnABall();
            }
        }
    }

    bool isEmpty()
    {
        RaycastHit hit;
        return !Physics.SphereCast(Vector3.zero, 0.1f, Vector3.zero, out hit, 0);
    }
   
    void SpawnABall()
    {
        golfBalls[golfBallIndex].GetComponent<Rigidbody>().velocity = Vector3.zero;
        golfBalls[golfBallIndex].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        golfBalls[golfBallIndex].transform.position = ballSpawnPosition;
        golfBalls[golfBallIndex].SetActive(true);
        golfBallIndex++;
        if(golfBallIndex >= golfBallsLimit)
        {
            golfBallIndex = 0;
        }
     
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
        golfBallIndex++;
    }
}
