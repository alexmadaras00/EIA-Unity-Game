using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchRange : MonoBehaviour
{
    private new CapsuleCollider2D collider;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CapsuleCollider2D>();
        player = GameObject.FindWithTag("Player");
        Physics2D.IgnoreCollision(collider, player.GetComponent<CapsuleCollider2D>(), true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(collision.name);
            if(player.GetComponent<Animator>().GetBool("isPunching"))
                if(collision.gameObject.GetComponent<Enemy>().hitPoints >= 1)
                    collision.gameObject.GetComponent<Enemy>().hitPoints -= 1;
        }
    
    }
}
