using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void NextLevel()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
