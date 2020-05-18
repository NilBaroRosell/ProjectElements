using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    GameObject canvas;
    private bool activated = false;
    public bool firstScene;
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.GetChild(0).gameObject;
        for(int i = 0; i < canvas.transform.childCount; i++)
        {
            canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(activated)
            {
                activated = false;
                for (int i = 0; i < canvas.transform.childCount; i++)
                {
                    canvas.transform.GetChild(i).gameObject.SetActive(false);
                }
                Time.timeScale = 1;
            }
            else
            {
                activated = true;
                for (int i = 0; i < 3; i ++)
                {
                    canvas.transform.GetChild(i).gameObject.SetActive(true);
                }
                canvas.transform.GetChild(3).gameObject.SetActive(true);
                if (firstScene)
                {
                    canvas.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
                    canvas.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    canvas.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                    canvas.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
                }
                Time.timeScale = 0;
            }
        }
    }

    public void ShowControls()
    {
        for(int i = 0; i < 4; i++)
        {
            canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
        canvas.transform.GetChild(4).gameObject.SetActive(true);
        canvas.transform.GetChild(5).gameObject.SetActive(true);
    }

    public void HideControls()
    {
        canvas.transform.GetChild(4).gameObject.SetActive(false);
        canvas.transform.GetChild(5).gameObject.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            canvas.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void GoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
