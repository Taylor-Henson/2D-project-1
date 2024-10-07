using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    #region variables and references
    public int boxCount = 0;
    int boxesRequired = 4;

    float x = 12.54012f;

    public GameObject blockingPath;
    public GameObject player;
    #endregion

    #region start and update
    // Start is called before the first frame update
    void Start()
    {
        blockingPath = GameObject.Find("BlockPathBlocks");
        player = GameObject.Find("Player");
    }
    
    // Update is called once per frame
    void Update()
    {
        CountBoxes();
        Death();
    }
    #endregion

    #region methods
    void CountBoxes()
    {
        if(boxCount >= boxesRequired)
        {
            OpenPath();
        }
    }

    void OpenPath()
    {
        blockingPath.transform.position = new Vector2(x, -2f);
    }

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
