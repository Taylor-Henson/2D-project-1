using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region variables and references
    bool isOnGround = false;
    public float jumpForce = 20;
    public float speed = 2f;
    float xSpawn = -4.61f;
    float ySpawn = -1.51f;

    public Rigidbody2D playerRb;
    public Animator playerAnim;
    public SpriteRenderer playerSr;
    public GameObject box;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
       playerRb = GetComponent<Rigidbody2D>();
       playerAnim = GetComponent<Animator>();
       playerSr = GetComponent<SpriteRenderer>();
       box = GameObject.Find("Box 2");
    }

    // Update is called once per frame
    void Update()
    {
        MoveSprite();
        Jump();
        Falling();
        Landing();
        Attacking();
    }

    void MoveSprite() // moves sprite in four planes with optional WASD controls as well as arrow keys
    {
        playerAnim.SetBool("walk", false);

        if (Input.GetKey("right") == true || Input.GetKey("d") == true)
        {
            playerRb.velocity = new Vector2(3f * speed, playerRb.velocity.y);
            playerAnim.SetBool("walk", true);
            playerSr.flipX = false;

        }

        if (Input.GetKey("left") == true || Input.GetKey("a") == true)
        {
            playerRb.velocity = new Vector2(-3f * speed, playerRb.velocity.y);
            playerAnim.SetBool("walk", true);
            playerSr.flipX = true;
        }
    }

    void Jump() // enables jumping animation and forces
    { 
        if (Input.GetKeyDown("space") && isOnGround)
        {
            playerRb.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
            playerAnim.SetBool("jump", true);
        }
    }

    void Falling()
    {
       if(playerRb.velocity.y < 0)
        {
            playerAnim.SetBool("fall", true);
        }
    }

    void Landing()
    {
        if(isOnGround == true)
        {
            playerAnim.SetBool("fall", false);
            playerAnim.SetBool("jump", false);
            playerAnim.SetBool("idle", true);
        }
        if(isOnGround == false)
        {
            playerAnim.SetBool("idle", false);
        }

    }

    void Attacking()
    {
        playerAnim.SetBool("attack", false);
        if(Input.GetKeyDown("q"))
        {
            playerAnim.SetBool("attack", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // when the sprite touches the ground 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Kill"))
        {
            transform.position = new Vector3(xSpawn, ySpawn, 0);
        }
    }

   

}
