using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private GameObject player;
    public PlayerCombat playerCombat;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerSprite");
        playerCombat = player.GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
           playerCombat.Die();
        }
    }
}
