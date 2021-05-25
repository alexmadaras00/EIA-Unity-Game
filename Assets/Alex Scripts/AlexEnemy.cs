using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexEnemy : MonoBehaviour
{
    public float healthPoints;
    public GameObject bullet;
    public float bulletSpeed;
    public float timer;
    public int shotReload = 2;
    public bool canShoot = true;
    public float movSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        healthPoints = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        DeathAnimation();
        Shooting();
        timer += Time.deltaTime;
        if (timer >= shotReload)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            //Debug.Log("Shot");
            healthPoints -= 2;
            Destroy(collision.gameObject);
        }
    }
    void DeathAnimation()
    {
        if(healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Shooting()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.transform.position.x >= transform.position.x)
            {
                
                if(Mathf.Abs(collision.gameObject.transform.position.x - transform.position.x) <= 5)
                {
                    
                    if(canShoot)
                    {
                        GameObject newBullet = Instantiate(bullet, new Vector2(transform.position.x + 0.5f, transform.position.y), transform.rotation);
                        newBullet.transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
                        timer = 0;
                    }
                }
                else
                {
                    transform.Translate(Vector2.right * movSpeed * Time.deltaTime);
                }
            }
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                if (Mathf.Abs(collision.gameObject.transform.position.x - transform.position.x) <= 5)
                {
                    if (canShoot)
                    {
                        GameObject newBullet = Instantiate(bullet, new Vector2(transform.position.x - 0.5f, transform.position.y), transform.rotation);
                        newBullet.transform.Translate(Vector3.left * bulletSpeed * Time.deltaTime);
                        timer = 0;
                    }
                }
                else
                {
                    transform.Translate(Vector2.left * movSpeed* Time.deltaTime);
                }
            }
        }
    }
}

