using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsPage : MonoBehaviour
{

    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private GameObject weaponUpgradeItem;

    // Color of button when it's not equipped (button says "equip")
    private Color unequipedColor;
    // Color of button when it's equipped (button says "equipped")
    private Color equipedColor;

    private void Awake()
    {
        unequipedColor = Color.green;
        equipedColor = Color.gray;
    }

    private void OnEnable()
    {
        updateWeaponPageDisplay();
    }

    // Display all weapons that player currently has according to the list stored in WeaponManager
    public void updateWeaponPageDisplay()
    {
        // remove the old display
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        // display the current list of weapons
        List<Weapon> weapons = weaponManager.weapons;
        for (int i = 0; i < weapons.Count; i++)
        {
            Weapon weapon = weapons[i];
            WeaponsShopManager item = Instantiate(weaponUpgradeItem, transform).GetComponent<WeaponsShopManager>();
            item.weaponsPage = this;
            
            // Set display info
            item.itemName = weapon.displayName;
            item.description = "Lv. " + weapon.level;
            item.icon = weapon.icon;
            item.cost = -2;    // cost=-2: means the button used for showing detail

            // Set other info
            item.weaponManager = weaponManager;
            item.weapon = weapon.transform.parent.gameObject;

            // Set equip/unequip button
            if (weapon.gameObject.activeInHierarchy)    // if weapon is already equipped
            {
                item.equipButton.GetComponent<Image>().color = equipedColor;
                item.gameObject.GetComponent<Image>().color = new Color(0.75f,0.75f,0.75f);
            }
            else
            {
                item.equipButton.GetComponent<Image>().color = unequipedColor;
                item.gameObject.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
