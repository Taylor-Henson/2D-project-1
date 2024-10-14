using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool inAttackRange = false;
    public bool canGoAgain = true;
    public bool inMeleeRange = false;

    private PlayerController playerController;
    private GameObject player;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerSprite");
        playerController = player.GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
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
            anim.SetBool("attack", false);
        }
    }

    void Attack()
    {
        if(inMeleeRange)
        { 
            playerController.health -= 25;
            anim.SetBool("attack", true);
        }
    }


}
