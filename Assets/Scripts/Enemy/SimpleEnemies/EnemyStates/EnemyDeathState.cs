using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.Rb.velocity = Vector2.zero;
        enemy.transform.Find("Flashlight").GetComponent<SpriteRenderer>().enabled = false;
        //Debug.Log(enemy.CurrentState);
    }

    public override void Update(Enemy enemy)
    {      
        Physics2D.IgnoreCollision(enemy.gameObject.GetComponent<CapsuleCollider2D>(), GameObject.FindWithTag("Player").GetComponent<CapsuleCollider2D>(), true);
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().seen)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().seen = false;
        }
        GameObject.Destroy(enemy.gameObject, 1.5f);
    }
}
