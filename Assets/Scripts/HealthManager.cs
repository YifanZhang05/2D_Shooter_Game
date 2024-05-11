using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    private float maxWidth;
    [SerializeField] private bool monsterHealth;    // whether or not the health bar is for monsters
    [SerializeField] private Transform monsterBody;    // only if monsterHealth is true
    [SerializeField] Transform canvas;    // only if monsterHealth is true

    // Start is called before the first frame update
    void Start()
    {
        maxWidth = healthBar.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        // if the health bar is monster's, move it to monster's location
        if (monsterHealth)
        {
            canvas.position = monsterBody.position;
        }
    }

    public void setHealth(float health, float maxHealth)
    {
        healthBar.rectTransform.sizeDelta = new Vector2((health/maxHealth)*maxWidth, healthBar.rectTransform.rect.height);
        //Debug.Log(health / maxHealth);
    }
}
