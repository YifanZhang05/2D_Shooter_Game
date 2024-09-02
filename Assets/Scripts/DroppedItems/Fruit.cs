using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            applyFruitEffect(collision.transform.root.GetComponent<Player>());
        }
    }

    private void applyFruitEffect(Player player)
    {
        player.changeHealth(20f);
        Destroy(gameObject);
    }
}
