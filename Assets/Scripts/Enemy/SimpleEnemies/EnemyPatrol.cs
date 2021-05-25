using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private CapsuleCollider2D coll;
    private Animator anim;
    public float hitPoints;
    private Rigidbody2D rb;
    public List<float> positions;
    public float searchingTime = 3f;
    public float speed;
    public bool inLoop = false;
    public bool goingLeft;
    public bool isLooking = false;
    private int direction;
    private SpriteRenderer sr;
    private bool isDead = false;

    private 

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        hitPoints = 1f;
        goingLeft = true;
        direction = Random.Range(0, 10);
        rb = GetComponent<Rigidbody2D>();

        positions = new List<float>();
        AddPositions();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DeathAnimation();
        if(!isDead)
            GoToPositions();

        if (rb.velocity.x == 0)
        {
            anim.SetFloat("VelocityX", -1);
        }
        else
        {
            anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        }
    }
    private void AddPositions()
    {
        positions.Add(transform.position.x - 2);
        positions.Add(transform.position.x + 2);
    }
    private void GoToPositions()
    {
        if (transform.position.x != positions[0] && transform.position.x != positions[1] && !inLoop)
        {
            if (direction > 5)
                goingLeft = true;
            else
                goingLeft = false;
        }

        if (transform.position.x <= positions[0] && goingLeft)
        {
            inLoop = true;
            isLooking = true;
            searchingTime -= Time.deltaTime;
            if (searchingTime <= 0)
            {
                isLooking = false;
                goingLeft = false;
                searchingTime = 3f;
            }
        }
        if (transform.position.x >= positions[1] && !goingLeft)
        {
            inLoop = true;
            isLooking = true;
            searchingTime -= Time.deltaTime;
            if (searchingTime <= 0)
            {
                isLooking = false;
                goingLeft = true;
                searchingTime = 3f;
            }
        }

        if (!goingLeft)
        {
            if (!isLooking)
            {
                transform.localScale = new Vector3(-10, 10f, 1);
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
                rb.velocity = Vector2.zero;

        }
        else
        {
            if (!isLooking)
            {
                transform.localScale = new Vector3(10, 10f, 1);
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
                rb.velocity = Vector2.zero;

        }
    }

    void DeathAnimation()
    {
        if (hitPoints <= 0)
        {
            isDead = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Destroy(coll);
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().seen)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().seen = false;
            }
            anim.SetBool("isDead", true) ;
            gameObject.transform.Find("DetectionRadius").GetComponent<EdgeCollider2D>().enabled = false; 
            Destroy(gameObject,1.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitPoints -= collision.gameObject.GetComponent<Bullet>().bulletDamage;
        }
    }
}