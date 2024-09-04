using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Monster
{
    private int minSpawnPollusion = 600;

    private void Awake()
    {
        
    }

    // only can spawn summoner when pollusion reaches 600 (out of 1000)
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
