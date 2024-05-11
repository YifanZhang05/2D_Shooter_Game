using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{

    private void Awake()
    {
        weaponDamage = 2;
        level = 1;
        maxLevel = 3;
        pollusionPerAttack = new float[] { 10f, 15f, 20f };
        upgradeCosts = new int[] { 100, 200, -1 };    // -1 means at max level
        sellPrices = new int[] { 50, 100, 200 };
        description = new string[]
        {
            "Shoots 1 bullet",
            "Shoots 3 bullets in 3 directions. The 2 bullets in the side deals 50% damage",
            "Shoots 3 bullets in 3 directions. The middle bullet penetrates through enemies; other 2 bullets deal 75% damage"
        };
    }

    // Gun attack
    protected override void weaponAttack()
    {
        switch(level)
        {
            case 1:
                AttackLevel1();
                break;
            case 2:
                AttackLevel2();
                break;
            case 3:
                AttackLevel3();
                break;
        }

        // increase pollusion every attack
        pollusionManager.decreasePollusion(-pollusionPerAttack[level - 1]);
    }

    private GameObject shootOneBullet(Vector2 direction)
    {
        GameObject instantiatedBullet = Instantiate(bullet, transform.GetChild(0).position, Quaternion.identity);
        instantiatedBullet.GetComponent<PlayerBullet>().direction = direction;
        instantiatedBullet.GetComponent<PlayerBullet>().weapon = this;

        return instantiatedBullet;
    }

    // Attack for different levels
    private void AttackLevel1()    // Level 1: shoot 1 bullet when attack
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // get mouse position
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;    // find the direction that the bullet fly
        shootOneBullet(direction);
    }
    private void AttackLevel2()    // Lv 2: Shoots 3 bullets in 3 diff. directions
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // get mouse position
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;    // find the direction that the bullet fly

        // shoot in 3 directions, angle between them is 30 degrees. the other 2 bullets deal less damage
        shootOneBullet(direction);
        GameObject b1 = shootOneBullet((Quaternion.Euler(0f, 0f, 30) * direction).normalized);
        b1.GetComponent<PlayerBullet>().damageMultiplier = 0.5f;
        GameObject b2 = shootOneBullet((Quaternion.Euler(0f, 0f, -30) * direction).normalized);
        b2.GetComponent<PlayerBullet>().damageMultiplier = 0.5f;
    }
    private void AttackLevel3()    // Lv 3: the bullet in the middle goes through all enemies
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // get mouse position
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;    // find the direction that the bullet fly

        // shoot 3 bullets, the one in the middle go through enemies. the other 2 deal less damage
        GameObject b1 = shootOneBullet(direction);
        GameObject b2 = shootOneBullet((Quaternion.Euler(0f, 0f, 30) * direction).normalized);
        b2.GetComponent<PlayerBullet>().damageMultiplier = 0.75f;
        GameObject b3 = shootOneBullet((Quaternion.Euler(0f, 0f, -30) * direction).normalized);
        b3.GetComponent<PlayerBullet>().damageMultiplier = 0.75f;
        b1.GetComponent<PlayerBullet>().penetrate = true;    // make middle bullet penetrate
    }
}
