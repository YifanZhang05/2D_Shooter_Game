using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to the monster body with rigidbody, collider, etc. 
// NOT to attach to the empty monster gameobject
public class Monster : MonoBehaviour
{
    protected GameObject player;
    private GameObject goldManager;
    private DropWeaponManager dropWeaponManager;    // used to manage dropping weapon when monster dies
    private Rigidbody2D rb2D;
    private float timer = 0;
    private float freezeTimer = 0;    // timer used to freeze monster
    private GameObject freezeEffect;    // monster's current freeze effect on it. Null = no freeze effect on it

    public float speed;
    public float damage;
    public float knockBackIndex;   // how much knockback monster experiences when collide (for ex, hit player, being hit by bullet)
    public float attackWait;    // how many seconds does monster wait to attack
    public float maxHealth;
    public float health;
    [SerializeField] private int goldValue;    // how much gold player gets when kill this monster
    private float freezeIndex = 1;    // 1 = regular freeze time. <1 = receive less freeze time. >1 = receive longer freeze time
    public float weaponDropChance = 0.05f;    // chance to drop weapon when killed. 1 = 100%

    public int index;    // higher index = more likely to spawn this type of monster

    public GameObject healthBar;
    public SpriteRenderer border;    // outside part of the monster

    private ShieldManager shieldManager;
    private RageManager rageManager;

    
    private PollusionManager pollusionManager;

    // set stats of monster
    protected void setStats(float speed, float damage, float knockBackIndex, float attackWait, float maxHealth, int goldValue, float freezeIndex, float weaponDropChance)
    {
        this.speed = speed;
        this.damage = damage;
        this.knockBackIndex = knockBackIndex;
        this.attackWait = attackWait;
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        this.goldValue = goldValue;
        this.freezeIndex = freezeIndex;
        this.weaponDropChance = weaponDropChance;
    }

    // Default stats
    protected void setDefaultStats()
    {
        this.speed = 2f;
        this.damage = 2;
        this.knockBackIndex = 500;
        this.attackWait = 3.0f;
        this.maxHealth = 30;
        this.health = 30;
        this.goldValue = 2;
        this.freezeIndex = 1;
        this.weaponDropChance = 0.05f;
    }

    private void Awake()
    {
        setDefaultStats();
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        goldManager = GameObject.Find("Gold Manager");
        pollusionManager = GameObject.Find("Pollusion Manager").GetComponent<PollusionManager>();
        dropWeaponManager = gameObject.GetComponent<DropWeaponManager>();

        // Randomly apply mutation based on current pollusion
        randomMutation();
    }

