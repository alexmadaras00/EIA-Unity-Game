using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{

    public int gold;
    private Text coinsText;
    private Text errorText;

    public AudioSource audio;
    public AudioSource cantBuyAudio;
    public AudioSource music;

    public Button goToBoss;

    //Default error messages
    private string needMoreMoneyText;
    private string buyPreviousUpgradeText;
    private string alreadyhaveUpgradeText;

    public Pistol pistol;
    public SubmachineGun smg;
    public AssaultRifle ar;
    public Sniper sniper;

    //Pistol upgrades
    public Button pistolUpgrade1;
    public Image pistolUpgrade1Image;
    public Button pistolUpgrade2;
    public Image pistolUpgrade2Image;
    public Button pistolUpgrade3;
    public Image pistolUpgrade3Image;

    //SubMachine upgrades
    public Button subMachineUpgrade1;
    public Image subMachineUpgrade1Image;
    public Button subMachineUpgrade2;
    public Image subMachineUpgrade2Image;
    public Button subMachineUpgrade3;
    public Image subMachineUpgrade3Image;

    //Assault Rifle upgrades
    public Button assaultRifleUpgrade1;
    public Image assaultRifleUpgrade1Image;
    public Button assaultRifleUpgrade2;
    public Image assaultRifleUpgrade2Image;
    public Button assaultRifleUpgrade3;
    public Image assaultRifleUpgrade3Image;

    //Sniper Upgrades
    public Button sniperUpgrade1;
    public Image sniperUpgrade1Image;
    public Button sniperUpgrade2;
    public Image sniperUpgrade2Image;
    public Button sniperUpgrade3;
    public Image sniperUpgrade3Image;

    // Start is called before the first frame update
    void Start()
    {
        music.Play();
        needMoreMoneyText = "You don't have enough coins.";
        buyPreviousUpgradeText = "You need to buy the previous upgrade.";
        alreadyhaveUpgradeText = "You already have this upgrade.";

        gold = PlayerPrefs.GetInt("money");

        //Current Pistol Upgrade
        switch(PlayerPrefs.GetInt("pistolLevel"))
        {
            case 3:
                pistol = new Pistol(3);
                break;

            case 2:
                pistol = new Pistol(2);
                break;

            case 1:
                pistol = new Pistol(1);
                break;

            case 0:
                pistol = new Pistol(0);
                break;
        }

        //Current Submachine Gun Level
        switch (PlayerPrefs.GetInt("sMLevel"))
        {
            case 3:
                smg = new SubmachineGun(3);
                break;

            case 2:
                smg = new SubmachineGun(2);
                break;

            case 1:
                smg = new SubmachineGun(1);
                break;

            case 0:
                smg = new SubmachineGun(0);
                break;
        }

        //Current Assault Rifle Level
        switch (PlayerPrefs.GetInt("aRLevel"))
        {
            case 3:
                ar = new AssaultRifle(3);
                break;

            case 2:
                ar = new AssaultRifle(2);
                break;

            case 1:
                ar = new AssaultRifle(1);
                break;

            case 0:
                ar = new AssaultRifle(0);
                break;
        }

        //Current Sniper Level
        switch (PlayerPrefs.GetInt("sniperLevel"))
        {
            case 3:
                sniper = new Sniper(3);
                break;

            case 2:
                sniper = new Sniper(2);
                break;

            case 1:
                sniper = new Sniper(1);
                break;

            case 0:
                sniper = new Sniper(0);
                break;
        }

        coinsText = GameObject.Find("CoinsText").GetComponent<Text>();
        errorText = GameObject.Find("ErrorText").GetComponent<Text>();

        //Pistol upgrade button listeners
        pistolUpgrade1.onClick.AddListener(BuyPistolUpgrade1);
        pistolUpgrade2.onClick.AddListener(BuyPistolUpgrade2);
        pistolUpgrade3.onClick.AddListener(BuyPistolUpgrade3);


        //SMG upgrade button listeners
        subMachineUpgrade1.onClick.AddListener(BuySubMachineUpgrade1);
        subMachineUpgrade2.onClick.AddListener(BuySubMachineUpgrade2);
        subMachineUpgrade3.onClick.AddListener(BuySubMachineUpgrade3);


        //AR upgrade button listeners
        assaultRifleUpgrade1.onClick.AddListener(BuyAssaultRifleUpgrade1);
        assaultRifleUpgrade2.onClick.AddListener(BuyAssaultRifleUpgrade2);
        assaultRifleUpgrade3.onClick.AddListener(BuyAssaultRifleUpgrade3);


        //Sniper upgrade button listeners
        sniperUpgrade1.onClick.AddListener(BuySniperRifleUpgrade1);
        sniperUpgrade2.onClick.AddListener(BuySniperRifleUpgrade2);
        sniperUpgrade3.onClick.AddListener(BuySniperRifleUpgrade3);

    }

    // Update is called once per frame
    void Update()
    {
        MuteSound();
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Pistol: Damage " + pistol.damage + ", Upgrade Level " + pistol.upgradeLevel + ", Mag Size " + pistol.magSize + ", Upgrade Cost " + pistol.upgradeCost);
            Debug.Log("SMG: Damage " + smg.damage + ", Upgrade Level " + smg.upgradeLevel + ", Mag Size " + smg.magSize + ", Upgrade Cost " + smg.upgradeCost);
            Debug.Log("AR: Damage " + ar.damage + ", Upgrade Level " + ar.upgradeLevel + ", Mag Size " + ar.magSize + ", Upgrade Cost " + ar.upgradeCost);
            Debug.Log("Sniper: Damage " + sniper.damage + ", Upgrade Level " + sniper.upgradeLevel + ", Mag Size " + sniper.magSize + ", Upgrade Cost " + sniper.upgradeCost);
        }

        StartCoroutine(ErrorTextShowing());
        
        coinsText.text = gold.ToString();
    }

    private void BuyPistolUpgrade1()
    {
        //If you have the right level for the upgrade
        if (pistol.upgradeLevel == 0)
        {
            //If you have enough money for the upgrade
            if (gold >= pistol.upgradeCost)
            {
                gold -= pistol.upgradeCost;
                pistolUpgrade1Image.enabled = true;
                audio.Play();
                pistol = new Pistol(1);
            }
            //If you don't have enough money
            else
            {
                errorText.text = needMoreMoneyText;
                cantBuyAudio.Play();
                errorText.enabled = true;
            }
        }
        //If you already have a better upgrade
        else if(pistol.upgradeCost > 0)
        {
            errorText.text = alreadyhaveUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }
    }

    private void BuyPistolUpgrade2()
    {
        //If you have the right level for the upgrade
        if (pistol.upgradeLevel == 1)
        {
            //If you have enough money for the upgrade
            if (gold >= pistol.upgradeCost)
            {
                gold -= pistol.upgradeCost;
                pistolUpgrade2Image.enabled = true;
                audio.Play();
                pistol = new Pistol(2);
            }                
            //If you don't have enough money
            else
            {
                errorText.text = needMoreMoneyText;
                cantBuyAudio.Play();
                errorText.enabled = true;
            }
        }

        //If already have a better upgrade
        else if(pistol.upgradeLevel > 1)
        {
            errorText.text = alreadyhaveUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }
           
        //If you don't have the previous upgrade
        else
        {
            errorText.text = buyPreviousUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }
    }

    private void BuyPistolUpgrade3()
    {
        //If you have the right level for the upgrade
        if (pistol.upgradeLevel == 2)
        {
            //If you have enough money for the upgrade
            if (gold >= pistol.upgradeCost)
            {
                gold -= pistol.upgradeCost;
                pistolUpgrade3Image.enabled = true;
                audio.Play();
                pistol = new Pistol(3);
            }
            //If you don't have enough money
            else
            {
                errorText.text = needMoreMoneyText;
                cantBuyAudio.Play();
                errorText.enabled = true;
            }
        }

        //If you don't have the previous upgrade
        else if (pistol.upgradeLevel < 2)
        {
            errorText.text = buyPreviousUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }
    }

    private void BuySubMachineUpgrade1()
    {
        //If you have the right level for the upgrade
        if (smg.upgradeLevel == 0)
        {
            //If you have enough money for the upgrade
            if (gold >= smg.upgradeCost)
            {
                gold -= smg.upgradeCost;
                subMachineUpgrade1Image.enabled = true;
                audio.Play();
                smg = new SubmachineGun(1);
            }
            //If you don't have enough money
            else
            {
                errorText.text = needMoreMoneyText;
                cantBuyAudio.Play();
                errorText.enabled = true;
            }
        }
        //If you already have a better upgrade
        else if (smg.upgradeCost > 0)
        {
            errorText.text = alreadyhaveUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }
    }

    private void BuySubMachineUpgrade2()
    {
        //If you have the right level for the upgrade
        if (smg.upgradeLevel == 1)
        {
            //If you have enough money for the upgrade
            if (gold >= smg.upgradeCost)
            {
                gold -= smg.upgradeCost;
                subMachineUpgrade2Image.enabled = true;
                audio.Play();
                smg = new SubmachineGun(2);
            }
            //If you don't have enough money
            else
            {
                errorText.text = needMoreMoneyText;
                cantBuyAudio.Play();
                errorText.enabled = true;
            }
        }

        //If already have a better upgrade
        else if (smg.upgradeLevel > 1)
        {
            errorText.text = alreadyhaveUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }

        //If you don't have the previous upgrade
        else
        {
            errorText.text = buyPreviousUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }
    }

    private void BuySubMachineUpgrade3()
    {
        //If you have the right level for the upgrade
        if (smg.upgradeLevel == 2)
        {
            //If you have enough money for the upgrade
            if (gold >= smg.upgradeCost)
            {
                gold -= smg.upgradeCost;
                subMachineUpgrade3Image.enabled = true;
                audio.Play();
                smg = new SubmachineGun(3);
            }
            //If you don't have enough money
            else
            {
                errorText.text = needMoreMoneyText;
                cantBuyAudio.Play();
                errorText.enabled = true;
            }
        }

        //If you don't have the previous upgrade
        else if (smg.upgradeLevel < 2)
        {
            errorText.text = buyPreviousUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }
    }

    private void BuyAssaultRifleUpgrade1()
    {
        //If you have the right level for the upgrade
        if (ar.upgradeLevel == 0)
        {
            //If you have enough money for the upgrade
            if (gold >= ar.upgradeCost)
            {
                gold -= ar.upgradeCost;
                assaultRifleUpgrade1Image.enabled = true;
                audio.Play();
                ar = new AssaultRifle(1);
            }
            //If you don't have enough money
            else
            {
                errorText.text = needMoreMoneyText;
                cantBuyAudio.Play();
                errorText.enabled = true;
            }
        }
        //If you already have a better upgrade
        else if (ar.upgradeCost > 0)
        {
            errorText.text = alreadyhaveUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }
    }

    private void BuyAssaultRifleUpgrade2()
    {
        //If you have the right level for the upgrade
        if (ar.upgradeLevel == 1)
        {
            //If you have enough money for the upgrade
            if (gold >= ar.upgradeCost)
            {
                gold -= ar.upgradeCost;
                assaultRifleUpgrade2Image.enabled = true;
                audio.Play();
                ar = new AssaultRifle(2);
            }
            //If you don't have enough money
            else
            {
                errorText.text = needMoreMoneyText;
                cantBuyAudio.Play();
                errorText.enabled = true;
            }
        }

        //If already have a better upgrade
        else if (ar.upgradeLevel > 1)
        {
            errorText.text = alreadyhaveUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }

        //If you don't have the previous upgrade
        else
        {
            errorText.text = buyPreviousUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }
    }

    private void BuyAssaultRifleUpgrade3()
    {
        //If you have the right level for the upgrade
        if (ar.upgradeLevel == 2)
        {
            //If you have enough money for the upgrade
            if (gold >= ar.upgradeCost)
            {
                gold -= ar.upgradeCost;
                assaultRifleUpgrade3Image.enabled = true;
                audio.Play();
                ar = new AssaultRifle(3);
            }
            //If you don't have enough money
            else
            {
                errorText.text = needMoreMoneyText;
                cantBuyAudio.Play();
                errorText.enabled = true;
            }
        }

        //If you don't have the previous upgrade
        else if (ar.upgradeLevel < 2)
        {
            errorText.text = buyPreviousUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }
    }

    private void BuySniperRifleUpgrade1()
    {
        //If you have the right level for the upgrade
        if (sniper.upgradeLevel == 0)
        {
            //If you have enough money for the upgrade
            if (gold >= sniper.upgradeCost)
            {
                gold -= sniper.upgradeCost;
                sniperUpgrade1Image.enabled = true;
                audio.Play();
                sniper = new Sniper(1);
            }
            //If you don't have enough money
            else
            {
                errorText.text = needMoreMoneyText;
                cantBuyAudio.Play();
                errorText.enabled = true;
            }
        }
        //If you already have a better upgrade
        else if (sniper.upgradeCost > 0)
        {
            errorText.text = alreadyhaveUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }
    }

    private void BuySniperRifleUpgrade2()
    {
        //If you have the right level for the upgrade
        if (sniper.upgradeLevel == 1)
        {
            //If you have enough money for the upgrade
            if (gold >= sniper.upgradeCost)
            {
                gold -= sniper.upgradeCost;
                sniperUpgrade2Image.enabled = true;
                audio.Play();
                sniper = new Sniper(2);
            }
            //If you don't have enough money
            else
            {
                errorText.text = needMoreMoneyText;
                cantBuyAudio.Play();
                errorText.enabled = true;
            }
        }

        //If already have a better upgrade
        else if (sniper.upgradeLevel > 1)
        {
            errorText.text = alreadyhaveUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }

        //If you don't have the previous upgrade
        else
        {
            errorText.text = buyPreviousUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }
    }

    private void BuySniperRifleUpgrade3()
    {
        //If you have the right level for the upgrade
        if (sniper.upgradeLevel == 2)
        {
            //If you have enough money for the upgrade
            if (gold >= sniper.upgradeCost)
            {
                gold -= sniper.upgradeCost;
                sniperUpgrade3Image.enabled = true;
                audio.Play();
                sniper = new Sniper(3);
            }
            //If you don't have enough money
            else
            {
                errorText.text = needMoreMoneyText;
                cantBuyAudio.Play();
                errorText.enabled = true;
            }
        }

        //If you don't have the previous upgrade
        else if (sniper.upgradeLevel < 2)
        {
            errorText.text = buyPreviousUpgradeText;
            cantBuyAudio.Play();
            errorText.enabled = true;
        }
    }

    IEnumerator ErrorTextShowing()
    {
        if(errorText.enabled)
        {
            yield return new WaitForSeconds(2);
            errorText.enabled = false;
        }    
    }

    private void MuteSound()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            pistolUpgrade1.GetComponent<AudioSource>().mute = true;
            pistolUpgrade2.GetComponent<AudioSource>().mute = true;
            pistolUpgrade3.GetComponent<AudioSource>().mute = true;

            subMachineUpgrade1.GetComponent<AudioSource>().mute = true;
            subMachineUpgrade2.GetComponent<AudioSource>().mute = true;
            subMachineUpgrade3.GetComponent<AudioSource>().mute = true;

            assaultRifleUpgrade1.GetComponent<AudioSource>().mute = true;
            assaultRifleUpgrade2.GetComponent<AudioSource>().mute = true;
            assaultRifleUpgrade3.GetComponent<AudioSource>().mute = true;

            sniperUpgrade1.GetComponent<AudioSource>().mute = true;
            sniperUpgrade2.GetComponent<AudioSource>().mute = true;
            sniperUpgrade3.GetComponent<AudioSource>().mute = true;

            goToBoss.GetComponent<AudioSource>().mute = true;

            audio.mute = true;
            cantBuyAudio.mute = true;
            music.mute = true;
        }
        else
        {
            pistolUpgrade1.GetComponent<AudioSource>().mute = false;
            pistolUpgrade2.GetComponent<AudioSource>().mute = false;
            pistolUpgrade3.GetComponent<AudioSource>().mute = false;

            subMachineUpgrade1.GetComponent<AudioSource>().mute = false;
            subMachineUpgrade2.GetComponent<AudioSource>().mute = false;
            subMachineUpgrade3.GetComponent<AudioSource>().mute = false;

            assaultRifleUpgrade1.GetComponent<AudioSource>().mute = false;
            assaultRifleUpgrade2.GetComponent<AudioSource>().mute = false;
            assaultRifleUpgrade3.GetComponent<AudioSource>().mute = false;

            sniperUpgrade1.GetComponent<AudioSource>().mute = false;
            sniperUpgrade2.GetComponent<AudioSource>().mute = false;
            sniperUpgrade3.GetComponent<AudioSource>().mute = false;

            goToBoss.GetComponent<AudioSource>().mute = false;


            audio.mute = false;
            cantBuyAudio.mute = false;
            music.mute = false;
        }
    }
}
