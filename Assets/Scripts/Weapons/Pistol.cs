using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponClass
{

    public Pistol()
    {
        
    }

    public Pistol(int upgradeLevel)
    {
        gunName = "Pistol";
        this.upgradeLevel = upgradeLevel;
        switch (upgradeLevel)
        {
            //Base stats after buying
            case 0:
                damage = 1f;
                bulletSpeed = 20f;
                fireRate = 1f;
                magSize = 5;
                currentMagSize = 5;
                reloadTime = 2f;
                upgradeCost = 5;
                break;

            //Upgrade 1
            case 1:
                damage = 2f;
                bulletSpeed = 20f;
                fireRate = 1.5f;
                magSize = 7;
                currentMagSize = 7;
                reloadTime = 2f;
                upgradeCost = 10;
                break;

            //Upgrade 2
            case 2:
                damage = 2f;
                bulletSpeed = 20f;
                fireRate = 2f;
                magSize = 7;
                currentMagSize = 7;
                reloadTime = 1.75f;
                upgradeCost = 20;
                break;

            //Upgrade 3
            case 3:
                damage = 3f;
                bulletSpeed = 25f;
                fireRate = 2f;
                magSize = 9;
                currentMagSize = 9;
                reloadTime = 1.5f;
                upgradeCost = 0;
                break;
        }
    }

    public Pistol(int upgradeLevel, Sprite sprite)
    {
        gunName = "Pistol";
        gunSprite = sprite;
        this.upgradeLevel = upgradeLevel;
        switch (upgradeLevel)
        {
            //Base stats after buying
            case 0:
                damage = 1f;
                bulletSpeed = 20f;
                fireRate = 1f;
                magSize = 5;
                currentMagSize = 5;
                reloadTime = 2f;
                upgradeCost = 5;         
                break;

            //Upgrade 1
            case 1:
                damage = 2f;
                bulletSpeed = 20f;
                fireRate = 1.5f;
                magSize = 7;
                currentMagSize = 7;
                reloadTime = 2f;
                upgradeCost = 10;
                break;

            //Upgrade 2
            case 2:
                damage = 2f;
                bulletSpeed = 20f;
                fireRate = 2f;
                magSize = 7;
                currentMagSize = 7;
                reloadTime = 1.75f;
                upgradeCost = 20;
                break;

            //Upgrade 3
            case 3:
                damage = 3f;
                bulletSpeed = 25f;
                fireRate = 2f;
                magSize = 9;
                currentMagSize = 9;
                reloadTime = 1.5f;
                upgradeCost = 0;
                break;
        }
    }
}
