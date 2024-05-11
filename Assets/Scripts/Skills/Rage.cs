using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : Skill
{

    [SerializeField] private float rageDuration;
    [SerializeField] private GameObject rageEffect;
    private float timer;
    private bool rage;    // whether or not the player is in rage state

    protected override void Start()
    {
        base.Start();
        rageEffect.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
        if (rage)
        {
            timer += Time.deltaTime;
            if (timer >= rageDuration) // when rage is over
            {
                rage = false;
                timer = 0;

                // remove rage effects
                rageEffect.SetActive(false);
                player.transform.localScale /= 1.5f; // size
                player.speed /= 1.5f; // speed
                player.baseDamage /= 1.5f; // damage
            }
        }
    }

    public override void useSkill()
    {
        base.useSkill();
        Debug.Log("Rage");
        rage = true;
        timer = 0;

        // Apply rage effects
        rageEffect.SetActive(true);
        player.transform.localScale *= 1.5f; // size
        player.speed *= 1.5f; // speed
        player.baseDamage *= 1.5f; // damage
    }
}
