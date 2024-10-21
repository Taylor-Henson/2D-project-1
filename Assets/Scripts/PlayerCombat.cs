using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    #region variables and references
    [Header("Variables")]
    //variables
    public int health = 3;
    public TextMeshProUGUI health_text;

    //attack variables
    private float attackRange = 1;
    public int attackDamage = 1;
    private bool attackCooldown;

    [Header("References")]
    //layers
    public LayerMask enemyLayers;

    //components
    public Transform attackPoint;
    public Animator anim;

    //gameObjects
    public GameObject knight;
    #endregion

    #region start and update
    // Start is called before the first frame update
    void Start()
    {
        //components
        anim = GetComponent<Animator>();
        knight = GameObject.Find("Knight");
    }

    // Update is called once per frame
    void Update()
    {
        //user input
        if (Input.GetKeyDown("e") && attackCooldown == false)
        {
            //Debug.Log("e");
            Attack();
        }

        health_text.text = "Lives Left: " + health;

    }
    #endregion

    #region attacking enemies

    void Cooldown()
    {
        //cooldown between attacks
        attackCooldown = false;
    }
    void Attack()
    {
        //attack animation
        anim.SetTrigger("Attack");
        //cooldown
        attackCooldown = true;
        Invoke("Cooldown", 0.5f);

        //Debug.Log("attack");

        //enemy detection
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D hit in hitEnemies)
        {
            //calls method in enemy scripts
            hit.GetComponent<EnemyDamage>().TakeDamage(attackDamage);
            hit.GetComponent<EnemyDamage>().TakeDamage(attackDamage);
            //Debug.Log("hit");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    #endregion

    #region taking damage and dying
    public void TakeDamage()
    {
        //takes away health
        health -= 1;
        //animates it
        anim.SetBool("Hurt", true);

        if (health <= 0)
        {
            Die();
        }
    }

    public void GainHealth()
    {
        health += 2;
    }

    public void Die()
    {
        //kills player
        //Debug.Log("player died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion
}
