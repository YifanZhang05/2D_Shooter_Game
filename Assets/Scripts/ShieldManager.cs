using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public float shieldValue;
    [SerializeField] private float maxShield;
    [SerializeField] private float timer;
    private float reloadShieldTime;
    public Monster monster;
    private float prevUpdateHealth;    // health of monster at previous update
    public SpriteRenderer border;

    private void Start()
    {
        timer = 0;
        reloadShieldTime = 5f;
        prevUpdateHealth = monster.health;
        border = monster.border;
        shieldValue = monster.health / 5;    // shield value is set to 1/5 of monster health
        maxShield = shieldValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldValue <= 0)    // if doesn't have shield for enough time, get new shield
        {
            timer += Time.deltaTime;

            if (prevUpdateHealth > monster.health)    // if monster's health decreased, reset timer for shield to 0 (interrupt shield recovery)
            {
                timer = 0;
            }

            if (timer >= reloadShieldTime)
            {
                shieldValue = monster.health / 5;
                maxShield = shieldValue;
                timer = 0;
            }
        }

        // update the display of shield
        border.color = new Color(1 - shieldValue / maxShield, border.color.g, border.color.b);
        // if has shield, then health bar is black. Otherwise it's green
        if (shieldValue > 0)
        {
            monster.healthBar.GetComponent<HealthManager>().healthBar.color = Color.black;
        }
        else
        {
            monster.healthBar.GetComponent<HealthManager>().healthBar.color = Color.green;
        }

        prevUpdateHealth = monster.health;
    }
}
