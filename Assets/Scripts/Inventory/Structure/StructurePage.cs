using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StructurePage : MonoBehaviour
{
    [SerializeField] private Inventory inv;
    [SerializeField] private GameObject structInvItem;

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
        structurePageDisplay();
    }

    // Display all Structures that player currently has according to the list stored in Inventory
    public void structurePageDisplay()
    {
        // remove the old display
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        // display the current list of weapons
        Dictionary<Structure, int> structs = inv.structs;

        foreach (KeyValuePair<Structure, int> entry in structs)
        {
            Structure structure = entry.Key;
            StructureInvManager item = Instantiate(structInvItem, transform).GetComponent<StructureInvManager>();

            item.itemName = structure.name;
            item.numOwned = entry.Value;
            item.icon = structure.icon;
            item.structure = structure.gameObject;
            item.sPage = this;

            // Set equip/unequip button
            GameObject equippedStruct = Game.findPlayer().GetComponentInChildren<StructurePlacer>().structure;
            if (equippedStruct != null && item.structure == equippedStruct)    // if structure is already equipped
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
