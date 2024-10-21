using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnMenu : MonoBehaviour
{
   public void RespawnGame()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
   }

   public void MainMenu()
   {
        SceneManager.LoadScene("Main Menu");
    }
}
