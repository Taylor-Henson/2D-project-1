using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region variables and references
   
    [Header("Variables")]
    public float jumpForce = 20;
    public float speed = 2f;
    float xSpawn = -4.61f;
    float ySpawn = -1.51f;
    float x;
    float y;
    float ex;
    float ey;
    public float xDistance;
    public float yDistance;
    public bool inAttackRange;
    public bool enemyAlive;
   


    [Header("References")]
    public Rigidbody2D playerRb;
    public Animator playerAnim;
    public SpriteRenderer playerSr;
    public GameObject box;
    private EnemyScript enemyScript;
   
    GameObject enemy;
    private Helper helper;

   
    #endregion

    #region start and update
    // Start is called before the first frame update
    void Start()
    {
       playerRb = GetComponent<Rigidbody2D>();
       playerAnim = GetComponent<Animator>();
       playerSr = GetComponent<SpriteRenderer>();
       box = GameObject.Find("Box 2");
       enemyScript = GameObject.Find("Enemy").GetComponent<EnemyScript>();
       enemy = GameObject.Find("Enemy");
       helper = GetComponent<Helper>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveSprite();
        Jump();
        Falling();
        Landing();
        Attacking();

        if (enemyAlive == true)
        {
            GetPositions();
        }
    }

        #endregion

    #region getpositions
        void GetPositions()
    {
        x = transform.position.x;
        y = transform.position.y;
        ex = enemy.transform.position.x;
        ey = enemy.transform.position.y;

        xDistance = ex - x;
        yDistance = ey - y;

        if (xDistance < 1 && xDistance > -1)
        {
            inAttackRange = true;
        }
        else
        {
            inAttackRange = false;
        }
    }
    #endregion

    #region movement
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
    #endregion

    #region jumping and landing
    void Jump() // enables jumping animation and forces
    { 
        if (Input.GetKeyDown("space") && helper.isOnGround == true  )
        {
            playerRb.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode2D.Impulse);
            helper.isOnGround = false;
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
        if(helper.isOnGround == true)
        {
            playerAnim.SetBool("fall", false);
            playerAnim.SetBool("jump", false);
            playerAnim.SetBool("idle", true);
        }
        if(helper.isOnGround == false)
        {
            playerAnim.SetBool("idle", false);
        }

    }
    #endregion

    #region attacking
    void Attacking()
    {
        playerAnim.SetBool("attack", false);
        if(Input.GetKeyDown("q"))
        {
            playerAnim.SetBool("attack", true);
        }

        if(Input.GetKeyDown("q") && inAttackRange == true)
        {
            enemyScript.TakeDamage();
        }
    }
    #endregion

    #region collisions 
    void OnCollisionEnter2D(Collision2D collision) // when the sprite touches the ground 
    {
        if (collision.gameObject.CompareTag("Kill"))
        {
            transform.position = new Vector3(xSpawn, ySpawn, 0);
        }
    }
    #endregion

    
    
    
}

