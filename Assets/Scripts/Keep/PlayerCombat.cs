using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    #region variables and references
    [Header("Variables")]
    //variables
    private float attackRange = 1;
    public int attackDamage = 1;

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
        if (Input.GetKeyDown("e"))
        {
            //Debug.Log("e");
            Attack();
        }
        //calling methods
    }
    #endregion

    #region attacking enemies
    void Attack()
    {
        //attack animation
        anim.SetTrigger("Attack");
        //Debug.Log("attack");

        //enemy detection
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D knight in hitEnemies)
        {
            //calls method in enemy scripts
            knight.GetComponent<EnemyDamage>().TakeDamage(attackDamage);
            //Debug.Log("hit");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    #endregion
}
