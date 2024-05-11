using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Skill
{

    [SerializeField] private float radius;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
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
        bool canTeleport = false; // if there's ground in the chosen location, set this to true

        // unselect skill when use skill
        selected = false;
        SkillsManager.hasSelectedSkill = false;

        Debug.Log("Teleport");

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(Game.mousePos, radius);
        foreach (Collider2D collider in hitColliders) // check everything at the landing location
        {
            //Debug.Log(collider.tag);
            if (collider.tag == "Ground") // check if ground is chosen
            {
                canTeleport = true;
            }
        }

        if (canTeleport) // only teleport, start cooldown, and add pollusion if canTeleport is true (i.e. ground is chosen)
        {
            // teleport player to the position
            base.useSkill();
            player.moveTo(effectRange.transform.position.x, effectRange.transform.position.y);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
