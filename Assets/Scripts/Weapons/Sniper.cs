using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : WeaponClass
{
    public Sniper()
    {
        
    }

    public Sniper(int upgradeLevel)
    {
        gunName = "Sniper";
        this.upgradeLevel = upgradeLevel;
        switch (upgradeLevel)
        {
            //Base stats after buying
            case 0:
                damage = 10f;
                bulletSpeed = 35f;
                fireRate = 0.5f;
                magSize = 3;
                currentMagSize = 3;
                reloadTime = 6f;
                upgradeCost = 30;
                break;

            //Upgrade 1
            case 1:
                damage = 15f;
                bulletSpeed = 35f;
                fireRate = 0.5f;
                magSize = 5;
                currentMagSize = 5;
                reloadTime = 5f;
                upgradeCost = 60;
                break;

            //Upgrade 2
            case 2:
                damage = 15f;
                bulletSpeed = 35f;
                fireRate = 0.5f;
                magSize = 5;
                currentMagSize = 5;
                reloadTime = 5f;
                upgradeCost = 100;
                break;

            //Upgrade 3
            case 3:
                damage = 20f;
                bulletSpeed = 40f;
                fireRate = 1f;
                magSize = 7;
                currentMagSize = 7;
                reloadTime = 4.5f;
                upgradeCost = 0;
                break;
        }
    }

    public Sniper(int upgradeLevel, Sprite sprite)
    {
        gunName = "Sniper";
        gunSprite = sprite;
        this.upgradeLevel = upgradeLevel;
        switch (upgradeLevel)
        {
            //Base stats after buying
            case 0:
                damage = 10f;
                bulletSpeed = 35f;
                fireRate = 0.5f;
                magSize = 3;
                currentMagSize = 3;
                reloadTime = 6f;
                upgradeCost = 30;
                break;

            //Upgrade 1
            case 1:
                damage = 15f;
                bulletSpeed = 35f;
                fireRate = 0.5f;
                magSize = 5;
                currentMagSize = 5;
                reloadTime = 5f;
                upgradeCost = 60;
                break;

            //Upgrade 2
            case 2:
                damage = 15f;
                bulletSpeed = 35f;
                fireRate = 0.5f;
                magSize = 5;
                currentMagSize = 5;
                reloadTime = 5f;
                upgradeCost = 100;
                break;

            //Upgrade 3
            case 3:
                damage = 20f;
                bulletSpeed = 40f;
                fireRate = 1f;
                magSize = 7;
                currentMagSize = 7;
                reloadTime = 4.5f;
                upgradeCost = 0;
                break;
        }
    }
}
