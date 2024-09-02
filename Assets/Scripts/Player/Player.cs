using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float baseDamage;

    private Rigidbody2D rb2D;
    public float health;
    public float maxHealth;
    public float speed;
    public float luck;    // chance to drop items from trees and monsters * (1+luck/100)
    public Inventory inventory;
    public GameObject currentWeapon;
    public GameObject healthBar;
    private float moveHoriz;
    private float moveVert;
    private float knockBackIndex;    // how much knockback player experiences when monster hits it (contact with monster)
    private float knockBackPower;    // how much player can knockback enemies when enemies hits player

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        health = 100;
        maxHealth = 100;
        speed = 4f;
        luck = 0f;
        baseDamage = 3;
        knockBackIndex = 500;
        knockBackPower = 500;
    }

    // Update is called once per frame
    void Update()
    {

        // check if player is alive
        if (health <= 0)
        {
            playerDie();
        }

        // get keyboard input for moving up/down and left/right
        moveHoriz = Input.GetAxisRaw("Horizontal");
        moveVert = Input.GetAxisRaw("Vertical");

        // get mouse's position used for making the player face mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;    // mouse pos - player pos
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;    // find angle to rotate
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    private void FixedUpdate()
    {
        // move player depending on the keyboard input and speed
        Vector3 tempVect = new Vector3(moveHoriz, moveVert, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        rb2D.MovePosition(rb2D.transform.position + tempVect);

    }

    // if player experiences a collision (gets hit by monster, for ex)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Monster")    // if hit by monster
        {
            playerGetHit(collision.gameObject);
        }
    }

    public GameObject equipWeapon(GameObject weapon)
    {
        if (weapon != null)
        {
            return Instantiate(weapon, transform.GetChild(0));
        }
        else
        {
            return null;
        }
    }

    // when player gets hit
    public void playerGetHit(GameObject hitSource)
    {
        if (hitSource.tag == "Monster" || hitSource.tag == "Monster Bullet")    // if hit by monster
        {
            Monster monster = hitSource.GetComponent<Monster>();

            // knockback player
            Vector3 knockBackDirection = (transform.position - hitSource.transform.position).normalized;
            rb2D.AddForce(knockBackDirection * knockBackIndex);

            if (hitSource.tag == "Monster")
            {
                // also knockBack monster according to monster's knockback index (but no damage) (only if monster itself hits)
                monster.monsterGetHit(-knockBackDirection, knockBackPower, 0);
                // reduce player health
                health -= monster.damage;
            }
            else if (hitSource.tag == "Monster Bullet")
            {
                health -= hitSource.GetComponent<MonsterBullet>().monster.bulletDamage;
            }

            healthBar.GetComponent<HealthManager>().setHealth(health, maxHealth);
            //Debug.Log(health);
        }
    }

    public void changeHealth(float addHealth)
    {
        health += addHealth;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthBar.GetComponent<HealthManager>().setHealth(health, maxHealth);
    }

    // move to a given location
    public void moveTo(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }

    // when player dies
    public void playerDie()
    {
        SceneManager.LoadScene("GameOverScene");
        //Destroy(gameObject);
    }

}
