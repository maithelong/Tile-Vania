using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
   public void play()
    {
        SceneManager.LoadScene("Lv1");
    }
    public void quit()
    {

        Application.Quit();
    }    
}
