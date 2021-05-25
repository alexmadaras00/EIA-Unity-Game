using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    #region Player Stats and Variables
    public float hitPoints;
    public float speed = 25;
    public float jumpStrength = 500f;
    public float verticalMaxVelocity = 5f;

    public int coinsCollected;
    public int codesCollected;

    private Rigidbody2D rb;
    private CapsuleCollider2D capsulCollider2d;
    public bool isCollidingWithWall = false;
    public bool isCollidingWithGround = true;

    public float seenTimer;
    public float maxSeenTimer = 2f;
    public bool seen = false;
    private bool canHide = false;
    public bool isHidden = false;
    public bool isLooking = false;
    public bool canDoubleJump = false;
    
    private Animator anim;
    public Transform bulletSpawn;

    public SpriteRenderer sprRen;
    //private float originalScale;
    #endregion


    #region Canvas Things
    private GameObject menuCanvas;
    private Image screenFlash;
    private float transparencyValue = 0f;
    public bool transparencyValueShouldGoUp = true;
    public Image innerEye;
    public Text hpText;

    #endregion

    public AudioSource collectAudio;

    public bool gameOver = false;

    public GameObject punchRange;

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
        //bulletSpawn = transform.Find("bulletSpawn");
        hpText = GameObject.Find("HitPoints").GetComponent<Text>();
        innerEye = GameObject.Find("InnerEye").GetComponent<Image>();
        menuCanvas = GameObject.Find("MenuBackground");
        rb = GetComponent<Rigidbody2D>();
        hitPoints = 20f;
        sprRen = gameObject.GetComponent<SpriteRenderer>();
        //originalScale = transform.localScale.x;
        anim = GetComponent<Animator>();
        capsulCollider2d = GetComponent<CapsuleCollider2D>();

        screenFlash = GameObject.Find("RedFlashingScreen").GetComponent<Image>();

        coinsCollected = PlayerPrefs.GetInt("money");

    }

    // Update is called once per frame
    void Update()
    {
        Cheats();
        CheckIfLooking();

        hpText.text = hitPoints.ToString() + " / 20";

        if(canHide && Input.GetKeyDown(KeyCode.W))
        {
            if (isHidden)
                isHidden = false;
            else
                isHidden = true;
        }

        if (isHidden)
        {
            sprRen.color = new Color(sprRen.color.r, sprRen.color.g, sprRen.color.b, .5f);
            sprRen.sortingOrder = 14;
            seen = false;
        }
        else
        {
            sprRen.color = new Color(sprRen.color.r, sprRen.color.g, sprRen.color.b, 1);
            sprRen.sortingOrder = 32767;

        }

        if (anim.GetBool("isPunching"))
            punchRange.SetActive(true);
        else
            punchRange.SetActive(false);

        if (!isLooking)
            Movement();
        CheckVerticalMaxVelocity();

        
        if (anim.GetBool("isCrouched") && anim.GetBool("onGround") && !anim.GetBool("isShooting") && !anim.GetBool("isPunching"))
        {
            capsulCollider2d.direction = CapsuleDirection2D.Horizontal;
            capsulCollider2d.size = new Vector2(0.2989759f, 0.1847509f);
            capsulCollider2d.offset = new Vector2(0.01029191f, 0.095f);
            
        }
        else
        {
            capsulCollider2d.direction = CapsuleDirection2D.Vertical;
            capsulCollider2d.size = new Vector2(0.1f, 0.3f);
            capsulCollider2d.offset = new Vector2(0.0f, 0.15f);
            
        }

        if (!seen && seenTimer > 0)
            seenTimer -= Time.deltaTime;
        if (seen && seenTimer < maxSeenTimer)
            seenTimer += Time.deltaTime;

        if (seenTimer > maxSeenTimer)
            seenTimer = maxSeenTimer;
        if (seenTimer < 0)
            seenTimer = 0;

        if(seenTimer == maxSeenTimer)
        {
            gameOver = true;
        }

        

        if(innerEye)
        {
            innerEye.color = new Color(1, 1 - seenTimer / maxSeenTimer, 1 - seenTimer / maxSeenTimer);
            screenFlash.color = new Color(screenFlash.color.r, screenFlash.color.g, screenFlash.color.b, transparencyValue);
            
            if (seenTimer / maxSeenTimer >= 0.5f)
            {
                if (transparencyValueShouldGoUp)
                    transparencyValue += Time.deltaTime * 0.5f;
                else
                    transparencyValue -= Time.deltaTime * 0.5f;
            }
            else
                if (transparencyValue > 0)
                transparencyValue -= Time.deltaTime * 0.5f;


            if (transparencyValue >= 0.3f)
            {
                transparencyValueShouldGoUp = false;
                transparencyValue = 0.29f;
            }
            else if (transparencyValue <= 0)
                transparencyValueShouldGoUp = true;
        }

        if (hitPoints <= 0)
        {
            gameOver = true;
        }
        
    }

    

    private void Movement()
    {

        //Getting mouse position in world units
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //if(menuCanvas)
        {
            if (mousePos.x < transform.position.x) //&& !menuCanvas.activeSelf)
            {
                //transform.localScale = new Vector3(-10f, transform.localScale.y, transform.localScale.z);
                sprRen.flipX = true;
                bulletSpawn.position = new Vector2(transform.position.x - .01f, bulletSpawn.position.y);
            }
            if (mousePos.x > transform.position.x)// && !menuCanvas.activeSelf)
            {
                //transform.localScale = new Vector3(10, transform.localScale.y, transform.localScale.z);
                sprRen.flipX = false;
                bulletSpawn.position = new Vector2(transform.position.x + .01f, bulletSpawn.position.y);
            }
        }

        if (rb.velocity.x == 0)
        {
            anim.SetFloat("VelocityX", -1);
        }
        else
        {
            anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        }
        anim.SetFloat("VelocityY", rb.velocity.y);


        //Moving Right
        if (!gameOver)
        { 
           
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1f), Vector2.right);
            float dist = Mathf.Abs(hit.point.x - (transform.position.x));
            if (Input.GetKey(KeyCode.D))
            {
                if(!anim.GetBool("onRope"))
                {
                    if (dist <= .6f)
                    {
                        if (hit.collider.gameObject.CompareTag("Wall") || hit.collider.gameObject.CompareTag("Platform"))
                        {
                            Debug.Log("Cant move");
                            rb.velocity = new Vector2(0, rb.velocity.y);
                            isCollidingWithWall = true;
                        }
                    }
                    else
                    {
                        rb.velocity = new Vector2(speed, rb.velocity.y);
                        isCollidingWithWall = false;
                        lastDirection = (int)LastDirection.Right;
                    }
                }
                else
                    transform.Translate(Vector2.right * Time.deltaTime * 5);
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        

        //Moving Left
        if (!gameOver)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1f), Vector2.left);
            float dist = Mathf.Abs(hit.point.x - (transform.position.x));
            if (Input.GetKey(KeyCode.A))
            {
                if(!anim.GetBool("onRope"))
                {
                    if (dist <= .6f)
                    {
                        if (hit.collider.gameObject.CompareTag("Wall") || hit.collider.gameObject.CompareTag("Platform"))
                        {
                            rb.velocity = new Vector2(0, rb.velocity.y);
                            isCollidingWithWall = true;
                        }
                    }
                    else
                    {
                        rb.velocity = new Vector2(-speed, rb.velocity.y);
                        isCollidingWithWall = false;
                        lastDirection = (int)LastDirection.Left;
                    }
                }
                else
                    transform.Translate(Vector2.left * Time.deltaTime * 5);
            }
            if(Input.GetKeyUp(KeyCode.A))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        

        //Jumping
        if (!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Check if player is close to the ground
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - .001f), Vector2.down);
                if (hit.collider != null)
                {
                    float dist = Mathf.Abs(hit.point.y - (transform.position.y - .001f));
                    if (dist <= .1f && hit.collider.gameObject.CompareTag("Platform"))
                    {
                        rb.AddForce(Vector2.up * jumpStrength);
                        canDoubleJump = true;
                    }
                }

                if (canDoubleJump && isCollidingWithWall)
                {
                    if (lastDirection == (int)LastDirection.Left)
                    {
                        Debug.Log("Double Jumped");
                        rb.AddForce(new Vector2(200, jumpStrength));
                    }
                    else if (lastDirection == (int)LastDirection.Right)
                    {
                        Debug.Log("Double Jumped");
                        rb.AddForce(new Vector2(-200, jumpStrength));
                    }
                    canDoubleJump = false;
                } 
            }
        }


        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - .001f), Vector2.down);
            if (hit.collider != null)
            {
                float dist = Mathf.Abs(hit.point.y - (transform.position.y - .001f));
                if (dist <= .01f && hit.collider.gameObject.CompareTag("Platform"))
                {
                    anim.SetBool("isSprinting", true);
                    speed = 7;
                }else
                {
                    anim.SetBool("isSprinting", false);
                    speed = 5;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("isSprinting", false);
            speed = 5;
        }

        //Crouching
        if (Input.GetKeyDown(KeyCode.S) && !anim.GetBool("onRope"))
        {
            anim.SetBool("isCrouched", true);
        }
        if (Input.GetKeyUp(KeyCode.S) && !anim.GetBool("onRope"))
        {
            anim.SetBool("isCrouched", false);
        }


        if (Input.GetKey(KeyCode.W) && (anim.GetBool("onRope") && (anim.GetBool("isClimbing"))))
        {
            transform.Translate(Vector2.up * Time.deltaTime * 5);
        }
        if (Input.GetKey(KeyCode.S) && (anim.GetBool("onRope") &&  (anim.GetBool("isClimbing"))))
        {
            transform.Translate(Vector2.down * Time.deltaTime * 5);
        }

    }

    private void CheckVerticalMaxVelocity()
    {
        if (Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) > verticalMaxVelocity)
        {
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                rb.velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -verticalMaxVelocity);
            }

            if (gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
            {
                rb.velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, verticalMaxVelocity);
            }
        }
    }

    private void CheckIfLooking()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isLooking)
                isLooking = false;
            else
                isLooking = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            hitPoints -= 2f;
            Destroy(collision.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        /*if (anim.GetBool("isPunching"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
                collision.gameObject.GetComponent<Enemy>().hitPoints -= 1;  
        }*/

        if (collision.gameObject.CompareTag("Wall"))
        {
            isCollidingWithWall = true;
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            isCollidingWithGround = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isCollidingWithWall = false;
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            isCollidingWithGround = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            if(PlayerPrefs.GetInt("music") == 1)
            {
                collectAudio.Play();
            }
            coinsCollected += 1;
        }

        if (collision.gameObject.CompareTag("Code"))
        {
            Destroy(collision.gameObject);
            if (PlayerPrefs.GetInt("music") == 1)
            {
                collectAudio.Play();
            }
            codesCollected += 1;
        }

        if (collision.gameObject.name == "FallToDeathTrigger")
        {
            anim.SetBool("shouldDie", true);
            gameOver = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rope"))
        {
            anim.SetBool("onRope", true);
            Physics2D.gravity = Vector2.zero;
            rb.velocity = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isClimbing", true);               
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isClimbing", true);             
            }
            
            if (!Input.anyKey)
            {
                anim.SetBool("isClimbing", false);
            }
        }
        
        if (collision.gameObject.CompareTag("Door"))
        {
            canHide = true;
        }

        if (collision.gameObject.CompareTag("BigDoor"))
        {
            if (Input.GetKeyDown(KeyCode.W) && codesCollected >= 4)
            {
                PlayerPrefs.SetInt("money", GetComponent<PlayerMovement>().coinsCollected);
                PlayerPrefs.SetInt("pistolLevel", GetComponent<Gun>().pistol.upgradeLevel);
                PlayerPrefs.SetInt("sMLevel", GetComponent<Gun>().smg.upgradeLevel);
                PlayerPrefs.SetInt("aRLevel", GetComponent<Gun>().ar.upgradeLevel);
                PlayerPrefs.SetInt("sniperLevel", GetComponent<Gun>().sniper.upgradeLevel);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Store");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rope"))
        {
            Physics2D.gravity = new Vector2(0, -19.62f);
            anim.SetBool("onRope", false);
        }

        if (collision.gameObject.CompareTag("Door"))
        {
            canHide = false;
            isHidden = false;
        }
    }

    private void Cheats()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            coinsCollected += 10;
        }
    }
}