    // Update is called once per frame
    void Update()
    {
        // if monster has no health, it dies
        if (health <= 0)
        {
            monsterDie();
        }

        if (freezeTimer > 0)    // monster frozen
        {
            rb2D.velocity = Vector3.zero;
            freezeTimer -= Time.deltaTime;
        }
        else    // monster not frozen
        {
            freezeTimer = 0;
            if (freezeEffect != null)    // remove freeze effect, if there is
            {
                Destroy(freezeEffect);
                freezeEffect = null;
            }

            // monster attack every given amound of time
            timer += Time.deltaTime;
            if (timer >= attackWait)
            {
                monsterAttack();
                timer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (player != null && freezeTimer <= 0)    // only when player is alive and monster not freezed
        {
            moveMonster();

        }
    }

    // Monster mutations
    // -----------------------------------------------------------------------------------------
    // randomly decide whether or not monster has mutation. If it does, then randomly decide the type
    private void randomMutation ()
    {
        int randomInt = Random.Range(0, (int)pollusionManager.maxPollusion * 3/4);    // mutation chance = pollusion / (maxPollusion * 3/4)
        if (randomInt < pollusionManager.pollusion)    // monster is mutation
        {
            int mutationType = Random.Range(0, 4);    // Random.Range(a, b): include a, doesn't include b
            switch(mutationType)
            {
                case 0:
                    speedMutation();
                    break;
                case 1:
                    strengthMutation();
                    break;
                case 2:
                    shieldMutation();
                    break;
                case 3:
                    rageMutation();
                    break;
            }
        }
    }

    // Effects of speed mutation: faster, lighter and smaller
    private void speedMutation()
    {
        // change color
        border.color = Color.white;

        // change stats
        speed *= 1.75f;
        transform.localScale *= 0.75f;
        rb2D.mass *= 0.75f;
    }

    // Effects of strength mutation: receive less knockback & freeze, bigger, and heavier. More health
    private void strengthMutation()
    {
        // change color
        border.color = Color.yellow;

        // change stats
        knockBackIndex /= 1.5f;
        freezeIndex /= 1.35f;
        rb2D.mass *= 2f;
        health *= 1.25f;
        maxHealth *= 1.25f;
    }

    // Shield mutation: has a shield (equal to 1/5 of health). Shield recovers to 1/5 of current health every 5 seconds of not being attacked
    private void shieldMutation()
    {
        // change color
        border.color = Color.black;

        // load shield manager prefab from resources using its path, then get the ShieldManager class script
        shieldManager = Instantiate(Resources.Load("Shield Manager") as GameObject, transform).GetComponent<ShieldManager>();
        shieldManager.monster = gameObject.GetComponent<Monster>();
    }

    // Rage mutation: lower the health remained (%), higher the damage, movespeed, and attack speed
    private void rageMutation()
    {
        // change color
        border.color = Color.magenta;

        rageManager = Instantiate(Resources.Load("Rage Manager") as GameObject, transform).GetComponent<RageManager>();
        rageManager.monster = gameObject.GetComponent<Monster>();
    }
    // -----------------------------------------------------------------------------------------


    // move monster so it chases player
    private void moveMonster()
    {
        // direction monster towards player
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // make monster look at the player when moving
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;    // find angle to rotate
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // make monster move toward player
        direction = direction.normalized * speed * Time.deltaTime;
        rb2D.MovePosition(transform.position + direction);
    }

    // when monster gets hit
    public void monsterGetHit(Vector3 bulletDirection, float knockBackPower, float damage)
    {
        //Debug.Log("hit monster, damage = " + damage);
        // knockback
        rb2D.AddForce(bulletDirection*(knockBackPower+knockBackIndex));

        // reduce monster health
        monsterLoseHealth(damage);
        healthBar.GetComponent<HealthManager>().setHealth(health, maxHealth);
    }

    // freeze monster (make it unable to move) for a given time
    public void freezeMonster(float freezeTime)
    {
        freezeTimer = freezeTime *= freezeIndex;
    }

    // freeze monster with effect
    public void freezeMonster(float freezeTime, GameObject effect)
    {
        freezeTimer = freezeTime *= freezeIndex;
        freezeEffect = Instantiate(effect, transform);
    }

    // monster dies
    public void monsterDie()
    {
        // decide if drop weapon. If yes, decide what weapon to drop
        float chance = weaponDropChance * (1 + player.GetComponent<Player>().luck / 100f);
        float randomValue = Random.Range(0.0f, 1.0f);
        if (randomValue < chance)
        {
            dropWeaponManager.dropRandomWeapon();
        }

        goldManager.GetComponent<GoldManager>().gold += goldValue;
        Destroy(transform.parent.gameObject);    // remove the empty parent monster gameobject, so the monster body and health bar will also be removed
    }

    // the default type monster doesn't have separate attack. It only chase after player
    protected virtual void monsterAttack()
    {

    }

    private void monsterLoseHealth(float damage)
    {
        if (shieldManager == null || shieldManager.shieldValue <= 0)
        {
            health -= damage;
        }
        else
        {
            shieldManager.shieldValue -= damage;
            if (shieldManager.shieldValue < 0)
            {
                shieldManager.shieldValue = 0;
            }
        }
    }
    
}
