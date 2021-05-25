using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private float timer = 2f;

    public override void EnterState(Enemy enemy)
    {
        //Debug.Log(enemy.CurrentState);
        enemy.Anim.SetBool("isMoving", false);
        enemy.Rb.velocity = Vector2.zero;
        timer = 2f;
    }

    public override void Update(Enemy enemy)
    {
        enemy.isPlayerHidden = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isHidden;
        if(enemy.isPlayerHidden)
            Physics2D.IgnoreCollision(enemy.gameObject.GetComponent<CapsuleCollider2D>(), GameObject.FindWithTag("Player").GetComponent<CapsuleCollider2D>(), true);
        else
            Physics2D.IgnoreCollision(enemy.gameObject.GetComponent<CapsuleCollider2D>(), GameObject.FindWithTag("Player").GetComponent<CapsuleCollider2D>(), false);

        if (enemy.hitPoints <= 0)
        {
            enemy.Anim.SetBool("isMoving", false);
            enemy.Anim.SetBool("shouldDie", true);
            enemy.TransitionToState(enemy.DeathState);
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (enemy.facingRight)
            {
                enemy.facingRight = false;
                enemy.TransitionToState(enemy.MovingState);
            }
            else
            {
                enemy.facingRight = true;
                enemy.TransitionToState(enemy.MovingState);
            }
        }

        
    }

}
