using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public int gold;
    [SerializeField] private TextMeshProUGUI goldUI;

    private void Start()
    {
        gold = 0;
    }
    private void Update()
    {
        goldUI.text = "Gold: " + gold;
    }
}
