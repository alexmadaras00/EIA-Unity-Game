using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveCamera : MonoBehaviour
{
    private Transform player;
    public float speed = 2f;
    public float xCamMaxDistance = 4f;
    public float yCamMaxDistance = 3f;
    public bool isLooking;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void LateUpdate()
    {
        isLooking = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().isLooking;

        if (isLooking)
            LookingCameraMovement();
        else
        {
            if (transform.position != player.position)
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.position.y, transform.position.z), 0.1f);
            else
                transform.position = player.position;
        }
        
    }

    private void LookingCameraMovement()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float xLimit = mousePos.x - transform.position.x;
        float xDiff = 0;
        float yLimit = mousePos.y - transform.position.y;
        float yDiff = 0;
        if (xLimit > 0)
        {
            xDiff = xCamMaxDistance;
        }
        else
        {
            xDiff = -xCamMaxDistance;
        }
        if (yLimit > 0)
        {
            yDiff = yCamMaxDistance;
        }
        else
        {
            yDiff = -yCamMaxDistance;
        }

        //If the player and the camera are too far apart
        if (Vector2.Distance(new Vector2(player.transform.position.x, 0), new Vector2(transform.position.x, 0)) > 13)
        {
            //If the camera is right side of the camera
            if ((transform.position.x > player.transform.position.x) && (Mathf.Abs(xLimit) < xCamMaxDistance))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x + 13,
                                     transform.position.y, transform.position.z), speed * Time.deltaTime);
            }
            //If the camera is left side of the camera
            else if ((transform.position.x < player.transform.position.x) && (Mathf.Abs(xLimit) < xCamMaxDistance))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x - 13,
                                     transform.position.y, transform.position.z), speed * Time.deltaTime);
            }

        }
        //If the camera is not too far away from the player
        else
        {
            if (Mathf.Abs(xLimit) >= xCamMaxDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(mousePos.x + xDiff,
                                     transform.position.y, transform.position.z), speed * Time.deltaTime);
            }
        }

        //If the player and the camera are too far apart
        if (Vector2.Distance(new Vector2(0, player.transform.position.y), new Vector2(0, transform.position.y)) > 6)
        {
            //If the camera is above the camera
            if ((transform.position.y > player.transform.position.y) && (Mathf.Abs(yLimit) < yCamMaxDistance))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x,
                                     player.transform.position.y + 6, transform.position.z), speed * Time.deltaTime);
            }
            //If the camera is below the camera
            else if ((transform.position.y < player.transform.position.y) && (Mathf.Abs(yLimit) < yCamMaxDistance))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x,
                                     player.transform.position.y - 6, transform.position.z), speed * Time.deltaTime);
            }
        }
        //If the camera is not too far away from the player

        else
        {
            if (Mathf.Abs(yLimit) >= yCamMaxDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x,
                                     mousePos.y + yDiff, transform.position.z), speed * Time.deltaTime);
            }

        }
    }
}
