using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexPlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb = new Rigidbody2D();
    public GameObject bullet;
    //Player Stats
    public float hitPoints;
    private float speed;
    public float jumpStrength;
    public bool isJumping = false;
    public float bulletSpeed;
    public int coinsCollected;
    public bool isColliding = false;
    public bool canDoubleJump = false;
    public float verticalMaxVelocity = 150f;

    


    //Checks if the player is turning left or right before shooting to determine bullet direction
    public enum LastDirection
    {
        Left,
        Right
    }
    public int lastDirection;

    
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        rb = GetComponent<Rigidbody2D>();
        hitPoints = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
        CheckVerticalMaxVelocity();
    }

    private void Movement()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (Input.GetKey(KeyCode.D))
        {
            if(!isColliding || lastDirection == (int)LastDirection.Left)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                lastDirection = (int)LastDirection.Right;
                if(gameObject.GetComponent<Rigidbody2D>().velocity.x != 0)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    
                }
            }

        }

        if (Input.GetKey(KeyCode.A))
        {
            if (!isColliding || lastDirection == (int)LastDirection.Right)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                lastDirection = (int)LastDirection.Left;
                if (rb.velocity.x != 0)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                
            }
        }
       

        if (Input.GetKeyDown(KeyCode.Space) && (!isJumping || canDoubleJump))
        {
            if(!isJumping)
            {
                rb.AddForce(Vector2.up * jumpStrength);
                isJumping = true;
            }
            
            if(isJumping && canDoubleJump)
            {
                if(lastDirection == (int)LastDirection.Left)
                {
                    rb.AddForce(new Vector2(300, jumpStrength));
                    Debug.Log("Wall Jumped Right");
                    canDoubleJump = false;
                }
                else if (lastDirection == (int)LastDirection.Right)
                {
                    rb.AddForce(new Vector2(-300, jumpStrength));
                    Debug.Log("Wall Jumped Left");
                    canDoubleJump = false;
                }

            }
            
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += 3f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed -= 3f;
        }
    }
    void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (lastDirection == (int)LastDirection.Left)
            {
                Instantiate(bullet, new Vector2(transform.position.x - 1f, transform.position.y), transform.rotation);
            }
            if (lastDirection == (int)LastDirection.Right)
            {
                Instantiate(bullet, new Vector2(transform.position.x + 1f, transform.position.y), transform.rotation);
            }
        }
    }

    private void CheckVerticalMaxVelocity()
    {
        if (Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) > verticalMaxVelocity)
        {
            if(gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                rb.velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -verticalMaxVelocity);
            }

            if (gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
            {
                rb.velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, verticalMaxVelocity);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Floor")
        {
            isJumping = false;
        }

        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            hitPoints -= 2f;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            coinsCollected += 1;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && !isColliding)
        {
            isColliding = true;
            canDoubleJump = true;
            Debug.Log("Is Colliding");
            gameObject.GetComponent<Rigidbody2D>().drag = 5f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && isColliding)
        {
            isColliding = false;
            canDoubleJump = false;
            Debug.Log("Is Not Colliding");
            gameObject.GetComponent<Rigidbody2D>().drag = 0.05f;
        }
    }
}
