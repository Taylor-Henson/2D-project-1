using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    private float attackRange = 1;

    public int attackDamage = 1;

    public LayerMask enemyLayers;

    public Animator anim;

    public GameObject knight;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            //Debug.Log("e");
            Attack();
        }
    }

    void Attack()
    {
        //attack animation
        anim.SetTrigger("Attack");
        //Debug.Log("attack");

        //enemy detection
        Collider2D hitEnemies = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);

        if (hitEnemies)
        {
            knight.GetComponent<Knight>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        //DrawWireSphere(attackPoint.position, attackRange);
    }
}
