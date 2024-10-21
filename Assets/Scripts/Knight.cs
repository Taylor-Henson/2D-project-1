using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Knight : MonoBehaviour
{
    #region variables and references

    [Header("Variables")]
    //movement variables
    private float speed = 1.5f;
    private int characterDirection;

    //attack variables
    private bool attackCooldown;
    private bool pauseWalk;

    [Header("References")]
    //layermasks
    private LayerMask playerLayer;

    //components
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    public Transform point;
    public PlayerScript playerScript;

    //gamobjects
    public GameObject player;

    #endregion

    #region start and update
    // Start is called before the first frame update
    void Start()
    {
        //components
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        //layermasks
        playerLayer = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //methods
        Walk();
        Animations();
        HitDetection();

        //setting the character direction
        if (rb.velocityX < 0)
        {
            characterDirection = -1;
        }
        else
        {
            characterDirection = 1;
        }
    }
    #endregion

    #region movement
    void Walk()
    {
        //makes the knight walk 
        rb.velocity = new Vector2(speed, rb.velocity.y);

        //animates it
        if (rb.velocityX > 0)
        {
            anim.SetBool("Run", true);
        }
    }

    //turns the knight when it reaches a turnpoint
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TurnPoint"))
        {
            speed *= -1;
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
    #endregion

    #region Attack
    void HitDetection()
    {
        //player detection

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
        //performs the attack
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
        //cooldown where the knight cannot attack
        attackCooldown = false;
    }

    void HurtPlayer()
    {
        //makes the player lose health
        player.GetComponent<PlayerCombat>().TakeDamage();
    }
    #endregion
}

