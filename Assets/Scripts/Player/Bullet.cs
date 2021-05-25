using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    private GameObject player;
    public float bulletDamage;
    private Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        bulletDamage = player.GetComponent<Gun>().selectedWeapon.damage;
        bulletSpeed = player.GetComponent<Gun>().selectedWeapon.bulletSpeed;
        //Direction where the shot is going 
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }

    // Update is called once per frame
    void Update()        
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(mousePos.x, mousePos.y).normalized * bulletSpeed;
        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
        else
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(), collision.gameObject.GetComponent<CapsuleCollider2D>());
        }
    }
}
