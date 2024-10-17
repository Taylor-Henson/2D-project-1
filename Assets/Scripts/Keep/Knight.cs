using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Knight : MonoBehaviour
{
    private float speed = 1.5f;

    private LayerMask playerLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    public Transform point;

    public GameObject player;
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
        Attack();
    }

    void Walk()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

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

    void Attack()
    {

        
    }

}

