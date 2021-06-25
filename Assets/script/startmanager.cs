using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class startmanager : MonoBehaviour
{
    public void start()
    {
        Time.timeScale = 4;
        SceneManager.LoadScene("ingame");
    }

    public void easy()
    {
        Time.timeScale = 2;
        SceneManager.LoadScene("ingame");
    }

    public void exit()
    {        
        Application.Quit();             
    }
}
