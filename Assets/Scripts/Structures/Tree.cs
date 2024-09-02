using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Structure
{

    private float health = 50;
    private float maxHealth = 50;
    private float freezeMonsterTime = 1f;    // how much time monster will be frozen when they hit a tree
    private int treeGoldValue = 50;    // how many gold does it give to player when they break a tree
    private float reducePollusionSec = 2.5f;    // decrease how much pollusion every sec per tree
    private float fruitChance = 0.25f;    // chance to drop a fruit when tree is destroyed
    private GameObject goldManager;
    private PollusionManager pollusionManager;
    private Player player;

    [SerializeField] private GameObject fruit;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        goldManager = GameObject.Find("Gold Manager");
        pollusionManager = GameObject.Find("Pollusion Manager").GetComponent<PollusionManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            killTree();
        }

        // as health of tree decreases, make the inside (green part) more transparent
        SpriteRenderer sR = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sR.color = new Color(sR.color.r, sR.color.g, sR.color.b, health / maxHealth);

        // reduce pollusion every sec
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0;
            pollusionManager.decreasePollusion(reducePollusionSec);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        treeGetHit(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster Bullet")
        {
            treeGetHit(collision.gameObject);
        }
        else if (collision.tag == "Bullet")    // player's penetrate bullet
        {
            PlayerBullet pB = collision.GetComponent<PlayerBullet>();
            // tree reduce health.
            //health -= pB.player.baseDamage + pB.weapon.weaponDamage;
            health -= pB.bulletDamage;

            // If health <= 0 after that, player gets gold
            if (health <= 0)
            {
                goldManager.GetComponent<GoldManager>().gold += treeGoldValue;
                //Debug.Log(goldManager.GetComponent<GoldManager>().gold);
            }
        }
    }

    public void treeGetHit(GameObject hitSource)
    {
        if (hitSource.tag == "Monster")
        {
            Monster monster = hitSource.GetComponent<Monster>();

            // knockback and freeze monster shortly
            Vector3 knockBackDirection = (transform.position - hitSource.transform.position).normalized;
            monster.monsterGetHit(-knockBackDirection, monster.knockBackIndex, 0);
            monster.freezeMonster(freezeMonsterTime);

            // tree reduce health
            health -= monster.damage;

        }
        else if (hitSource.tag == "Monster Bullet")
        {
            // tree reduce heatlh
            health -= hitSource.GetComponent<MonsterBullet>().monster.bulletDamage;
        }
        else if (hitSource.tag == "Bullet")
        {
            PlayerBullet pB = hitSource.GetComponent<PlayerBullet>();
            // tree reduce health.
            //health -= pB.player.baseDamage + pB.weapon.weaponDamage;
            health -= pB.bulletDamage;

            // If health <= 0 after that, player gets gold
            if (health <= 0)
            {
                goldManager.GetComponent<GoldManager>().gold += treeGoldValue;
                //Debug.Log(goldManager.GetComponent<GoldManager>().gold);
            }
        }
    }

    public void treeLoseHealth(float damage, bool fromPlayer)
    {
        health -= damage;
        if (health <= 0 && fromPlayer)
        {
            goldManager.GetComponent<GoldManager>().gold += treeGoldValue;
        }
    }

    private void killTree()
    {
        // randomly decide drop fruit or not
        float chance = fruitChance * (1 + player.luck/100f);
        float randomValue = Random.Range(0.0f, 1.0f);
        if (randomValue < chance)
        {
            generateFruit();
        }
        Destroy(gameObject);
    }

    private void generateFruit()
    {
        Instantiate(fruit, transform.position, Quaternion.identity);
    }

}
