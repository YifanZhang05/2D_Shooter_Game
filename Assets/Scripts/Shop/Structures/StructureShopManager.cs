using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureShopManager : ShopItemManager
{
    [SerializeField] GameObject structure;

    public void buyTree()
    {
        if (goldManager.gold >= cost)
        {
            goldManager.gold -= cost;
            player.inventory.addObject(structure.GetComponent<Structure>(), 1);
        }
        else
        {
            StartCoroutine(noMoneyTextEffect());
        }
    }
}
