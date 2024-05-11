using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Skill
{

    [SerializeField] private float radius;
    [SerializeField] private float knockBack;    // how much does it knock back monsters
    [SerializeField] private float damage;

    [SerializeField] private ExplosionEffect explosion;

    protected override void Start()
    {
        base.Start();
        effectRange.transform.localScale = new Vector2(2 * radius, 2 * radius);
    }

    protected override void Update()
    {
        base.Update();
        if (selected)
        {
            effectRange.SetActive(true);
            effectRange.transform.position = Game.mousePos;
        }
        else
        {
            effectRange.SetActive(false);
        }
    }

    public override void useSkill()
    {
        base.useSkill();
        //Debug.Log("Fireball");

        // find all colliders in the fireball radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(Game.mousePos, radius);
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.tag == "Monster")
            {
                Monster monster = collider.GetComponent<Monster>();
                monster.monsterGetHit(((Vector2)monster.transform.position - Game.mousePos).normalized, knockBack, damage);
            }
            else if (collider.tag == "Tree")
            {
                Tree tree = collider.GetComponent<Tree>();
                tree.treeLoseHealth(damage, true);
            }
        }

        // Apply explosion effect
        explosion.startExplosionEffect(radius, Game.mousePos, 0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
