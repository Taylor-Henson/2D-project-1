using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region variables and references
    [Header("Variables")]
    //movement variables
    private int speed = 8;
    private int jumpForce = 20;

    [Header("References")]
    //components
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    #endregion
    #region start and update
    // Start is called before the first frame update
    void Start()
    {
        //components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        //game objects
    }

    // Update is called once per frame
    void Update()
    {
        //calling methods
        Movement();
        Jump();
        Animations();
    }
    #endregion

    #region animations
    void Animations()
    {
        //where all animations occur

        //makes the player face left or right depending on their velocity (can be put into a helper)
        if (rb.velocityX < 0)
        {
            sr.flipX = true;
        }
        if ( rb.velocityX > 0)
        {
            sr.flipX = false;
        }

        //sanim.SetBool("Idle", true);
        //makes the run animation play if the player is moving and end when they stop
        if (rb.velocityX > 0 || rb.velocityX < 0)
        {
            //anim.SetBool("Idle", false);
            //anim.SetBool("Run", true);
        }
        else
        {
            //anim.SetBool("Idle", true);
            //anim.SetBool("Run", false);
        }        
    }
    #endregion

    #region player movement
    void Movement()
    {
        //press movement keys to apply a horizontal force
        if (Input.GetKey("d"))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        if (Input.GetKey("a"))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    void Jump()
    {
        //press space to apply a uppwards impulse force
        if (Input.GetKeyDown("space"))
        {
            rb.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode2D.Impulse);
        }
    }
    #endregion
}
