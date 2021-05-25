using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public float healthPoints;
    public GameObject bullet;
    public float bulletSpeed;
    public float timer;
    public int shotReload = 2;
    public bool canShoot = true;
    public float movSpeed = 5;
    public GameObject player;
    public Vector3 playerPos;
    public bool wantsToJump;
    private Rigidbody2D rb;
    private GameObject[] platformList;
    public bool hasJumped = false;
    public float jumpCooldown = 3;
    public float jumpTimer;

    public float bossShotRange = 12f;

    private Animator anim;

    public SpriteRenderer sprRen;
    private float originalScale;

    public enum BossState
    {
        Idle,
        Moving,
        Shooting,
        MoveAndShoot
    }

    public BossState currentState = BossState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale.x;
        sprRen = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        healthPoints = 30f;
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        platformList = GameObject.FindGameObjectsWithTag("Platform");

    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >= transform.position.x)
            transform.localScale = new Vector3(originalScale, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-originalScale, transform.localScale.y, transform.localScale.z);

        playerPos = player.transform.position;

        if (playerPos.y > transform.position.y + 1f)
        {
            wantsToJump = true;
        }
        else
            wantsToJump = false;

        DeathAnimation();

        ShootingTimer();

        JumpTimer();

        if (anim.GetBool("jumping"))
        {
            Vector2 vel = rb.velocity;
            vel.y -= 9.8f * Time.deltaTime;
            rb.velocity = vel;
        }


        switch (currentState)
        {
            case BossState.Idle:
                BossIdle();
                break;

            case BossState.Moving:
                BossMoving();
                break;

            case BossState.Shooting:
                BossShooting();
                break;
        }

        

    }

    private void ShootingTimer()
    {
        //Boss Shooting Timer
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

    private void JumpTimer()
    {
        //Boss Jump Timer
        jumpTimer += Time.deltaTime;
        if (jumpTimer >= jumpCooldown)
        {
            hasJumped = false;
        }
        else
            hasJumped = true;
    }

    //Code for Idle
    private void BossIdle()
    {

    }

    //Code for Moving
    private void BossMoving()
    {
        if (player.transform.position.x >= transform.position.x)
        {
            transform.Translate(Vector2.right * movSpeed * Time.deltaTime);
            if (Mathf.Abs(player.transform.position.x - transform.position.x) <= bossShotRange)
                anim.SetBool("isRunning", false);
            else
                anim.SetBool("isRunning", true);
        }
        else if(player.transform.position.x < transform.position.x)
        {
            transform.Translate(Vector2.left * movSpeed * Time.deltaTime);
            if (Mathf.Abs(player.transform.position.x - transform.position.x) <= bossShotRange)
                anim.SetBool("isRunning", false);
            else
                anim.SetBool("isRunning", true);
        }
        else

        if (wantsToJump)
        {
            if (!hasJumped)
                JumpToPlatform();
        }

    }

    //Code for Shooting
    private void BossShooting()
    {
        if(canShoot)
        {
            if (player.transform.position.x >= transform.position.x)
            {
                anim.SetBool("isShooting", true);
                GameObject newBullet = Instantiate(bullet, new Vector2(transform.position.x + 1.5f, transform.position.y +2f), transform.rotation);
                timer = 0;
            }
            else
            {
                anim.SetBool("isShooting", true);
                GameObject newBullet = Instantiate(bullet, new Vector2(transform.position.x - 1.5f, transform.position.y +2f), transform.rotation);
                timer = 0;
            }
        }else
            anim.SetBool("isShooting", false);

        if (wantsToJump)
        {
            if (!hasJumped)
                JumpToPlatform();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            healthPoints -= collision.gameObject.GetComponent<Bullet>().bulletDamage;
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            anim.SetBool("jumping", false);
        }

    }

    void DeathAnimation()
    {
        if (healthPoints <= 0)
        {
            anim.SetBool("shouldDie", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Destroy(GetComponent<CapsuleCollider2D>());
            GetComponent<Boss>().enabled = false;
            StartCoroutine(GoToMenu());
        }
    }

    void JumpToPlatform()
    {
        bool inPosition = false;
        Transform closestPlatform = LookForClosestPlatform(platformList);
        Vector3 moveDirection = (new Vector3(closestPlatform.position.x, 0, 0) - new Vector3(transform.position.x, 0, 0)).normalized;
        transform.position += moveDirection * movSpeed * Time.deltaTime;
        if (closestPlatform.position.x - 1f < transform.position.x && transform.position.x < closestPlatform.position.x + 1f)
        {
            inPosition = true;
        }
        else
            inPosition = false;

        if(inPosition)
        {
            rb.AddForce(Vector2.up * 900);
            anim.SetBool("jumping", true);
            jumpTimer = 0;
            hasJumped = true;
        }
    }

    Transform LookForClosestPlatform(GameObject[] platforms)
    {
        Transform closest = null;
        float closestSqrDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in platforms)
        {
            Vector3 direction = t.transform.position - currentPos;
            float sqrDirection = direction.sqrMagnitude;
            if (sqrDirection < closestSqrDist)
            {
                closestSqrDist = sqrDirection;
                closest = t.transform;
            }
        }
        return closest;
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        //player enters boss range
        if (collision.gameObject.CompareTag("Player"))
        {
            currentState = BossState.Moving;

            if (Mathf.Abs(collision.gameObject.transform.position.x - transform.position.x) <= bossShotRange)
            {
                currentState = BossState.Shooting;
            }
            else
                currentState = BossState.Moving;
        }
    }

    IEnumerator GoToMenu()
    {
        if(healthPoints <= 0)
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("MainMenu");
        }
    }

}

