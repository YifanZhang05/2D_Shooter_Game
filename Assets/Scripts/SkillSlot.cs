using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Use this script in the child of prefab called "script here"
// it's parent (the outside) is the indicator for "player selected this skill but haven't used it yet (for ex, player is aiming)"

public class SkillSlot : MonoBehaviour
{

    [SerializeField] private KeyCode key;    // key to press when you want to use the skill in this skill slot
    [SerializeField] private int index;    // index in equipedSkills that this slot is responsible for
    [SerializeField] private Image selectedIndicator;
    private SkillsManager skillsManager;
    private TextMeshProUGUI countDownText;

    // Start is called before the first frame update
    void Start()
    {
        skillsManager = GameObject.FindGameObjectWithTag("Skills Manager").GetComponent<SkillsManager>();
        countDownText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // display skill icon
        if (skillsManager.equipedSkills[index] != null)    // if skill slot not empty
        {
            gameObject.GetComponent<Image>().sprite = skillsManager.equipedSkills[index].icon;
            if (skillsManager.equipedSkills[index].countDown > 0)    // if skill in cooldown
            {
                gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
                countDownText.text = ((int)skillsManager.equipedSkills[index].countDown+1).ToString();
            }
            else
            {
                gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                countDownText.text = "";
            }

            // if the skill is selected, make the outer border black
            if (skillsManager.equipedSkills[index].selected)
            {
                selectedIndicator.color = Color.black;
            }
            else
            {
                selectedIndicator.color = Color.white;
            }
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = null;
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            countDownText.text = "";
        }
        
        // use the skill when press corresponding button
        if (Input.GetKeyDown(key) && skillsManager.equipedSkills[index] != null)
        {
            skillsManager.useEquipedSkill(index);
        }
    }
}
