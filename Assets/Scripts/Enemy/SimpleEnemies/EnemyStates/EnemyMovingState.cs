using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        //Debug.Log(enemy.CurrentState);
        enemy.Anim.SetBool("isMoving", true);
        if (!enemy.facingRight && enemy.transform.localScale.x < 0)
        {
            enemy.transform.localScale = new Vector2(-enemy.transform.localScale.x, enemy.transform.localScale.y);
        }
        else if (enemy.facingRight && enemy.transform.localScale.x > 0)
        {
            enemy.transform.localScale = new Vector2(-enemy.transform.localScale.x, enemy.transform.localScale.y);
        }
    }

    public override void Update(Enemy enemy)
    {
        enemy.isPlayerHidden = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isHidden;
        if (enemy.isPlayerHidden)
            Physics2D.IgnoreCollision(enemy.gameObject.GetComponent<CapsuleCollider2D>(), GameObject.FindWithTag("Player").GetComponent<CapsuleCollider2D>(), true);
        else
            Physics2D.IgnoreCollision(enemy.gameObject.GetComponent<CapsuleCollider2D>(), GameObject.FindWithTag("Player").GetComponent<CapsuleCollider2D>(), false);

        if (enemy.hitPoints <= 0)
        {
            enemy.Anim.SetBool("isMoving", false);
            enemy.Anim.SetBool("shouldDie", true);
            enemy.TransitionToState(enemy.DeathState);
        }

        
        //If enemy is going left
        if (!enemy.facingRight)
        {
            //Raycast at the left side of the enemy, going down
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(enemy.transform.position.x - 2f, enemy.transform.position.y), Vector2.down);
            //Raycast at the left side of the enemy, going left
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(enemy.transform.position.x - .5f, enemy.transform.position.y + .5f), Vector2.left);

            //If it hits something
            if (hit.collider != null)
            {
                float dist = Mathf.Abs(hit.point.y - enemy.transform.position.y);
                //If the platform at the left side of the player is ending
                if (dist > .2f)
                {
                    enemy.Rb.velocity = Vector2.zero;
                    enemy.TransitionToState(enemy.IdleState);
                }
                //If the platform at the left side of the player continues
                else
                {
                    enemy.Rb.velocity = new Vector2(-enemy.speed, enemy.Rb.velocity.y);
                }
            }

            //If it hits something
            if (hit2.collider != null)
            {
                float dist = Mathf.Abs(hit2.point.x - enemy.transform.position.x - .5f);

                if(!hit2.collider.CompareTag("Player"))
                {
                    if (dist < 3f)
                    {
                        enemy.Rb.velocity = Vector2.zero;
                        enemy.TransitionToState(enemy.IdleState);
                    }
                    else
                    {
                        enemy.Rb.velocity = new Vector2(-enemy.speed, enemy.Rb.velocity.y);
                    }
                }

                else
                {
                    //if (dist < minDistToSeePlayer)
                        //Should stop moving left (enter alert state)
                }
            }
        }

        //If enemy is going right
        else
        {
            //Raycast at the right side of the enemy, going down
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(enemy.transform.position.x + 2f, enemy.transform.position.y), Vector2.down);
            //Raycast at the right side of the enemy, going right
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(enemy.transform.position.x + 1f, enemy.transform.position.y + .5f), Vector2.right);

            //If it hits something
            if (hit.collider != null)
            {
                float dist = Mathf.Abs(hit.point.y - enemy.transform.position.y);

                //If the platform at the right side of the player is ending
                if (dist > .2f)
                {
                    enemy.Rb.velocity = Vector2.zero;
                    enemy.TransitionToState(enemy.IdleState);
                }
                else
                {
                    enemy.Rb.velocity = new Vector2(enemy.speed, enemy.Rb.velocity.y);
                }
            }

            //If it hits something
            if (hit2.collider != null)
            {
                float dist = Mathf.Abs(hit2.point.x - enemy.transform.position.x + .5f);
                if (!hit2.collider.CompareTag("Player"))
                {
                    if (dist < 3f)
                    {
                        enemy.Rb.velocity = Vector2.zero;
                        enemy.TransitionToState(enemy.IdleState);
                    }
                    else
                    {
                        enemy.Rb.velocity = new Vector2(enemy.speed, enemy.Rb.velocity.y);
                    }
                }
                else
                {
                    //if (dist < minDistToSeePlayer)
                    //Should stop moving left (enter alert state)
                }
            }
        }        
    }
}
