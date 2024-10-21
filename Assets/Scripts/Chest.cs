using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject player;
    public GameObject winScreen;
    public Animator anim;

    private void Start()
    {
        player = GameObject.Find("PlayerSprite");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Win");
            anim.SetTrigger("Win");
            Invoke("Win", 1.5f);

        }
    }

    void Win()
    {
        winScreen.SetActive(true);
    }
}
