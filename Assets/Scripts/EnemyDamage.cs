using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    #region variables and references

    [Header("Variables")]
    public int maxHealth = 3;
    public int currentHealth;

    [Header("References")]
    public Animator anim;

    #endregion

    #region start and update
    // Start is called before the first frame update
    void Start()
    {
        //components
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region taking damage and dying
    public void TakeDamage(int damage) // passes in damage from playerScript
    {
        //takes damage from health
        currentHealth -= damage;

        //animation
        anim.SetTrigger("Hurt");

        //when health reaches zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //die animation
        anim.SetBool("isDead", true);
        //disable enemy
        this.enabled = false;
        Destroy(gameObject, 3);
        GetComponent<Collider2D>().enabled = false;
        //Debug.Log("knight died");
    }
    #endregion
}
