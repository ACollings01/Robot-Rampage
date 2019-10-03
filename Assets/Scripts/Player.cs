﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int armor;
    public GameUI gameUI;
    private GunEquipper gunEquipper;
    private Ammo ammo;

    // Start is called before the first frame update
    void Start()
    {
        ammo = GetComponent<Ammo>();
        gunEquipper = GetComponent<GunEquipper>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        // Takes in the amount of damage taken
        int healthDamage = amount;

        if (armor > 0)
        {
            // Each point of armor can take two damage
            int effectiveArmor = armor * 2;
            effectiveArmor -= healthDamage;

            // If there is remaining armor, no need to process health damage
            if (effectiveArmor > 0)
            {
                armor = effectiveArmor / 2;
                return;
            }
            armor = 0;
        }

        health -= healthDamage;
        Debug.Log("Health is " + health);

        if (health <= 0)
        {
            Debug.Log("GameOver");
        }
    }

    // Adds 50 health to a max of 200
    private void PickupHealth()
    {
        health += 50;
        if (health > 200)
        {
            health = 200;
        }
    }

    // Adds 15 armor
    private void PickupArmor()
    {
        armor += 15;
    }

    // Adds 50 assault rifle ammo
    private void PickupAssaultRifleAmmo()
    {
        ammo.AddAmmo(Constants.AssaultRifle, 50);
    }

    // Adds 20 pistol ammo
    private void PickupPistolAmmo()
    {
        ammo.AddAmmo(Constants.Pistol, 20);
    }

    // Adds 10 shotgun ammo
    private void PickupShotgunAmmo()
    {
        ammo.AddAmmo(Constants.Shotgun, 10);
    }

    // Determines pickup type and whether or not it is a valid pickup type
    public void PickUpItem(int pickupType)
    {
        switch (pickupType)
        {
            case Constants.PickUpArmor:
                PickupArmor();
                break;
            case Constants.PickUpHealth:
                PickupHealth();
                break;
            case Constants.PickUpAssaultRifleAmmo:
                PickupAssaultRifleAmmo();
                break;
            case Constants.PickUpPistolAmmo:
                PickupPistolAmmo();
                break;
            case Constants.PickUpShotgunAmmo:
                PickupShotgunAmmo();
                break;
            default:
                Debug.LogError("Bad pickup type passed: " + pickupType);
                break;
        }
    }
}
