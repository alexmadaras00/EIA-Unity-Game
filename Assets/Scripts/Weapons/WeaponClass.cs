using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass
{
    //Info for game and app
    public string gunName;
    public int upgradeLevel;
    public int upgradeCost;

    //Weapon Stats for game
    public Sprite gunSprite;
    public Sprite bulletSprite;
    public float damage;
    public float bulletSpeed;
    public float fireRate;
    public int magSize;
    public int currentMagSize;
    public float reloadTime;    
    public bool isReloaded = true;
}
