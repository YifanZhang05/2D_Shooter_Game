using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShopManager : ShopItemManager
{
    public GameObject equipButton;
    [HideInInspector] public SkillsManager skillsManager;
    [HideInInspector] public SkillsPage skillsPage;
    public GameObject skill;

    public void equipAndUnequip()
    {
        if (skill.GetComponent<Skill>().equiped)    // if already equiped, unequip
        {
            skillsManager.unequipSkill(skill.GetComponent<Skill>());
        }
        else    // if not equiped, equip it
        {
            skillsManager.equipSkill(skill.GetComponent<Skill>());
        }
        skillsPage.updateSkillPageDisplay();
    }

}
