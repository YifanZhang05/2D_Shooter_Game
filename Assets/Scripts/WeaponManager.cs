using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{ 
    [SerializeField] private Player player;
    [SerializeField] private GameObject initWeapon;
    public List<Weapon> weapons;

    void Start()
    {

        getNewWeapon(initWeapon, true);

        // test
        getNewWeapon(initWeapon, true);
        weapons[0].weaponDamage = 10000;
        getWeaponObject(weapons[0]).GetComponent<SpriteRenderer>().color = Color.black;

    }

    // add new weapon to the List "weapons"; instantiate it. Can choose to active it or not
    public void getNewWeapon(GameObject weapon, bool active)
    {
        GameObject newWeapon = player.equipWeapon(weapon);
        weapons.Add(newWeapon.transform.GetChild(0).GetComponent<Weapon>());
        newWeapon.SetActive(active);

        if (active && newWeapon != player.currentWeapon)
        {
            if (player.currentWeapon != null)
            {
                player.currentWeapon.SetActive(false);
            }
            player.currentWeapon = newWeapon;
        }
    }

    // remove weapon at chosen index
    public void removeWeapon(int index)
    {
        weapons.RemoveAt(index);
        Destroy(transform.GetChild(index).gameObject);
    }

    // set the weapon at chosen index active, all other weapons not active
    private void activeWeapon(int index)
    {
        if (player.currentWeapon != null)
        {
            player.currentWeapon.SetActive(false);
        }
        transform.GetChild(index).gameObject.SetActive(true);
        player.currentWeapon = transform.GetChild(index).gameObject;
    }
    public void activeWeapon(GameObject weapon)
    {
        if (player.currentWeapon != null)
        {
            player.currentWeapon.SetActive(false);
        }
        weapon.SetActive(true);
        player.currentWeapon = weapon;
    }

    private GameObject getWeaponObject(Weapon weapon)
    {
        return transform.GetChild(weapons.IndexOf(weapon)).GetChild(0).gameObject;
    }
}
