using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class StoreToGame : MonoBehaviour
{

    public bool goldSaved = false;
    public bool pistolLevelSaved = false;
    public bool smgLevelSaved = false;
    public bool arLevelSaved = false;
    public bool sniperLevelSaved = false;

    private Store store;


    public string BaseAPI = "http://localhost:2021";

    void Start()
    {
        BaseAPI = "http://localhost:2021";
        store = GameObject.Find("StoreScript").GetComponent<Store>();
    }

    void Update()
    {
            
    }


    public void SetUserGold()
    {
        string jsonstring = JsonUtility.ToJson(new SetCharacterGold(store.gold, PlayerPrefs.GetInt("playerID")));
        StartCoroutine(PostRequest(BaseAPI + "/addgold", jsonstring));
        goldSaved = true;
    }

    public void SetPistolLevel()
    {
        string jsonstring = JsonUtility.ToJson(new SetWeaponLevel(PlayerPrefs.GetInt("playerID"), "Pistol", PlayerPrefs.GetInt("pistolLevel")));
        StartCoroutine(PostRequest(BaseAPI + "/setWeaponLevel", jsonstring));
    }

    public void SetSMGLevel()
    {
        string jsonstring = JsonUtility.ToJson(new SetWeaponLevel(PlayerPrefs.GetInt("playerID"), "SMG", PlayerPrefs.GetInt("sMLevel")));
        StartCoroutine(PostRequest(BaseAPI + "/setWeaponLevel", jsonstring));
    }

    public void SetARLevel()
    {
        string jsonstring = JsonUtility.ToJson(new SetWeaponLevel(PlayerPrefs.GetInt("playerID"), "Rifle", PlayerPrefs.GetInt("aRLevel")));
        StartCoroutine(PostRequest(BaseAPI + "/setWeaponLevel", jsonstring));
    }

    public void SetSniperLevel()
    {
        string jsonstring = JsonUtility.ToJson(new SetWeaponLevel(PlayerPrefs.GetInt("playerID"), "Sniper", PlayerPrefs.GetInt("sniperLevel")));
        StartCoroutine(PostRequest(BaseAPI + "/setWeaponLevel", jsonstring));
    }



    IEnumerator PostRequest(string url, string json)
    {
        UnityWebRequest webRequest = new UnityWebRequest(url, "POST");

        byte[] jsonbyte = new System.Text.UTF8Encoding().GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(jsonbyte);
        webRequest.downloadHandler = new DownloadHandlerBuffer();

        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();


        if (webRequest.isNetworkError)
        {
            Debug.Log(webRequest.error);

        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
        }
    }


    public void GoToBoss()
    {
        PlayerPrefs.SetInt("money", store.gold);

        switch (store.smg.upgradeLevel)
        {
            case 3:
                PlayerPrefs.SetInt("sMLevel", 3);
                break;
            case 2:
                PlayerPrefs.SetInt("sMLevel", 2);
                break;
            case 1:
                PlayerPrefs.SetInt("sMLevel", 1);
                break;
            case 0:
                PlayerPrefs.SetInt("sMLevel", 0);
                break;
        }

        switch (store.pistol.upgradeLevel)
        {
            case 3:
                PlayerPrefs.SetInt("pistolLevel", 3);
                break;
            case 2:
                PlayerPrefs.SetInt("pistolLevel", 2);
                break;
            case 1:
                PlayerPrefs.SetInt("pistolLevel", 1);
                break;
            case 0:
                PlayerPrefs.SetInt("pistolLevel", 0);
                break;
        }


        switch (store.ar.upgradeLevel)
        {
            case 3:
                PlayerPrefs.SetInt("aRLevel", 3);
                break;
            case 2:
                PlayerPrefs.SetInt("aRLevel", 2);
                break;
            case 1:
                PlayerPrefs.SetInt("aRLevel", 1);
                break;
            case 0:
                PlayerPrefs.SetInt("aRLevel", 0);
                break;
        }

        switch (store.sniper.upgradeLevel)
        {
            case 3:
                PlayerPrefs.SetInt("sniperLevel", 3);
                break;
            case 2:
                PlayerPrefs.SetInt("sniperLevel", 2);
                break;
            case 1:
                PlayerPrefs.SetInt("sniperLevel", 1);
                break;
            case 0:
                PlayerPrefs.SetInt("sniperLevel", 0);
                break;
        }

        PlayerPrefs.Save();

        Debug.Log(PlayerPrefs.GetInt("pistolLevel"));
        Debug.Log(PlayerPrefs.GetInt("sMLevel"));
        Debug.Log(PlayerPrefs.GetInt("aRLevel"));
        Debug.Log(PlayerPrefs.GetInt("sniperLevel"));

        //saveCompleted = true;
        SetUserGold();
        SetPistolLevel();
        SetSMGLevel();
        SetARLevel();
        SetSniperLevel();    
        SceneManager.LoadScene("BossFight");
    }
}
