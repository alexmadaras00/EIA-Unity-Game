using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    
    public float bulletSpeed;
    private GameObject player;
    private Vector2 playerPos;
    public bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        if (playerPos.x > transform.position.x)
            isRight = true;
        else 
            isRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRight)
            transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
        else
            transform.Translate(Vector3.left * bulletSpeed * Time.deltaTime);

    }
}
