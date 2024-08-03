using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Button : MonoBehaviour
{
    // Method to load the next scene in the build index
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Method to quit the application
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has been quit.");
    }
}

