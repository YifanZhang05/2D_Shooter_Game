using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsPage : MonoBehaviour
{

    [SerializeField] private SkillsManager skillsManager;
    [SerializeField] private GameObject skillDisplayItem;

    // Color of button when it's not equipped (button says "equip")
    private Color unequipedColor;
    // Color of button when it's equipped (button says "equipped")
    private Color equipedColor;

    private void Awake()
    {
        unequipedColor = Color.green;
        equipedColor = Color.red;
    }

    private void OnEnable()
    {
        updateSkillPageDisplay();
    }

    public void updateSkillPageDisplay()
    {
        // remove the old display
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        // display the current list of skills
        List<Skill> skills = skillsManager.skills;
        for (int i = 0; i < skills.Count; i++)
        {
            Skill skill = skills[i];
            SkillShopManager item = Instantiate(skillDisplayItem, transform).GetComponent<SkillShopManager>();
            item.skillsPage = this;

            // Set display info
            item.itemName = skill.displayName;
            item.description = skill.description;
            item.icon = skill.icon;
            item.cost = -2;    // cost=-2: means the button used for showing detail

            // Set other info
            item.skillsManager = skillsManager;
            item.skill = skill.gameObject;

            // Set equip/unequip button
            if (skill.equiped)    // if weapon is already equipped
            {
                item.equipButton.GetComponent<Image>().color = equipedColor;
                item.equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Unequip";
                item.gameObject.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f);
            }
            else
            {
                item.equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
                item.equipButton.GetComponent<Image>().color = unequipedColor;
                item.gameObject.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
