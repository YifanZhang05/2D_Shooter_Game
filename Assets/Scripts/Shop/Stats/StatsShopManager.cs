using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsShopManager : ShopItemManager
{
    public void buySpeed()
    {
        if (goldManager.gold >= cost)
        {
            goldManager.gold -= cost;
            player.speed += 0.25f;
        }
        else
        {
            StartCoroutine(noMoneyTextEffect());
        }
    }

    public void buyAttack()
    {
        if (goldManager.gold >= cost)
        {
            goldManager.gold -= cost;
            player.baseDamage += 0.25f;
        }
        else
        {
            StartCoroutine(noMoneyTextEffect());
        }
    }

    public void buyHealth()
    {
        if (goldManager.gold >= cost)
        {
            goldManager.gold -= cost;
            player.maxHealth += 10;
            player.health += 10;
            player.healthBar.GetComponent<HealthManager>().setHealth(player.health, player.maxHealth);
        }
        else
        {
            StartCoroutine(noMoneyTextEffect());
        }
    }

    public void buyLuck()
    {
        if (goldManager.gold >= cost)
        {
            goldManager.gold -= cost;
            player.luck += 15;
        }
        else
        {
            StartCoroutine(noMoneyTextEffect());
        }
    }
}
