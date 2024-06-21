using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene(5);
    }

    public void retourmenu()
    {
        SceneManager.LoadScene(0);
    }

    public void choixlevel()
    {
        SceneManager.LoadScene(1);
    }

    public void level1()
    {
        SceneManager.LoadScene(2);
    }

    public void level2()
    {
        SceneManager.LoadScene(3);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void compris()
    {
        SceneManager.LoadScene(2);
    }

    public void compris2()
    {
        SceneManager.LoadScene(6);
    }
}
