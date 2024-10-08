using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    #region variables and references
    [Header("Variables")]
    float px;
    float py;
    float x;
    float y;

    float distanceToPlayerX;
    float distanceToPlayerY;
    bool closeToPlayer;

    public float enemySpeed = 2.5f;
    public int health = 100;

    float offset;
    float newPos;
    
    public bool isOnGround = false;
    bool canTurn = true;
    float canTurnCooldown = 1;
    public bool aggresiveState = false;

    public bool enemyTakeDamage;
    public bool moveDirection = false;
    public bool inAggresiveZone = false;



    [Header("References")]
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Animator anim;

    public GameObject player;
    GameObject enemy;

    private PlayerController playerController;
    private Helper helper;
    private EnemyAttack enemyAttack;

    public LayerMask groundLayer;
    public LayerMask playerLayer;
    #endregion

    #region start and update
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if(player !=null)
        {
            playerController = GameObject.Find("PlayerSprite").GetComponent<PlayerController>();
        }
        
        helper = GetComponent<Helper>();
        enemyAttack = GetComponent<EnemyAttack>();

        enemy = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        GetPositions();
        Death();
        GroundCheck();
        PlayerCheck();
        TakeDamage();
    }
    #endregion

    #region getting positions
    void GetPositions()
    {
        if(player != null)
        {
            //finds the enemy's and player's positions and the distance between them
            px = player.transform.position.x;
            py = player.transform.position.y;

            x = transform.position.x;
            y = transform.position.y;

            distanceToPlayerX = px - x;
            distanceToPlayerY = py - y;
            
            //sets whether the enemy is in aggresive or patrolling ai
            if (closeToPlayer == true)
            {
                Aggresive();
                aggresiveState = true;
                
            }
            else
            {
                aggresiveState  = false;
                Patrolling();
            }
            
            //sets the animations
            if (rb.velocityX < 0)
            {
                anim.SetBool("idle", false);
                anim.SetBool("walk", true);
                moveDirection = false;
            }

            else if (rb.velocityX > 0)
            {
                anim.SetBool("idle", false);
                anim.SetBool("walk", true);
                moveDirection = true;
            }
            else
            {
                anim.SetBool("idle", true);
                anim.SetBool("walk", false);
            }
        }

    }
    #endregion

    #region checking for player

    public void PlayerCheck()
    {
        //finds if the distance to player is close enough for the aggresive state to be enabled.
        if (distanceToPlayerX > -3 && distanceToPlayerX < 3 || inAggresiveZone == true)
        {
            closeToPlayer = true;
            enemyAttack.inAttackRange = true;
        }
        else
        {
            closeToPlayer = false;
            enemyAttack.canGoAgain = true;
        }
    }
    #endregion

    #region aggresive ai
    void Aggresive()
    {
        if (aggresiveState)
        {
            float speed = 1.5f;
            //moves towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
  
        //make enemy face player (should never glitch as no more flipX should be used)
        if(distanceToPlayerX > 0)
        {
            sr.flipX = false;
        }
        else if(distanceToPlayerX < 0)
        {
            sr.flipX = true;
        }
        
       
    }
    #endregion

    #region taking damage and dying
    public void TakeDamage()
    {
        if (enemyTakeDamage == true)
        {
            health -= 34;
        }
        
    }

    void Death()
    {
        if (health < 0)
        {
            Destroy(enemy);
            if (player != null)
            {
                playerController.enemyAlive = false;
            }
               
        }
        else
        {
            if (player != null)
            {
                playerController.enemyAlive = true;
            }

        }
    }
    #endregion

    #region patrolling ai

    void Patrolling()
    {
        //face direction walking
        if(!aggresiveState)
        if(rb.velocityX < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

       if(aggresiveState == false)
        {
            if (enemySpeed < 0)
            {
                offset = -0.2f;
            }
            else
            { 
                offset = 0.2f;
            }

            if (!isOnGround && canTurn)
            {
                enemySpeed *= -1;
                canTurn = false;
                isOnGround = true;
                Invoke("CanTurnReset", canTurnCooldown);
            }

            rb.velocity = new Vector2(enemySpeed / 2, rb.velocityY);
        }
    }

    void CanTurnReset()
    {
        canTurn = true;
    }

    #endregion

    #region groundcheck
    void GroundCheck()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.5f;

        Debug.DrawRay(position + new Vector2(offset, 0), direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }
    #endregion
}
