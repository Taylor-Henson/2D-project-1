using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool inAttackRange = false;
    public bool canGoAgain = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inAttackRange && canGoAgain)
        {
            WaitForAFewSeconds();
        }
    }

    void WaitForAFewSeconds()
    {
        canGoAgain = false;

        if (inAttackRange )
        {
            InvokeRepeating("Attack", 1, 2);
        }
    }

    void Attack()
    {
        Debug.Log("attack");
    }


}
