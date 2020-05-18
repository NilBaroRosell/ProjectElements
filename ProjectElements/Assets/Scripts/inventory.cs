using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class inventory : MonoBehaviour
{
    [SerializeField] GameObject lifePrefab;
    [SerializeField] Transform lifes;
    [SerializeField] Text exp;
    [SerializeField] Transform coins;

    void Start()
    {
        if (PlayerPrefs.HasKey("lifes") || SceneManager.GetActiveScene().buildIndex == 0)
        {
            updateLifes(PlayerPrefs.GetInt("lifes"));
            updateExp(PlayerPrefs.GetInt("exp"));
        }
        else
        {
            updateLifes(3);
            updateExp(0);
        }
    }

    public void updateLifes(int incr)
    {
        int _lifes = lifes.childCount;
        _lifes += incr;
        foreach (Transform child in lifes){ Destroy(child.gameObject); }
        for (int i = 0; i < _lifes; i++)
        {
            RectTransform _transf = Instantiate(lifePrefab, lifes).GetComponent<RectTransform>();
            _transf.anchoredPosition += new Vector2(i * -40, 0);
        }
        if (_lifes == 0) { _lifes = 3; SceneManager.LoadScene(0); }
        PlayerPrefs.SetInt("lifes", _lifes);
    }

    public void updateExp(int incr)
    {
        exp.text = "EXP: " + (int.Parse(exp.text.Substring(4)) + incr).ToString();
        if(int.Parse(exp.text.Substring(4)) >= 50) { updateExp(-50); updateLifes(1); }
        else PlayerPrefs.SetInt("exp", int.Parse(exp.text.Substring(4)));
    }

    public void updateCoins(GameObject _coin)
    {
        for(int i = 0; i < coins.childCount; i++)
        {
            if(_coin == coins.GetChild(i).gameObject)
            {
                updateExp(10);
                _coin.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                return;
            }
        }
    }
}
