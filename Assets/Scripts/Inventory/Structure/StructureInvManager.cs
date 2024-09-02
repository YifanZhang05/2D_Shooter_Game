using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureInvManager : InvItemManager
{
    public GameObject structure;
    private StructurePlacer sPlacer;
    public GameObject equipButton;
    public StructurePage sPage;

    protected override void Start()
    {
        base.Start();
        Player p = Game.findPlayer().GetComponent<Player>();
        sPlacer = p.GetComponentInChildren<StructurePlacer>();
    }

    // When a structure is equiped, the structure placer will place that structure when used
    public void equipAndUnequip()
    {
        if (structure == sPlacer.structure)    // if already equiped, unequip
        {
            sPlacer.structure = null;
            sPlacer.icon = null;
            sPlacer.setEffectRange(null);
        }
        else    // if not equiped, equip it
        {
            sPlacer.structure = structure;
            sPlacer.icon = structure.GetComponent<Structure>().icon;
            sPlacer.setEffectRange(structure);
        }
        sPage.structurePageDisplay();
    }
}
