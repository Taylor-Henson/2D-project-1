using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayer : MonoBehaviour
{
    public EnemyScript enemyS;
    public GameObject player;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemyS = GameObject.Find("Enemy1").GetComponent<EnemyScript>();
        player = GameObject.Find("PlayerSprite");
        enemy = GameObject.Find("Enemy1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyS.inAggresiveZone = true;
        }
        else
        {
            enemyS.inAggresiveZone = false;
        }
        
        
          
       
    }
}
