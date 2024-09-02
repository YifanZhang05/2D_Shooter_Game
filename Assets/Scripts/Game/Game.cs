using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Vector2 mousePos;

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }

    public static GameObject findPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }
}
