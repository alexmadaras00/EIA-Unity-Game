using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float bulletSpeed;
    private GameObject player;
    private Vector2 direction;
    public bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("JoaoPlayer");
        direction = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) - 
                    new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z);
        
        if(PlayerPrefs.GetInt("sound") == 1)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(gameObject);
    }

}
