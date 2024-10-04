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
    float distanceToPlayer;
    bool closeToPlayer;
    public float enemySpeed = 2.5f;
    public int health = 100;
    int patrolSpeed = 1;
    int speedMultiplier = 1;
    float offset;
    float newPos;
    public bool isOnGround = false;



    [Header("References")]
    public GameObject player;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Animator anim;
    private PlayerController playerController;
    GameObject enemy;
    private Helper helper;
    public LayerMask groundLayer;
    #endregion

    #region start and update
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerController = GameObject.Find("PlayerSprite").GetComponent<PlayerController>();
        enemy = GameObject.Find("Enemy");
        helper = GetComponent<Helper>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPositions();
        Death();
        Patrolling();
        GroundCheck();
    }
    #endregion

    #region getting positions
    void GetPositions()
    {
        ex = player.transform.position.x;
        ey = player.transform.position.y;

        x = transform.position.x;
        y = transform.position.y;

        distanceToPlayer = ex - x;

        if (distanceToPlayer > -3 && distanceToPlayer < 3)
        {
            closeToPlayer = true;   
        }
        else
        {
            closeToPlayer = false;
        }

        if(closeToPlayer == true)
        {
           // FollowPlayer();
            anim.SetBool("walk", true);
            anim.SetBool("idle", false);
        }
        else
        {
            anim.SetBool("walk", false);
            anim.SetBool("idle", true);
        }
    }
    #endregion

    #region facing and following player
   

    void FollowPlayer()
    {
       
        if(distanceToPlayer < 0)
        {
            rb.velocity = new Vector2(-enemySpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(enemySpeed, 0);
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
            playerController.enemyAlive = false;
        }
        else
        {
            playerController.enemyAlive = true;
        }
    }
    #endregion

    #region patrolling ai

    void Patrolling()
    {
        if (enemySpeed < 0)
        {
            sr.flipX = true;
            offset = -0.4f;
        }
        else
        {
            sr.flipX = false;
            offset = 0.4f;
        }

        if (!isOnGround)
        {
            enemySpeed *= -1;
            isOnGround = true;
        }

        rb.velocity = new Vector2(enemySpeed / 2, rb.velocityY);
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
