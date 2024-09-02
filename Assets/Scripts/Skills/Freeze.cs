using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : Skill
{

    [SerializeField] private float radius;
    [SerializeField] private float freezeTime;

    [SerializeField] private GameObject freezeEffect;

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

        // find all colliders in the fireball radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(Game.mousePos, radius);
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.tag == "Monster")
            {
                Monster monster = collider.GetComponent<Monster>();
                monster.freezeMonster(freezeTime, freezeEffect);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
