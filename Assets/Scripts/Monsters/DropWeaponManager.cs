using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for monsters to drop weapon randomly
public class DropWeaponManager : MonoBehaviour
{
    public possibleDropWeapons[] weaponDropInfo;
    public void dropRandomWeapon()
    {
        // generate random weapon, chance based on index
        int indexSum = 0;
        for (int i = 0; i < weaponDropInfo.Length; i++)
        {
            indexSum += weaponDropInfo[i].index;
        }
        int randInt = Random.Range(1, indexSum + 1);
        for (int i = 0; i < weaponDropInfo.Length; i++)
        {
            randInt -= weaponDropInfo[i].index;
            if (randInt <= 0)
            {
                Instantiate(weaponDropInfo[i].weaponDropItem, transform.position, transform.rotation);
                return;
            }
        }
    }
}

// Class for all possible weapons to drop
[System.Serializable]
public class possibleDropWeapons
{
    public GameObject weaponDropItem;
    public int index;    // higher index = more likely to drop this type of weapon 
}
