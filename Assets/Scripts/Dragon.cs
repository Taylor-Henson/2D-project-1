using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    #region variables and references

    [Header("Variables")]
    public bool isFacingRight;
    private int characterDirection;
    private bool cooldown;

    [Header("References")]

    //components
    public Transform firePoint;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Animator anim;

    //gameobjects
    public GameObject player;
    public GameObject bullet;
    
    //scripts
    public FireBall fireBall;

    //layers
    private LayerMask playerLayer;

    #endregion

    #region start and update

    // Start is called before the first frame update
    void Start()
    {
        //components
        fireBall = GetComponent<FireBall>();
        //gameobjects
        player = GameObject.Find("PlayerSprite");
        //layers
        playerLayer = LayerMask.GetMask("Player");
    }
    // Update is called once per frame
    void Update()
    {
        //methods
        FindPlayer();
        Detection();
    }

    #endregion

    #region player detection
    void Detection()
    {
        //player detection

        float rayLength = 5;
        Vector2 position = transform.position;
        Vector2 direction = Vector2.right * new Vector2(characterDirection, 0f);
        RaycastHit2D hit;

        //the raycast

        hit = Physics2D.Raycast(position, direction, rayLength, playerLayer);
        //visual raycast
        Debug.DrawRay(position, direction, Color.green);

        if (hit.collider != null && cooldown == false)
        {
            //Debug.Log("attack");
            Shoot();
        }
    }

    #endregion

    #region finding player
    void FindPlayer()
    {
        //if player is to the left of the dragon
        if (transform.position.x > player.transform.position.x)
        {
            //flip animation
            sr.flipX = true;
            isFacingRight = false;
            characterDirection = -1;
        }
        else
        {
            sr.flipX = false;
            isFacingRight = true;
            characterDirection = 1;
        }
    }

    #endregion

    #region shooting fireball
    void Shoot()
    {
        //animation
        anim.SetTrigger("Attack");
        //set cooldown
        cooldown = true;
        Invoke("Cooldown", 2.5f);
        //instantiate fireball
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }

    //cooldown
    void Cooldown()
    {
        cooldown = false;
    }
    #endregion
}
