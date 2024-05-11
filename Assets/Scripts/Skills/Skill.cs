using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    public static Color color = new Color(0, 0, 1, 75 / 225);

    public string displayName;
    public Sprite icon;
    public string description;
    public bool equiped;
    public bool selected;    // player has selected this skill, but not used it yet

    public float pollusion;    // how much pollusion does this skill create

    [SerializeField] protected float coolDown;    // in how many seconds can player use the skill
    [HideInInspector] public float countDown;    // how many seconds left until player can use the skill again
    [SerializeField] protected GameObject effectRange;    // a shape showing the effect range of the skill when skill is selected

    protected PollusionManager pollusionManager;
    protected Player player;

    protected virtual void Start()
    {
        pollusionManager = GameObject.Find("Pollusion Manager").GetComponent<PollusionManager>();
        player = transform.root.GetComponent<Player>();
    }

    protected virtual void Update()
    {
        if (countDown > 0 && equiped)
        {
            countDown -= Time.deltaTime;
        }

        // use the skill when right click in selected mode
        if (selected && Input.GetMouseButtonDown(1))
        {
            useSkill();
        }
    }

    public virtual void useSkill()
    {
        countDown = coolDown;
        selected = false;
        SkillsManager.hasSelectedSkill = false;
        pollusionManager.pollusion += pollusion;
    }
}
