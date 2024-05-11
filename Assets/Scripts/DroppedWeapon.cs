using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedWeapon : MonoBehaviour
{

    private WeaponManager weaponManager;
    [SerializeField] private GameObject weapon;

    private void Start()
    {
        weaponManager = GameObject.FindGameObjectWithTag("Weapon Manager").GetComponent<WeaponManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            weaponManager.getNewWeapon(weapon, false);
            Destroy(gameObject);
        }
    }
}
