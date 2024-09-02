using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Use this script in the child of prefab called "script here"
// it's parent (the outside) is the indicator for "player selected this skill but haven't used it yet (for ex, player is aiming)"

public class SkillSlot : MonoBehaviour
{

    public KeyCode key;    // key to press when you want to use the skill in this skill slot
    public Skill skill;
    [SerializeField] private Image selectedIndicator;
    private SkillsManager skillsManager;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI keyText;

    // Start is called before the first frame update
    void Start()
    {
        skillsManager = GameObject.FindGameObjectWithTag("Skills Manager").GetComponent<SkillsManager>();
        if (key == KeyCode.Alpha1)
        {
            keyText.text = "1";
        }
        else if (key == KeyCode.Alpha2)
        {
            keyText.text = "2";
        }
        else if (key == KeyCode.Alpha3)
        {
            keyText.text = "3";
        }
        else
        {
            keyText.text = key.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // display skill icon
        if (skill != null)    // if skill slot not empty
        {
            gameObject.GetComponent<Image>().sprite = skill.icon;
            if (skill.countDown > 0)    // if skill in cooldown
            {
                gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
                countDownText.text = ((int)skill.countDown+1).ToString();
            }
            else
            {
                gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                countDownText.text = "";
            }

            // if the skill is selected, make the outer border black
            if (skill.selected)
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
        if (Input.GetKeyDown(key) && skill != null)
        {
            skillsManager.useEquipedSkill(skill);
        }
    }
}
