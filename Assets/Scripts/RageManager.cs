using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageManager : MonoBehaviour
{
    public Monster monster;
    private float baseDamage;
    private float baseSpeed;
    private float baseAttackWait;

    private void Start()
    {
        baseDamage = monster.damage;
        baseSpeed = monster.speed;
        baseAttackWait = monster.attackWait;
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercentLost = 1.0f - monster.health / monster.maxHealth;

        // increase monster damage, movespeed, and atk speed
        monster.damage = baseDamage * (1.0f + healthPercentLost/1.25f);
        monster.speed = baseSpeed * (1.0f + healthPercentLost/1.25f);
        monster.attackWait = baseAttackWait / (1.0f + healthPercentLost / 1.25f);
    }
}
