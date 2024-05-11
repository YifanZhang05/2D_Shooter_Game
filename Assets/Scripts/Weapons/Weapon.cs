using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Parent class Weapon
public class Weapon : MonoBehaviour
{
    public string displayName;
    public Sprite icon;
    [SerializeField] protected GameObject bullet;
    public float weaponDamage;

    public int level;
    public int maxLevel;
    [HideInInspector] public int[] upgradeCosts;    // cost to upgrade at each level. In addition, upgrade costs a weapon at current level
    [HideInInspector] public int[] sellPrices;    // sell price at each level
    [HideInInspector] public float[] pollusionPerAttack;    // pollusion per attack at diff. lvs
    [HideInInspector] public string[] description;    // weapon description at current level

    protected PollusionManager pollusionManager;


    // Start is called before the first frame update
    void Start()
    { 
        pollusionManager = GameObject.Find("Pollusion Manager").GetComponent<PollusionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Attack when left button is pressed and game not paused
        if (Input.GetMouseButtonDown(0) && Time.timeScale > 0)
        {
            //Debug.Log("attack");
            weaponAttack();
        }
    }

    // weapon attack (override in child classes for specific weapon type)
    protected virtual void weaponAttack()
    {
        
    }
}
