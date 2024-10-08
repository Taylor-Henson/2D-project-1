using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlocksAfterKill : MonoBehaviour
{
    GameObject enemy;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy == null)
        {
            rb.velocity = new Vector2(0, 3);
        }
    }
}
