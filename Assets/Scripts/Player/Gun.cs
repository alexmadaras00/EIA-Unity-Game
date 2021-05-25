using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Gun : MonoBehaviour
{
    private Transform playerTransform;
    private PlayerMovement playerMovement;
    public Image currentWeaponImage;

    //Different Weapons
    public Pistol pistol;
    public SubmachineGun smg;
    public AssaultRifle ar;
    public Sniper sniper;
    public Punch punch;

    //Different Weapon Sprites
    public Sprite punchSprite;
    public Sprite pistolSprite;
    public Sprite smgSprite;
    public Sprite arSprite;
    public Sprite sniperSprite;

    [SerializeField] float timeUntilNextShot = 0f;
    [SerializeField] float timeUntilNextPunch = 0f;
    

    public GameObject bullet;
    public GameObject spawnLocation;

    [SerializeField] bool reloading = false;

    public Text ammo;
    public Text gun;
    public Text upgradeText;

    private Animator anim;

    public float timeSinceLastShot = 2f;
    
    public WeaponClass selectedWeapon;


    // Start is called before the first frame update
    void Start()
    {

        punch = new Punch(punchSprite);

        spawnLocation = transform.Find("bulletSpawn").gameObject;
        currentWeaponImage = GameObject.Find("CurrentWeaponImage").GetComponent<Image>();
        playerTransform = GetComponent<Transform>();
        playerMovement = GetComponent<PlayerMovement>();
        gun = GameObject.Find("GunText").GetComponent<Text>();
        ammo = GameObject.Find("AmmoText").GetComponent<Text>();
        upgradeText = GameObject.Find("UpgradeText").GetComponent<Text>();
        anim = GetComponent<Animator>();


        //Check Pistol Upgrade Bought
        if (PlayerPrefs.GetInt("pistolLevel") == 3)
        {
            pistol = new Pistol(3, pistolSprite);
        }
        else if (PlayerPrefs.GetInt("pistolLevel") == 2)
        {
            pistol = new Pistol(2, pistolSprite);
        }
        else if (PlayerPrefs.GetInt("pistolLevel") == 1)
        {
            pistol = new Pistol(1, pistolSprite);
        }
        else
        {
            pistol = new Pistol(0, pistolSprite);
        }

        //Check Submachine Upgrades Bought
        if (PlayerPrefs.GetInt("sMLevel") == 3)
        {
            smg = new SubmachineGun(3, smgSprite);
        }
        else if (PlayerPrefs.GetInt("sMLevel") == 2)
        {
            smg = new SubmachineGun(2, smgSprite);
        }
        else if (PlayerPrefs.GetInt("sMLevel") == 1)
        {
            smg = new SubmachineGun(1, smgSprite);
        }
        else
        {
            smg = new SubmachineGun(0, smgSprite);
        }

        //Check Assault Rifle Upgrades Bought
        if (PlayerPrefs.GetInt("aRLevel") == 3)
        {
            ar = new AssaultRifle(3, arSprite);
        }
        else if (PlayerPrefs.GetInt("aRLevel") == 2)
        {
            ar = new AssaultRifle(2, arSprite);
        }
        else if (PlayerPrefs.GetInt("aRLevel") == 1)
        {
            ar = new AssaultRifle(1, arSprite);
        }
        else
        {
            ar = new AssaultRifle(0, arSprite);
        }

        //Check Sniper Upgrades Bought
        if (PlayerPrefs.GetInt("sniperLevel") == 3)
        {
            sniper = new Sniper(3, sniperSprite);
        }
        else if (PlayerPrefs.GetInt("sniperLevel") == 2)
        {
            sniper = new Sniper(2, sniperSprite);
        }
        else if (PlayerPrefs.GetInt("sniperLevel") == 1)
        {
            sniper = new Sniper(1, sniperSprite);
        }
        else
        {
            sniper = new Sniper(0, sniperSprite);
        }
        selectedWeapon = punch;
    }

    // Update is called once per frame
    void Update()
    {
        //Methods
        Combat();
        Upgrade();
        ChangeWeapon();
        StartCoroutine(ReloadCurrentWeapon());
        StartCoroutine(PunchTimer());

        //Animations
        if (timeSinceLastShot < 1)
            timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= 0.1f || selectedWeapon.currentMagSize == 0)
            anim.SetBool("isShooting", false);
        else
            anim.SetBool("isShooting", true);

        if (selectedWeapon == punch)
            anim.SetBool("punchSelected", true);
        else
            anim.SetBool("punchSelected", false);      

        


        //Weapon sprite
        if (currentWeaponImage)
        {
            currentWeaponImage.sprite = selectedWeapon.gunSprite;
            if (selectedWeapon == pistol)
                currentWeaponImage.rectTransform.sizeDelta = new Vector2(120, 90);
            else if (selectedWeapon == punch)
                currentWeaponImage.rectTransform.sizeDelta = new Vector2(130, 100);
            else
                currentWeaponImage.rectTransform.sizeDelta = new Vector2(240, 90);
        }
        
        //Weapon name
        gun.text = selectedWeapon.gunName;

        //Ammo amount
        if (selectedWeapon != punch)
            ammo.text = selectedWeapon.currentMagSize + "/" + selectedWeapon.magSize;
        else
            ammo.text = null;

        //Upgrade level
        if (selectedWeapon != punch && selectedWeapon.upgradeLevel > 0)
            upgradeText.text = "Upgrade " + selectedWeapon.upgradeLevel;
        else
            upgradeText.text = null;
    }

    private void Combat()
    {
        if (Input.GetMouseButton(0) && !(GetComponent<PlayerMovement>().gameOver))
        {
            //Punching
            if (selectedWeapon == punch)
            {
                if (timeUntilNextPunch <= 0)
                {
                    anim.SetBool("isPunching", true);
                    timeUntilNextPunch = 0.5f;
                }
            }
            timeSinceLastShot = 0f;

            //Shooting
            if (selectedWeapon != punch)
            {
                if(SceneManager.GetActiveScene().name != "Level")
                {
                    if (timeUntilNextShot <= 0 && selectedWeapon.currentMagSize > 0 && !reloading)
                    {
                        if (bullet)
                        {
                            Instantiate(bullet, spawnLocation.transform.position, spawnLocation.transform.rotation);
                            selectedWeapon.currentMagSize -= 1;
                            selectedWeapon.isReloaded = false;
                        }
                        timeUntilNextShot = 1;
                    }
                }
            }
        }
        //Timers
        timeUntilNextPunch -= Time.deltaTime;
        timeUntilNextShot -= Time.deltaTime * selectedWeapon.fireRate;

    }

    //Cheats for Upgrading Weapons
    private void Upgrade()
    {
        if (selectedWeapon != punch)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {

                if (selectedWeapon.upgradeLevel < 3)
                {
                    if (selectedWeapon == pistol)
                        pistol = new Pistol(selectedWeapon.upgradeLevel + 1, pistolSprite);
                    if (selectedWeapon == smg)
                        smg = new SubmachineGun(selectedWeapon.upgradeLevel + 1, smgSprite);
                    if (selectedWeapon == ar)
                        ar = new AssaultRifle(selectedWeapon.upgradeLevel + 1, arSprite);
                    if (selectedWeapon == sniper)
                        sniper = new Sniper(selectedWeapon.upgradeLevel + 1, sniperSprite);
                }
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                if (selectedWeapon.upgradeLevel > 0)
                {
                    if (selectedWeapon == pistol)
                        pistol = new Pistol(selectedWeapon.upgradeLevel - 1, pistolSprite);
                    if (selectedWeapon == smg)
                        smg = new SubmachineGun(selectedWeapon.upgradeLevel - 1, smgSprite);
                    if (selectedWeapon == ar)
                        ar = new AssaultRifle(selectedWeapon.upgradeLevel - 1, arSprite);
                    if (selectedWeapon == sniper)
                        sniper = new Sniper(selectedWeapon.upgradeLevel - 1, sniperSprite);
                }
            }
        }
    }

    //Choose Different Weapon
    private void ChangeWeapon()
    {
        if (SceneManager.GetActiveScene().name != "Level")
        {
            //Next Weapon
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (selectedWeapon.gunName)
                {
                    case "Pistol":
                        selectedWeapon = smg;
                        break;

                    case "SMG":
                        selectedWeapon = ar;
                        break;

                    case "Rifle":
                        selectedWeapon = sniper;
                        break;

                    case "Sniper":
                        selectedWeapon = punch;
                        break;

                    case "Knife":
                        selectedWeapon = pistol;
                        break;
                }
            }

            //Previous Weapon
            if (Input.GetKeyDown(KeyCode.Q))
            {
                switch (selectedWeapon.gunName)
                {
                    case "Pistol":
                        selectedWeapon = punch;
                        break;

                    case "SMG":
                        selectedWeapon = pistol;
                        break;

                    case "Rifle":
                        selectedWeapon = smg;
                        break;

                    case "Sniper":
                        selectedWeapon = ar;
                        break;

                    case "Knife":
                        selectedWeapon = sniper;
                        break;
                }
            }
        }
    }

    //Reloading Weapon
    private IEnumerator ReloadCurrentWeapon()
    {
        if (selectedWeapon != punch)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                reloading = true;
                yield return new WaitForSeconds(selectedWeapon.reloadTime);
                reloading = false;
                selectedWeapon.currentMagSize = selectedWeapon.magSize;
                selectedWeapon.isReloaded = true;
            }
        }
    }

    //Timer used in punch animation
    private IEnumerator PunchTimer()
    {
        if (anim.GetBool("isPunching"))
        {
            yield return new WaitForSeconds(0.3f);
            anim.SetBool("isPunching", false);
        }
    }
}
