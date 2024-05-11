using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsShopManager : ShopItemManager
{
    [SerializeField] private GameObject infoPage;
    [HideInInspector] public static bool infoPageOpen = false;    // if there's a currently open info page. If that's the case, can't open more info page
    public GameObject equipButton;
    [HideInInspector] public WeaponManager weaponManager;
    [HideInInspector] public WeaponsPage weaponsPage;
    public GameObject weapon;

    public void equipWeapon()
    {
        weaponManager.activeWeapon(weapon);
        weaponsPage.updateWeaponPageDisplay();
    }

    public void openInfoPage()
    {
        if (!infoPageOpen)
        {
            GameObject page = Instantiate(infoPage, weaponsPage.transform.parent.parent);
            page.GetComponent<WeaponInfoPage>().weaponsShopManager = this;
            infoPageOpen = true;
        }
    }

    public void upgrade()
    {
        Weapon weaponClass = weapon.transform.GetChild(0).GetComponent<Weapon>();
        if (weaponClass.level < weaponClass.maxLevel)
        {
            goldManager.gold -= weaponClass.upgradeCosts[weaponClass.level-1];
            weaponClass.level++;
            weaponsPage.updateWeaponPageDisplay();
        }
    }
}
