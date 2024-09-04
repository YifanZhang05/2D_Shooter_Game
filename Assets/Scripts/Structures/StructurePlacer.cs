using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePlacer : Skill
{

    public GameObject structure;    // the structure that will be placed
    [SerializeField] private Inventory inv;

    protected override void Start()
    {
        base.Start();
        
    }

    protected override void Update()
    {
        base.Update();

        // When struct placer activitated and effectRange (the structure) exists, show effectRange
        if (effectRange == null) { return; }
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

    // Place the structure at mouse position, and reduce the number of that structure owned
    public override void useSkill()
    {
        base.useSkill();
        if (structure != null)
        {
            GameObject s = Instantiate(structure, Game.mousePos, Quaternion.identity);
            s.GetComponent<Structure>().goldValue = 0;    // structure placed by player doesn't give gold
            inv.removeObject(structure.GetComponent<Structure>(), 1);
            if (!inv.structs.ContainsKey(structure.GetComponent<Structure>()))
            {
                structure = null;
                icon = null;
                Destroy(effectRange);
            }
        }
    }

    // set the display of structure that will be placed (make it blue and remove its collision)
    public void setEffectRange(GameObject ob)
    {
        Destroy(effectRange);
        if (ob != null)
        {
            effectRange = Instantiate(ob);
            effectRange.GetComponent<Collider2D>().enabled = false;
            effectRange.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    public GameObject getEffectRange()
    {
        return effectRange;
    }
}
