using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed;
    Vector3 targetpos;

    public GameObject ways;
    public Transform[] waypoints;
    int pointIndex;
    int pointCount;
    int direction = 1;

    public float waitduration;
    int speedmultiplier = 1;

    private void Awake()
    {
        waypoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.gameObject.transform.childCount; i++) 
        {
            waypoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        pointCount = waypoints.Length;
        pointIndex = 1;
        targetpos = waypoints[pointIndex].transform.position;
    }

    private void Update()
    {
        var step = speedmultiplier * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetpos, step);

        if (transform.position == targetpos) 
        {
            NextPoint();
        }
    }

    void NextPoint()
    {
        if (pointIndex == pointCount - 1)
        {
            direction = -1;
        }

        if (pointIndex == 0)
        {
            direction = 1;
        }

        pointIndex += direction;
        targetpos = waypoints[pointIndex].transform.position;
        StartCoroutine(waitnextpoint());
    }

    IEnumerator waitnextpoint() 
    {
        speedmultiplier = 0;
        yield return new WaitForSeconds(waitduration);
        speedmultiplier = 1;
    }
}


