using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void showOptions()
    {
        for (int i = 1; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(false);
    }
}
