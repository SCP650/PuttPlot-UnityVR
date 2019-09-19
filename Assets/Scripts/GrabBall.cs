using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBall : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 0.2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().GetType() == typeof(SphereCollider))
        {
            Vector3 targetPosition = transform.parent.position;

            StartCoroutine(MoveBall(targetPosition, other));
        }
    }

    IEnumerator MoveBall(Vector3 target, Collider ball)
    {
        while (ball.transform.position != target)
        {
            ball.transform.position = Vector3.MoveTowards(ball.transform.position, target, maxSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
