using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float waitDuration;

    private int currentPoint = 0;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(MoveToPoint(points[currentPoint].position));
    }

    // move platform to set point
    private IEnumerator MoveToPoint(Vector3 point)
    {
        while (Vector3.Distance(transform.position, point) > 1f) // while not reached point
        {
            transform.position = Vector3.Lerp(transform.position, point, 1f * Time.deltaTime); // move towards point

            yield return null;
        }

        yield return new WaitForSeconds(waitDuration); // wait at destination

        if ((currentPoint + 1) >= points.Length) // if reached last point
            currentPoint = 0; // reset to first point
        else
            currentPoint++; // set next point

        StartCoroutine(MoveToPoint(points[currentPoint].position)); // move to next point
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player")) // if colliding with the player
            collision.transform.parent = transform; // make the player a child of the platform
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player")) // if colliding with the player
            collision.transform.parent = null; // disconnect the player from the platform
    }
}
