using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverSceneDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;

    // Update is called once per frame
    void Update()
    {
        scoreUI.text = "Score: " + ScoringSystem.score;
    }
}
