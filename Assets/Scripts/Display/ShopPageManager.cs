using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPageManager : PageManager
{

    public void openShop()
    {
        openPage();
    }

    public void closeShop()
    {
        closePage();
    }

    public void openStatsPage()
    {
        openPage("Stats Page");
    }

    public void openWeaponsPage()
    {
        openPage("Weapons Page");
    }

    public void openSkillsPage()
    {
        openPage("Skills Page");
    }

    public void openStructuresPage()
    {
        openPage("Structures Page");
    }
}
