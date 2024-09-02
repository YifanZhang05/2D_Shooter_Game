using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    [SerializeField] protected GameObject pageUI;

    [SerializeField] protected GameObject[] pages;

    // set active the page with given name, deactive other pages
    protected void openPage(string pg)
    {
        for (int i = 0; i < pages.Length; ++i)
        {
            if (pages[i].name == pg)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
    }

    public void openPage()
    {
        pageUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void closePage()
    {
        pageUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
