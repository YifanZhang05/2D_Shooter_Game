using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    [HideInInspector] public static int maxSkillEquipNum = 3;
    [HideInInspector] public static bool hasSelectedSkill = false;    // this controls that only 1 skill is selected at a time

    public List<Skill> skills;
    public Skill[] equipedSkills = new Skill[maxSkillEquipNum];

    // Player gets a new skill
    public void getNewSkill(Skill skill)
    {
        Skill skillObject = Instantiate(skill.gameObject, transform).GetComponent<Skill>();
        skills.Add(skillObject);
        // If equiped skill is not full, then equip the new skill
        if (skills.Count <= maxSkillEquipNum)
        {
            equipedSkills[skills.Count - 1] = skillObject;
            skillObject.equiped = true;
        }
    }

    // Player equips a skill by replacing an equiped skill
    public void swapSkill(Skill oldSkill, Skill newSkill)
    {
        for (int i = 0; i < maxSkillEquipNum; i++)
        {
            if (equipedSkills[i] == oldSkill)
            {
                oldSkill.equiped = false;
                newSkill.equiped = true;
                equipedSkills[i] = newSkill;
                return;
            }
        }
    }

    // Unequip a skill
    public void unequipSkill(Skill skill)
    {
        for (int i = 0; i < maxSkillEquipNum; i++)
        {
            if (equipedSkills[i] == skill)
            {
                skill.equiped = false;
                equipedSkills[i] = null;
                return;
            }
        }
    }

    // Equip a skill
    public void equipSkill(Skill skill)
    {
        for (int i = 0; i < maxSkillEquipNum; i++)
        {
            if (equipedSkills[i] == null)
            {
                skill.equiped = true;
                equipedSkills[i] = skill;
                return;
            }
        }
    }

    public void useEquipedSkill(int index)
    {
        if (equipedSkills[index].countDown <= 0 && Time.timeScale > 0)     // can only use skill if countDown is finished and game not paused
        {
            if (!hasSelectedSkill && equipedSkills[index].selected == false)
            {
                equipedSkills[index].selected = true;
                hasSelectedSkill = true;
            }
            else if (equipedSkills[index].selected == true)
            {
                equipedSkills[index].selected = false;
                hasSelectedSkill = false;
            }
            //equipedSkills[index].useSkill();
            //equipedSkills[index].selected = !equipedSkills[index].selected;
        }
    }

}
