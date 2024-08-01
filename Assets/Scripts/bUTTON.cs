using UnityEngine.SceneManagement;
using UnityEngine;

public class bUTTON : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("poka negr");
    }
}
