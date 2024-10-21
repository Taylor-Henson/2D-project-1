using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public bool dead = false;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerSprite");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -50);
        }
    }
}

// endssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss