using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PlayerCombat playerCombat;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GameObject.Find("PlayerSprite").GetComponent<PlayerCombat>();
        player = GameObject.Find("PlayerSprite");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerCombat>().GainHealth();
            Destroy(gameObject);
            this.enabled = false;
        }
    }
}
