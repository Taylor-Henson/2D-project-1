using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public int boxCount = 0;
    int boxesRequired = 4;

    float x = 12.54012f;

    public GameObject blockingPath;
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
}
