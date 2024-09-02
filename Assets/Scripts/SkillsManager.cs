using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    [HideInInspector] public static int maxSkillEquipNum = 3;
    [HideInInspector] public static bool hasSelectedSkill = false;    // this controls that only 1 skill is selected at a time

    public List<Skill> skills;

    // list of skill slots
    [SerializeField] private GameObject[] skillSlots = new GameObject[maxSkillEquipNum];
    [SerializeField] private KeyCode[] keys = new KeyCode[maxSkillEquipNum];
    // skill slot prefab
    [SerializeField] private GameObject skillSlot;
    [SerializeField] private GameObject canvas;

    private void Start()
    {
        // create skill slots (instead of having them ready before starting the game)
        for (int i = 0; i < maxSkillEquipNum; ++i)
        {
            skillSlots[i] = Instantiate(skillSlot, new Vector2(-85+85*i+canvas.transform.position.x, 0), Quaternion.identity, canvas.transform);
            SkillSlot sS = skillSlots[i].GetComponentInChildren<SkillSlot>();
            sS.key = keys[i];
        }
    }

    // Player gets a new skill
    public void getNewSkill(Skill skill)
    {
        Skill skillObject = Instantiate(skill.gameObject, transform).GetComponent<Skill>();
        skills.Add(skillObject);
        // If equiped skill is not full, then equip the new skill
        if (skills.Count <= maxSkillEquipNum)
        {
            skillSlots[skills.Count - 1].GetComponentInChildren<SkillSlot>().skill = skillObject;
            skillObject.equiped = true;
        }
    }

    // Player equips a skill by replacing an equiped skill
    public void swapSkill(Skill oldSkill, Skill newSkill)
    {
        for (int i = 0; i < maxSkillEquipNum; i++)
        {
            if (skillSlots[i].GetComponentInChildren<SkillSlot>().skill == oldSkill)
            {
                oldSkill.equiped = false;
                newSkill.equiped = true;
                skillSlots[i].GetComponentInChildren<SkillSlot>().skill = newSkill;
                return;
            }
        }
    }

    // Unequip a skill
    public void unequipSkill(Skill skill)
    {
        for (int i = 0; i < maxSkillEquipNum; i++)
        {
            if (skillSlots[i].GetComponentInChildren<SkillSlot>().skill == skill)
            {
                skill.equiped = false;
                skillSlots[i].GetComponentInChildren<SkillSlot>().skill = null;
                return;
            }
        }
    }

    // Equip a skill
    public void equipSkill(Skill skill)
    {
        for (int i = 0; i < maxSkillEquipNum; i++)
        {
            if (skillSlots[i].GetComponentInChildren<SkillSlot>().skill == null)
            {
                skill.equiped = true;
                skillSlots[i].GetComponentInChildren<SkillSlot>().skill = skill;
                return;
            }
        }
    }

    public void useEquipedSkill(Skill s)
    {
        if (s.countDown <= 0 && Time.timeScale > 0)     // can only use skill if countDown is finished and game not paused
        {
            if (!hasSelectedSkill && s.selected == false)
            {
                s.selected = true;
                hasSelectedSkill = true;
            }
            else if (s.selected == true)
            {
                s.selected = false;
                hasSelectedSkill = false;
            }
            //equipedSkills[index].useSkill();
            //equipedSkills[index].selected = !equipedSkills[index].selected;
        }
    }

}
