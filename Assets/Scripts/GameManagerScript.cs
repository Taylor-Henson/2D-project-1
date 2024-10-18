using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    #region variables and references

    private GameObject player;
    #endregion

    #region start and update
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerSprite");
    }
    
    // Update is called once per frame
    void Update()
    {
        Death();
    }
    #endregion

    #region methods
   

    void Death()
    {
        if(player == null)
        {
            if(Input.GetKeyDown("r"))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    #endregion
}
