using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool doorShouldClose;
    private bool doorIsClosed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doorShouldClose && !doorIsClosed)
        {
            CloseDoor();
        }

        if (!doorShouldClose && doorIsClosed)
            OpenDoor();

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerMovement>().isHidden)
            { 
                doorShouldClose = true; 
            }
            else
            {
                doorShouldClose = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            doorShouldClose = false;
        }
    }

    private void CloseDoor()
    {
            transform.Translate(Vector2.left * 10);
            doorIsClosed = true;
    }

    private void OpenDoor()
    {
            transform.Translate(Vector2.right * 10);
            doorIsClosed = false;
    }
}
