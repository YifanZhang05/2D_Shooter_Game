using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponInfoPage : MonoBehaviour
{

    public WeaponsShopManager weaponsShopManager;
    private List<Weapon> weapons;
    private Weapon weaponClass;

    // UI components
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private TextMeshProUGUI upgradeDescription;
    [SerializeField] private TextMeshProUGUI upgradeCost;    //Show that upgrading requires another same weapon at same lvl
    [SerializeField] private TextMeshProUGUI goldCostText;

    // Buttons
    [SerializeField] private Button sellButton;
    [SerializeField] private Button upgradeButton;

    // upgrade info
    private bool canUpgrade;
    private int upgradeWeaponIndex;    // another weapon used for upgrading the current weapon

    // Start is called before the first frame update
    void Start()
    {
        weaponClass = weaponsShopManager.weapon.GetComponentInChildren<Weapon>();

        // find weapon of same type and level
        canUpgrade = false;
        weapons = weaponsShopManager.weaponManager.weapons;
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i] != weaponClass)    // only look for weapons other than the current one
            {
                if (weapons[i].displayName == weaponClass.displayName && weapons[i].level == weaponClass.level)    // if both type and level are same
                {
                    canUpgrade = true;
                    upgradeWeaponIndex = i;
                }
            }
        }

        //update UI display texts and images
        nameText.text = weaponsShopManager.itemName;
        iconImage.sprite = weaponsShopManager.icon;
        sellText.text = "Sell: " + weaponClass.sellPrices[weaponClass.level - 1].ToString() + " Gold";

        if (weapons.Count > 1)
        {
            sellButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            sellButton.GetComponent<Image>().color = Color.gray;
        }

        upgradeDescription.text = weaponClass.description[weaponClass.level - 1];

        if (canUpgrade || weaponClass.upgradeCosts[weaponClass.level - 1] == -1)
        {
            upgradeCost.color = Color.black;
        }
        else
        {
            upgradeCost.color = Color.red;
        }

        if (weaponClass.upgradeCosts[weaponClass.level - 1] == -1)    // max level
        {
            upgradeCost.text = "MAX LEVEL";
            goldCostText.text = "MAX LEVEL";
        }
        else
        {
            upgradeCost.text = "Need: Lv. " + weaponClass.level + " " + weaponsShopManager.itemName;
            goldCostText.text = "Upgrade: " + weaponClass.upgradeCosts[weaponClass.level - 1].ToString() + " Gold";
        }

        if (!canUpgrade || weaponClass.upgradeCosts[weaponClass.level - 1] == -1)
        {
            upgradeButton.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            upgradeButton.GetComponent<Image>().color = Color.white;
        }

        // set OnClick for buttons
        sellButton.onClick.AddListener(sell);
        upgradeButton.onClick.AddListener(upgrade);
    }

    public void close()
    {
        WeaponsShopManager.infoPageOpen = false;
        Destroy(gameObject);
    }

    public void upgrade()
    {
        if (!canUpgrade)    // can't upgrade (doesn't have another same weapon)
        {
            return;
        }

        // have another same weapon
        if (weaponClass.level < weaponClass.maxLevel && weaponsShopManager.goldManager.gold >= weaponClass.upgradeCosts[weaponClass.level - 1])
        {
            weaponsShopManager.weaponManager.removeWeapon(upgradeWeaponIndex);
            weaponsShopManager.goldManager.gold -= weaponClass.upgradeCosts[weaponClass.level - 1];
            weaponClass.level++;
            weaponsShopManager.weaponsPage.updateWeaponPageDisplay();
            close();
        }
        else if (weaponsShopManager.goldManager.gold < weaponClass.upgradeCosts[weaponClass.level - 1])    // not enough money
        {
            StartCoroutine(ShopItemManager.noMoneyTextEffect(goldCostText));
        }
    }

    public void sell()
    {
        if (weapons.Count > 1)    // can only sell if has >1 weapons left
        {
            weaponsShopManager.goldManager.gold += weaponClass.sellPrices[weaponClass.level - 1];
            weaponsShopManager.weaponManager.removeWeapon(weapons.IndexOf(weaponClass));
            weaponsShopManager.weaponsPage.updateWeaponPageDisplay();
            close();
        }
    }
}
