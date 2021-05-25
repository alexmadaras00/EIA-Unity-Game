using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletSpeed;
    private GameObject player;
    private int lastDirection;
    //private Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        lastDirection = player.GetComponent<PlayerMovement>().lastDirection;
        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (lastDirection == 0)
        {
            transform.Translate(Vector3.left * bulletSpeed * Time.deltaTime);
        }
        if (lastDirection == 1)
        {
            transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
        }
    }
}
