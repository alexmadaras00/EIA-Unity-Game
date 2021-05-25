using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGun : WeaponClass
{

    public SubmachineGun()
    {
        
    }

    public SubmachineGun(int upgradeLevel)
    {
        gunName = "SMG";
        this.upgradeLevel = upgradeLevel;
        switch (upgradeLevel)
        {
            //Base stats after buying
            case 0:
                damage = 1f;
                bulletSpeed = 25f;
                fireRate = 4f;
                magSize = 20;
                currentMagSize = 20;
                reloadTime = 3.5f;
                upgradeCost = 15;
                break;

            //Upgrade 1
            case 1:
                damage = 2f;
                bulletSpeed = 25f;
                fireRate = 4.5f;
                magSize = 22;
                currentMagSize = 22;
                reloadTime = 3f;
                upgradeCost = 30;
                break;

            //Upgrade 2
            case 2:
                damage = 2f;
                bulletSpeed = 25f;
                fireRate = 4.5f;
                magSize = 22;
                currentMagSize = 22;
                reloadTime = 3f;
                upgradeCost = 60;
                break;

            //Upgrade 3
            case 3:
                damage = 3f;
                bulletSpeed = 30f;
                fireRate = 5f;
                magSize = 24;
                currentMagSize = 24;
                reloadTime = 2.5f;
                upgradeCost = 0;
                break;
        }
    }

    public SubmachineGun(int upgradeLevel, Sprite sprite)
    {
        gunName = "SMG";
        gunSprite = sprite;
        this.upgradeLevel = upgradeLevel;
        switch (upgradeLevel)
        {
            //Base stats after buying
            case 0:
                damage = 1f;
                bulletSpeed = 25f;
                fireRate = 4f;
                magSize = 20;
                currentMagSize = 20;
                reloadTime = 3.5f;
                upgradeCost = 15;
                break;

            //Upgrade 1
            case 1:
                damage = 2f;
                bulletSpeed = 25f;
                fireRate = 4.5f;
                magSize = 22;
                currentMagSize = 22;
                reloadTime = 3f;
                upgradeCost = 30;
                break;

            //Upgrade 2
            case 2:
                damage = 2f;
                bulletSpeed = 25f;
                fireRate = 4.5f;
                magSize = 22;
                currentMagSize = 22;
                reloadTime = 3f;
                upgradeCost = 60;
                break;

            //Upgrade 3
            case 3:
                damage = 3f;
                bulletSpeed = 30f;
                fireRate = 5f;
                magSize = 24;
                currentMagSize = 24;
                reloadTime = 2.5f;
                upgradeCost = 0;
                break;
        }
    }
}
