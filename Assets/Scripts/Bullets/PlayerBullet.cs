using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public Weapon weapon;
    public Player player;
    public float damageMultiplier = 1;
    public float bulletDamage;

    public bool penetrate;    // if true, then bullet always penetrate regardless penetrateCount

    private void Update()
    {
        // update bullet damage
        bulletDamage = damageMultiplier * (player.baseDamage + weapon.weaponDamage);
        
        // special color for penetrate bullet
        if (penetrate)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

    // when bullet collide with other objects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Monster")    // if penetrate bullet hits monster
        {
            bulletDamage = damageMultiplier * (player.baseDamage + weapon.weaponDamage);
            collision.gameObject.GetComponent<Monster>().monsterGetHit(direction, bulletPower, bulletDamage);    // call monsterGetHit in Monster Class
            //Debug.Log(bulletDamage);
            if (!penetrate)
            {
                removeBullet();
            }
        }
        else if (collision.tag == "Tree" || collision.tag == "Edge")    // if hit tree or edge, remove bullet
        {
            bulletDamage = damageMultiplier * (player.baseDamage + weapon.weaponDamage);
            removeBullet();
        }
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}
