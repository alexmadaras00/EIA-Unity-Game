using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : WeaponClass
{
    public AssaultRifle()
    {
        
    }

    public AssaultRifle(int upgradeLevel)
    {
        gunName = "Rifle";
        this.upgradeLevel = upgradeLevel;
        switch (upgradeLevel)
        {
            //Base stats after buying
            case 0:
                damage = 2f;
                bulletSpeed = 25f;
                fireRate = 3f;
                magSize = 15;
                currentMagSize = 15;
                reloadTime = 4f;
                upgradeCost = 20;
                break;

            //Upgrade 1
            case 1:
                damage = 3f;
                bulletSpeed = 25f;
                fireRate = 3.5f;
                magSize = 17;
                currentMagSize = 17;
                reloadTime = 3.5f;
                upgradeCost = 40;
                break;

            //Upgrade 2
            case 2:
                damage = 3f;
                bulletSpeed = 25f;
                fireRate = 4f;
                magSize = 17;
                currentMagSize = 17;
                reloadTime = 3.5f;
                upgradeCost = 80;
                break;

            //Upgrade 3
            case 3:
                damage = 4f;
                bulletSpeed = 30f;
                fireRate = 4.5f;
                magSize = 20;
                currentMagSize = 20;
                reloadTime = 3f;
                upgradeCost = 0;
                break;
        }
    }

    public AssaultRifle(int upgradeLevel, Sprite sprite)
    {
        gunName = "Rifle";
        gunSprite = sprite;
        this.upgradeLevel = upgradeLevel;
        switch (upgradeLevel)
        {
            //Base stats after buying
            case 0:
                damage = 2f;
                bulletSpeed = 25f;
                fireRate = 3f;
                magSize = 15;
                currentMagSize = 15;
                reloadTime = 4f;
                upgradeCost = 20;
                break;

            //Upgrade 1
            case 1:
                damage = 3f;
                bulletSpeed = 25f;
                fireRate = 3.5f;
                magSize = 17;
                currentMagSize = 17;
                reloadTime = 3.5f;
                upgradeCost = 40;
                break;

            //Upgrade 2
            case 2:
                damage = 3f;
                bulletSpeed = 25f;
                fireRate = 4f;
                magSize = 17;
                currentMagSize = 17;
                reloadTime = 3.5f;
                upgradeCost = 80;
                break;

            //Upgrade 3
            case 3:
                damage = 4f;
                bulletSpeed = 30f;
                fireRate = 4.5f;
                magSize = 20;
                currentMagSize = 20;
                reloadTime = 3f;
                upgradeCost = 0;
                break;
        }
    }
}
