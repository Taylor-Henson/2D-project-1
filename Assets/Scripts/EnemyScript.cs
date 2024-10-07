using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    #region variables and references
    [Header("Variables")]
    float ex;
    float ey;
    float x;
    float y;

    float distanceToPlayerX;
    float distanceToPlayerY;
    bool closeToPlayer;

    public float enemySpeed = 2.5f;
    public int health = 100;

    int patrolSpeed = 1;
    int speedMultiplier = 1;
    
    float offset;
    float newPos;
    
    public bool isOnGround = false;
    bool canTurn = true;
    float canTurnCooldown = 1;
    public bool aggresiveState = false;



    [Header("References")]
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Animator anim;

    public GameObject player;
    GameObject enemy;

    private PlayerController playerController;
    private Helper helper;

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

        enemy = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        GetPositions();
        Death();
        GroundCheck();
        PlayerCheck();
    }
    #endregion

    #region getting positions
    void GetPositions()
    {
        if(player != null)
        {
            ex = player.transform.position.x;
            ey = player.transform.position.y;

            x = transform.position.x;
            y = transform.position.y;

            distanceToPlayerX = ex - x;
            distanceToPlayerY = ey - y;

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
            
            if (rb.velocityX < 0)
            {
                anim.SetBool("idle", false);
                anim.SetBool("walk", true);
            }

            else if (rb.velocityX > 0)
            {
                anim.SetBool("idle", false);
                anim.SetBool("walk", true);
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

    void PlayerCheck()
    {
        if (distanceToPlayerX > -2 && distanceToPlayerX < 2)
        {
            closeToPlayer = true;
        }
        else
        {
            closeToPlayer = false;
        }
    }
    void Aggresive()
    {
        if (aggresiveState)
        {
            //make enemy run to player
            if (distanceToPlayerX > 0)
            {
                rb.velocity = new Vector2(enemySpeed, 0);
            }
            else if (distanceToPlayerX < 0)
            {
                rb.velocity = new Vector2(-enemySpeed, 0);
            }
            else if (distanceToPlayerX == 0)
            {
                rb.velocity = new Vector2(0, 0);
            }
            //make enemy face player
            if(distanceToPlayerX > 0)
            {
                sr.flipX = false;
            }
            else if(distanceToPlayerX < 0)
            {
                sr.flipX = true;
            }
        }
       
    }
    #endregion

    #region taking damage and dying
    public void TakeDamage()
    {
        health -= 34;
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
