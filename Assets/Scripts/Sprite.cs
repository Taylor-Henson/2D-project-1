using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region variables and references

    [Header("Variables")]
    public int health = 100;

    public float jumpForce = 20;
    public float speed = 2f;

    float x;
    float y;
    float ex;
    float ey;
    public float xDistance;
    public float yDistance;

    public bool inAttackRange;
    public bool enemyAlive;

    //the area where the player will die if they go too far
    int xBound = 13;
    int yBound = -3;

    //if the player is not null
    bool playerActive = true;

    [Header("References")]
    public Rigidbody2D playerRb;
    public Animator playerAnim;
    public SpriteRenderer playerSr;

    public GameObject box;

    public EnemyScript enemyScript;
    public GameManagerScript gameManagerScript;
   
    GameObject enemy;
    public HelperScript helper;
    #endregion

    #region start and update
    // Start is called before the first frame update
    void Start()
    {
       playerRb = GetComponent<Rigidbody2D>();
       playerAnim = GetComponent<Animator>();
       playerSr = GetComponent<SpriteRenderer>();

       box = GameObject.Find("Box 2");
       enemyScript = GameObject.Find("Enemy1").GetComponent<EnemyScript>();

       //enemyScript = enemy.GetComponent<EnemyScript>();
       helper = gameObject.AddComponent<HelperScript>();
       gameManagerScript = GetComponent<GameManagerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveSprite();
        Jump();
        Falling();
        Landing();
        Attacking();
        OutOfBounds();
        NoHealthLeft();

        if (enemyAlive == true)
        {
            GetPositions();
        }

        playerAnim.SetBool("idle", false);
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
    void MoveSprite() // moves sprite in two planes with optional AD controls as well as arrow keys
    {
        playerAnim.SetBool("walk", false);

        if (Input.GetKey("d") && playerActive)
        {
            playerRb.velocity = new Vector2(3f * speed, playerRb.velocity.y);
            playerAnim.SetBool("walk", true);
            playerSr.flipX = false;

        }

        if (Input.GetKey("a") && playerActive)
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
        //print("grounded=" + helper.isOnTheGround);

        if (Input.GetKeyDown("space") && helper.isOnTheGround && playerActive)
        {
            playerRb.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode2D.Impulse);
            helper.isOnTheGround = false;
            playerAnim.SetBool("jump", true);
        }
    }

    void Falling()
    {
       if(playerRb.velocity.y < -0.1)
       {
            //Debug.Log("falling");
            playerAnim.SetBool("fall", true);
       }
    }

    public void Landing()
    {
        //Debug.Log("landing method");
        if(helper.isOnTheGround)
        {
            //Debug.Log("on the ground");

            playerAnim.SetBool("fall", false);
            playerAnim.SetBool("jump", false);
            playerAnim.SetBool("idle", true);
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
            enemyScript.enemyTakeDamage = true;
        }
        else
        {
            enemyScript.enemyTakeDamage = false;
        }
    }
    #endregion

    #region collisions 
    void OnCollisionEnter2D(Collision2D collision) // when the sprite touches the ground 
    {
        if (collision.gameObject.CompareTag("Kill"))
        {
            Death();
        }
    }
    #endregion

    #region death
    
    private void Death()
    {
        Destroy(gameObject);
    }

    void OutOfBounds()
    {
        if(transform.position.x > xBound && transform.position.y < yBound)
        {
            Death();
        }
    }

    void NoHealthLeft()
    {
        if(health <= 0)
        {
            Death();
        }
    }

    
    #endregion





}

