using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Knight : MonoBehaviour
{
    private float speed = 1.5f;

    private int characterDirection;

    private bool attackCooldown;
    private bool pauseWalk;

    private LayerMask playerLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    public Transform point;

    public GameObject player;
    public PlayerScript playerScript;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        playerLayer = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Animations();
        HitDetection();

        if (rb.velocityX < 0)
        {
            characterDirection = -1;
        }
        else
        {
            characterDirection = 1;
        }
    }

    void Walk()
    {

        if (pauseWalk == false)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        if (rb.velocityX > 0)
        {
            anim.SetBool("Run", true);
        }
    }

    void Animations()
    {
        //makes the player face left or right depending on their velocity (can be put into a helper)
        if (rb.velocityX < 0)
        {
            sr.flipX = true;
        }
        if (rb.velocityX > 0)
        {
            sr.flipX = false;
        }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TurnPoint"))
        {
            speed *= -1;
        }
    }

    void HitDetection()
    {
        float rayLength = 1;
        Vector2 position = transform.position;
        Vector2 direction = Vector2.right * new Vector2 (characterDirection, 0f);
        RaycastHit2D hit;

        //the raycast

        hit = Physics2D.Raycast(position, direction, rayLength, playerLayer);
        //visual raycast
        Debug.DrawRay(position, direction, Color.green);

        if (hit.collider != null)
        {
            //Debug.Log("attack");
            Attack();
        }

    }

    void Attack()
    {
        if (attackCooldown == false)
        {
            //animation 
            anim.SetTrigger("Attack");
            //damage player
            Invoke("HurtPlayer", 0.5f);
            //cooldown
            attackCooldown = true;
            Invoke("Cooldown", 4);
        }
    }

    void Cooldown()
    {
        attackCooldown = false;
    }

    void HurtPlayer()
    {
        player.GetComponent<PlayerCombat>().TakeDamage();
    }

}

