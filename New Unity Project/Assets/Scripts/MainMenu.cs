using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Animator fadeOut;

    public void PlayGame()
    {
        //HAY QUE AÑADIR LA ESCENA AL BUILDEAR
        Debug.Log("Empecemos");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        fadeOut.SetBool("Active", true);

        Invoke("LoadNextScene", 0.5f);
    }

    public void ExitGame()
    {
        Debug.Log("Debo cerrarme");
        Application.Quit();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("StageScreen1");
    }
}
