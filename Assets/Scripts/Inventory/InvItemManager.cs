using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvItemManager : MonoBehaviour
{
    // information about the item (to edit when there's a new item)
    public string itemName;
    public int numOwned;
    public Sprite icon;

    // Components in the display of the item in UI (NOT to change when there's a new item)
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI numOwnedText;
    [SerializeField] private Image iconImage;

    virtual protected void Start()
    {
        // update the display on UI
        nameText.text = itemName;
        numOwnedText.text = numOwned.ToString();
        iconImage.sprite = icon;
    }
}
