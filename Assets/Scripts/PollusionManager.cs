using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PollusionManager : MonoBehaviour
{
    public float pollusion;
    public float maxPollusion;
    [SerializeField] private Image pollusionBar;

    private float maxWidth;

    private void Start()
    {
        pollusion = 0;
        maxPollusion = 1000;

        maxWidth = pollusionBar.rectTransform.rect.width;
    }

    private void Update()
    {
        pollusionBar.rectTransform.sizeDelta = new Vector2((pollusion / maxPollusion) * maxWidth, pollusionBar.rectTransform.rect.height);
    }

    public void decreasePollusion(float value)
    {
        pollusion -= value;
        if (pollusion < 0)
        {
            pollusion = 0;
        }
        else if (pollusion > maxPollusion)
        {
            pollusion = maxPollusion;
        }
    }
}
