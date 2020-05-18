using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(4).gameObject.GetComponent<UnityEngine.UI.Button>().interactable =
            !PlayerPrefs.HasKey("LEVEL2") || PlayerPrefs.GetInt("LEVEL2") == 0 ? false : true;
    }

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
