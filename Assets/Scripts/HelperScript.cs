using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperScript : MonoBehaviour
{

    #region variables and references

    public bool isOnTheGround = false;

    public LayerMask groundLayer;

    private GameObject playerSprite;
    private PlayerController playerScript;
    #endregion

    #region start and update

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<PlayerController>();
        playerSprite = GameObject.Find("PlayerSprite");

        groundLayer = LayerMask.GetMask("Ground");

    }

    // Update is called once per frame
    void Update()
    {
        DoGroundCheck();
    }
    #endregion

    #region RayCasts
    void DoGroundCheck()
    {
        //Debug.Log("do groundcheck raycast");
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.5f;

        Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            isOnTheGround = true;
            //Debug.Log("on ground");
        }
        else
        {
            isOnTheGround = false;
        }
    }

    #endregion




}
