using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : Bullet
{

    public GunMonster monster;

    private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("-" + monster.bulletDamage);
            player.playerGetHit(gameObject);
            removeBullet();
        }

        else if (other.tag == "Tree" || other.tag == "Edge")
        {
            removeBullet();
        }
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}
