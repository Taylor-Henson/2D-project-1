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
    float enemySpeed = 2.5f;

    [Header("References")]
    public GameObject player;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Animator anim;
    #endregion

    #region start and update
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPositions();
        FacePlayer();
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

        if (distanceToPlayer > -3 && distanceToPlayer< 3)
        {
            closeToPlayer = true;   
        }
        else
        {
            closeToPlayer = false;
        }

        if(closeToPlayer == true)
        {
            FollowPlayer();
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
    void FacePlayer()
    {

        if (ex < x)
        {
            sr.flipX = true;

        }
        else
        {
            sr.flipX = false;
        }

    }

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
}