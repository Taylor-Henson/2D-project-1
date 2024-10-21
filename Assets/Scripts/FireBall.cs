using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    #region variables and references

    [Header("Variables")]

    public float force;
    private float timer;

    [Header("References")]

    private Rigidbody2D rb;

    private GameObject player;
    public PlayerCombat playerCombat;

    #endregion

    #region start and update
    // Start is called before the first frame update
    void Start()
    {
        //components
        rb = GetComponent<Rigidbody2D>();
        playerCombat = GetComponent<PlayerCombat>();

        //gameobjects
        player = GameObject.Find("PlayerSprite");

        //finds the x distance and y distance of the player
        Vector3 direction = player.transform.position - transform.position;
        //applys a force in the direction of the x and y distance
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        //finds a angle in radians based on the direction and converts it into degrees (negative as it needs to be in the direction of
        //the player from the player from the enemy, not the enemy from the player like in the line above).
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        //rotates the fireball based on that angle
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);
    }

    // Update is called once per frame
    void Update()
    {
        //timer to delete fireball
        timer += Time.deltaTime;

        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region player detection

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            player.GetComponent<PlayerCombat>().TakeDamage();
        }
    }

    #endregion
}

