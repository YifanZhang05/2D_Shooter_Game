using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject statsPage;
    [SerializeField] private GameObject weaponsPage;
    [SerializeField] private GameObject skillsPage;


    public void openShop()
    {
        shopUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void closeShop()
    {
        shopUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void openStatsPage()
    {
        statsPage.SetActive(true);
        weaponsPage.SetActive(false);
        skillsPage.SetActive(false);
    }

    public void openWeaponsPage()
    {
        statsPage.SetActive(false);
        weaponsPage.SetActive(true);
        skillsPage.SetActive(false);
    }

    public void openSkillsPage()
    {
        statsPage.SetActive(false);
        weaponsPage.SetActive(false);
        skillsPage.SetActive(true);
    }
}
