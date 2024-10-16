using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    #region variables and references
    [Header("Variables")]
    //movement variables
    private int speed = 8;
    private int jumpForce = 20;

    //groundcheck
    public bool touchingGround = false;

    [Header("References")]
    //components
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    //layers
    LayerMask groundLayerMask;

    #endregion
    #region start and update
    // Start is called before the first frame update
    void Start()
    {
        //components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        //layers
        groundLayerMask = LayerMask.GetMask("Ground");

        //game objects
    }

    // Update is called once per frame
    void Update()
    {
        //calling methods
        Movement();
        Jump();
        Animations();
        GroundCheck();
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

        //makes the run animation play if the player is moving and end when they stop
        if (rb.velocityX > 0 || rb.velocityX < 0 && touchingGround)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        //makes the jump animation override others if the space key is pressed
        if (rb.velocity.y > 1)
        {
            Debug.Log("Jumping");
            anim.SetBool("Jump", true);
        }

        else if (rb.velocity.y < 0)
        {
            Debug.Log("falling;");
            anim.SetBool("Fall", true);
            anim.SetBool("Jump", false);
        }

        //landing on ground
        if (touchingGround)
        {
            Debug.Log("landing");
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", false);
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
        if (Input.GetKeyDown("space") && touchingGround)
        {
            rb.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode2D.Impulse);
        }
    }
    #endregion

    #region checks
    //where all checks occur

    //performs a check using Tags to see if the player is touching the ground
    private void GroundCheck()
    {
        //raycast variables
        float rayLength = 1.5f;
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        RaycastHit2D hit;

        //the raycast
        
        hit = Physics2D.Raycast(position, direction, rayLength, groundLayerMask);
        //visual raycast
        Debug.DrawRay(position, direction, Color.green);
        
        //if raycast hits ground
        if (hit.collider != null)
        {
            touchingGround = true;
            //Debug.Log("grounded");
        }
        else
        {
            touchingGround = false;
        }
    }

    #endregion
}
