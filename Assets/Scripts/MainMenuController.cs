using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private string sceneName;

    public void PlayGame()
    {

        SceneManager.LoadScene(sceneName);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
