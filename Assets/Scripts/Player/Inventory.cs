using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Player player;
    public Dictionary<Structure, int> structs = new Dictionary<Structure, int> { };   // store all owned structures and # of it owned


    public void addObject(Structure obj, int num)
    {
        if (structs.ContainsKey(obj))
        {
            structs[obj] += num;
        }
        else
        {
            structs.Add(obj, num);
        }
    }

    public void removeObject(Structure obj, int num)
    {
        if (structs.ContainsKey(obj))
        {
            structs[obj] -= num;
            if (structs[obj] <= 0)
            {
                structs.Remove(obj);
            }
        }
    }
}
