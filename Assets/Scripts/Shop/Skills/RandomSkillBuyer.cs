using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomSkillBuyer : MonoBehaviour
{
    [SerializeField] private int cost;
    // List of all skills to randomly choose from
    public List<GameObject> allSkills;
    [SerializeField] private SkillsManager skillsManager;
    [SerializeField] private SkillsPage skillsPage;
    [SerializeField] private GoldManager goldManager;
    private TextMeshProUGUI buttonText;

    private void Start()
    {
        buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        buttonText.text = "Random Skill\n" + cost.ToString() + " Gold";
    }

    private void Update()
    {
        if (allSkills.Count <= 0)
        {
            buttonText.text = "Already have all skills";
        }
    }

    public void buyRandomSkill()
    {
        // If player hasn't gotten all skills
        if (allSkills.Count > 0)
        {
            if (goldManager.gold < cost)
            {
                StartCoroutine(ShopItemManager.noMoneyTextEffect(buttonText));
                return;
            }
            goldManager.gold -= cost;
            int skillNum = allSkills.Count;
            int randIndex = Random.Range(0, skillNum);    // get a random index
            skillsManager.getNewSkill(allSkills[randIndex].GetComponent<Skill>());
            // refresh display
            skillsPage.updateSkillPageDisplay();
            // remove the selected random skill from allSkills, so player won't get it again
            allSkills.RemoveAt(randIndex);
        }
    }
    
}
