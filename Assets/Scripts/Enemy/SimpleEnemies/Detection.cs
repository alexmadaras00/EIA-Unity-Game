using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detection : MonoBehaviour
{
    private GameObject player;
    public CapsuleCollider2D cc;
    private new EdgeCollider2D collider;
    public SpriteRenderer flashlight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        collider = GetComponent<EdgeCollider2D>();
        Physics2D.IgnoreCollision(collider, player.transform.Find("punchRange").GetComponent<CapsuleCollider2D>(), true);
    }

    // Update is called once per frame
    void LateUpdate()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            RaycastHit2D ray = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), 
                               new Vector2(player.transform.position.x, player.transform.position.y + 2) - 
                               new Vector2(transform.position.x, transform.position.y));
            if(ray)
            {
                if (ray.collider.CompareTag("Player"))
                {
                    flashlight.color = new Color(0.8962264f, 0.1817818f, 0.1817818f, 0.3f);
                    player.GetComponent<PlayerMovement>().seen = true;
                }
                else
                {
                    flashlight.color = new Color(1, 1, 1, 0.3f);
                    player.GetComponent<PlayerMovement>().seen = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            flashlight.color = new Color(1, 1, 1, .3f);
            player.GetComponent<PlayerMovement>().seen = false;
        }
    }
}
