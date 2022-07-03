using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        Debug.Log("hey");
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void Level1()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Level2()
    {
        SceneManager.LoadScene("SampleScene 1");
    }
}
