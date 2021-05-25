using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    #region Enemy Variables

    public float hitPoints;
    public float speed;
    public bool facingRight;
    private Rigidbody2D rb;

    public Rigidbody2D Rb
    {
        get { return rb; }
    }

    private Animator anim;
    public Animator Anim
    {
        get { return anim; }
    }

    #endregion

    public bool isPlayerHidden;

    private EnemyBaseState currentState;
    public EnemyBaseState CurrentState
    {
        get { return currentState; }
    }

    public readonly EnemyIdleState IdleState = new EnemyIdleState();
    public readonly EnemyMovingState MovingState = new EnemyMovingState();
    public readonly EnemyDeathState DeathState = new EnemyDeathState();

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //TransitionToState(IdleState);
        TransitionToState(MovingState);
    }

    void Update()
    {
        currentState.Update(this);
    }

    public void TransitionToState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitPoints -= collision.gameObject.GetComponent<Bullet>().bulletDamage;
        }
    }
}
