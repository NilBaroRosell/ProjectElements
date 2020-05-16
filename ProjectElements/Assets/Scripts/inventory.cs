using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{
    [SerializeField] GameObject lifePrefab;
    [SerializeField] Transform lifes;
    [SerializeField] Text exp;
    [SerializeField] Transform coins;

    void Start()
    {
        //if(PlayerPrefs.HasKey("lifes"))
        //{
        //}
        updateLifes(7);
    }

    public void updateLifes(int incr)
    {
        int _lifes = lifes.childCount;
        _lifes += incr;
        while (lifes.childCount != 0) Destroy(lifes.GetChild(0).gameObject);
        for (int i = 0; i < _lifes; i++)
        {
            RectTransform _transf = Instantiate(lifePrefab, lifes).GetComponent<RectTransform>();
            _transf.anchoredPosition += new Vector2(i * -40, 0);
        }
    }
}
