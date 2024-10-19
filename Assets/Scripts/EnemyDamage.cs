using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    #region variables and references

    [Header("Variables")]
    public int maxHealth = 3;
    public int currentHealth;
    public bool iFrames;

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
        if (iFrames == false)
        {
            //takes damage from health
            currentHealth -= 1;
            iFrames = true;
            Invoke("IFrames", 0.5f);

            //animation
            anim.SetTrigger("Hurt");

            //when health reaches zero
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }
    void IFrames()
    {
        iFrames = false;
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
