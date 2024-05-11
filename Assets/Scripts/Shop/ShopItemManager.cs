using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemManager : MonoBehaviour
{
    // information about the item (to edit when there's a new item)
    public string itemName;
    public string description;
    public Sprite icon;

    // when cost = -1, that means you can't buy the item anymore
    // Cost = -2: the cost button used for showing detail instead of buying things
    public int cost;

    // Components in the display of the item in UI (NOT to change when there's a new item)
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI costText;

    // other
    public GoldManager goldManager;
    [SerializeField] protected Player player;

    private void Start()
    {
        // update the display on UI
        nameText.text = itemName;
        descriptionText.text = description;
        iconImage.sprite = icon;
        if (cost == -1)
        {
            costText.text = "MAX";
        }
        else if (cost == -2)
        {
            costText.text = "Detail";
        }
        else
        {
            costText.text = cost.ToString();
        }

        // get the gold manager and player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        goldManager = GameObject.Find("Gold Manager").GetComponent<GoldManager>();
    }

    // Effect on the costText when player try to buy but doesn't have enough money
    public static IEnumerator noMoneyTextEffect(TextMeshProUGUI costText)
    {
        costText.color = Color.red;
        yield return new WaitForSecondsRealtime(0.1f);
        costText.color = Color.black;
    }
    public IEnumerator noMoneyTextEffect()
    {
        costText.color = Color.red;
        yield return new WaitForSecondsRealtime(0.1f);
        costText.color = Color.black;
    }
}
