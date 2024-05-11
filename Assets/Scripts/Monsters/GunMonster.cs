using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMonster : Monster
{
    [SerializeField] GameObject bullet;
    public float bulletDamage = 5;

    private void Awake()
    {
        setDefaultStats();
    }

    protected override void monsterAttack()
    {
        // direction monster towards player
        Vector3 direction = (player.transform.position - transform.position).normalized;
        GameObject instantiatedBullet = Instantiate(bullet, transform.GetChild(0).position, Quaternion.identity);
        instantiatedBullet.GetComponent<MonsterBullet>().direction = direction;
        instantiatedBullet.GetComponent<MonsterBullet>().monster = this;
    }

}
