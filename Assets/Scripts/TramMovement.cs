using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TramMovement : MonoBehaviour
{
    public float moveSpeed;
    public Vector3[] tramPoints;
    public int pointsIndex;

    // Update is called once per frame
    void Update()
    {
      
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, tramPoints[pointsIndex], moveSpeed*Time.deltaTime);
        
        if (transform.localPosition == tramPoints[pointsIndex])
        {
            //Next point of the array of Locations
            pointsIndex++;
        }

        if (pointsIndex == (tramPoints.Length))
        {
            //Going Back to the start point
            pointsIndex = 0;
        }
    }
}
