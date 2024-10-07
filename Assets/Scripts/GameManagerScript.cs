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
    #endregion

    #region start and update
    // Start is called before the first frame update
    void Start()
    {
        blockingPath = GameObject.Find("BlockPathBlocks");
    }
    
    // Update is called once per frame
    void Update()
    {
        CountBoxes();
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
    #endregion
}
