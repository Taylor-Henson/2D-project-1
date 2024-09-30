using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestroy : MonoBehaviour
{
    private GameManagerScript gameManager;
    
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }
    // Update is called once per frame
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(base.gameObject);
            gameManager.boxCount++;
       
            
        }
    }
}
