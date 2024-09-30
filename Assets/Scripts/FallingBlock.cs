using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerSprite");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {
          
        }
    }
}
