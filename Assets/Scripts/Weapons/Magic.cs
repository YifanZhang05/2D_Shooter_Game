using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : Weapon
{

    private float[] weaponDamages = new float[] { 0f, 1f, 1f };    // weapon damage at each level
    private float[] attackRadii = new float[] { 1f, 1.25f, 1.25f };    // attack radius at each level
    private float[] freezeChances = new float[] { 0f, 0f, 0.05f };    // chance to freeze enemy at each level

    private void Awake()
    {
        level = 1;
        maxLevel = 3;
        pollusionPerAttack = new float[] { 10f, 15f, 20f };
        upgradeCosts = new int[] { 100, 200, -1 };    // -1 means at max level
        sellPrices = new int[] { 50, 100, 200 };
        description = new string[]
        {
            "Attack all enemies in a range",
            "Higher damage and larger radius",
            "attack has low chance to freeze enemy"
        };
    }

    // Magic attack
    protected override void weaponAttack()
    {
        GameObject bulletObject = Instantiate(bullet, Game.mousePos, Quaternion.identity);
        FreezeMagicAttack fma = bulletObject.GetComponent<FreezeMagicAttack>();
        Player player = Game.findPlayer().GetComponent<Player>();

        // set attack damage and radius
        fma.damage = (player.baseDamage + weaponDamages[level-1]); ;
        fma.radius = attackRadii[level-1];
        fma.freezeChance = freezeChances[level - 1];

        // increase pollusion every attack
        pollusionManager.decreasePollusion(-pollusionPerAttack[level - 1]);
    }
}
