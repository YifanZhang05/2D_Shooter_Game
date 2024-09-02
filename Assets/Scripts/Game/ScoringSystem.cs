using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringSystem : MonoBehaviour
{
    public static int score;
    [SerializeField] private TextMeshProUGUI scoreUI;

    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        scoreUI.text = "Score: " + score;
    }
}
