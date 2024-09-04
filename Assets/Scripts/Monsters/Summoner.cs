using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Monster
{
    [SerializeField] private GameObject skelecton;

    // only can spawn summoner when pollusion reaches 200 (out of 1000)
    private int minSpawnPollusion = 200;

    private void Awake()
    {
        
    }

    // Summoner's attack summon Skelectons
    protected override void monsterAttack()
    {
        Instantiate(skelecton, transform.position, Quaternion.identity);
    }

    // only can spawn summoner when pollusion reaches 200 (out of 1000)
    public override int getIndex()
    {
        pollusionManager = GameObject.Find("Pollusion Manager").GetComponent<PollusionManager>();
        if (pollusionManager.pollusion >= minSpawnPollusion)
        {
            return base.getIndex();
        }
        else
        {
            return 0;
        }
    }
}
