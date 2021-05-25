using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SceneChange : MonoBehaviour
{
    public GameObject menuUI;

    

    

    //Test

    //Login Buttons and Fields
    public GameObject loginStuff;
    public InputField usernameText;
    public InputField passwordText;
    public Button loginButton;
    public Button registerButton;
    public Button skipLogin;

    //Register Buttons and Fields
    public GameObject registerStuff;
    public InputField newUsernameText;
    public InputField newPasswordText;
    public InputField newConfirmPasswordText;
    public InputField newEmailText;
    public InputField newConfirmEmailText;
    public Button registerNewUserButton;
    public Button goBack;

    //Post Login Buttons and stuff
    public GameObject postLoginStuff;
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;
    public Button logoutButton;

    //Settings Buttons and stuff
    public GameObject settingsUI;
    public Button settingsBackButton;
    public Toggle soundToggle;
    public Toggle musicToggle;

    public string passwordAttempt;
    public string loggedUser;
    public int loggedID;
    public bool loginSucessful = false;
    
    public int userGold;
    public int pistolLevel = 5;
    public int smgLevel = 5;
    public int arLevel = 5;
    public int sniperLevel = 5;

    public bool pistolAdded = false;
    public bool smgAdded = false;
    public bool arAdded = false;
    public bool sniperAdded = false;

    public bool weaponsAdded = false;
    public bool needToCheckWeapons = false;

    public string BaseAPI = "http://localhost:2020";

    private void Start()
    {
        BaseAPI = "http://localhost:2020";
    }

    private void Update()
    {
        if (musicToggle.isOn)
            PlayerPrefs.SetInt("music", 1);
        else
            PlayerPrefs.SetInt("music", 0);

        MuteSound();

        if (needToCheckWeapons)
        {
            string jsonstring = JsonUtility.ToJson(new UserInfoWithID(loggedID));
            StartCoroutine(CheckWeaponsFromIDPostRequest((BaseAPI + "/weapons"), jsonstring));
            Debug.Log("Checked");
            CheckWeaponLevels();

            needToCheckWeapons = false;
        }
    }

    public void RegisterUser()
    {
        loggedUser               = newUsernameText.text.ToString();
        string username          = newUsernameText.text.ToString();
        string password          = newPasswordText.text.ToString();
        string email             = newEmailText.text;
        string confirmedEmail    = newConfirmEmailText.text;
        string confirmedPassword = newConfirmPasswordText.text.ToString();


        if(password != null && email != null && password == confirmedPassword && email == confirmedEmail)
        {
            string jsonstring = JsonUtility.ToJson(new NewPlayerInfo(username, password));
            Debug.Log(jsonstring);
            StartCoroutine(RegisterPostRequest(BaseAPI + "/signup", jsonstring));
        }
    }

    public void LoginUser()
    {
        loggedUser = usernameText.text.ToString();
        string username = usernameText.text.ToString();
        string password = passwordText.text.ToString();

        string jsonstring = JsonUtility.ToJson(new LoginUser(username, password));
        StartCoroutine(LoginPostRequest((BaseAPI + "/unityLogin"), jsonstring));
    }

    public void GetUserID()
    {
        string username = usernameText.text.ToString();
        string jsonstring = JsonUtility.ToJson(new UserInfo(username));
        StartCoroutine(UserIDPostRequest((BaseAPI + "/userID"), jsonstring));
    }

    public void CheckWeaponLevels()
    {
        string jsonstring = JsonUtility.ToJson(new WeaponLevel(loggedID, "Pistol"));
        StartCoroutine(PistolLevelPostRequest((BaseAPI + "/weaponLevel"), jsonstring));

        string jsonstring2 = JsonUtility.ToJson(new WeaponLevel(loggedID, "SMG"));
        StartCoroutine(SMGLevelPostRequest((BaseAPI + "/weaponLevel"), jsonstring2));

        string jsonstring3 = JsonUtility.ToJson(new WeaponLevel(loggedID, "Rifle"));
        StartCoroutine(ARLevelPostRequest((BaseAPI + "/weaponLevel"), jsonstring3));

        string jsonstring4 = JsonUtility.ToJson(new WeaponLevel(loggedID, "Sniper"));
        StartCoroutine(SniperLevelPostRequest((BaseAPI + "/weaponLevel"), jsonstring4));
    }

    IEnumerator RegisterPostRequest(string url, string json)
    {
        UnityWebRequest webRequest = new UnityWebRequest(url, "POST");

        byte[] jsonbyte = new System.Text.UTF8Encoding().GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(jsonbyte);
        webRequest.downloadHandler = new DownloadHandlerBuffer();

        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();


        if(webRequest.downloadHandler.text == "")
        {
            //GetUserID();
        }


        if (webRequest.isNetworkError)
        {
            Debug.Log(webRequest.error);
            
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
        }
    }


    IEnumerator LoginPostRequest(string url, string json)
    {
        UnityWebRequest webRequest = new UnityWebRequest(url, "POST");

        byte[] jsonbyte = new System.Text.UTF8Encoding().GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(jsonbyte);
        webRequest.downloadHandler = new DownloadHandlerBuffer();

        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if(webRequest.downloadHandler.text == "")
        {
            loginSucessful = false;
        }
        else
        {
            needToCheckWeapons = true;
            loginSucessful = true;
            loginStuff.SetActive(false);
            postLoginStuff.SetActive(true);

            string username = loggedUser;
            string jsonstring = JsonUtility.ToJson(new UserInfo(username));
            StartCoroutine(UserIDPostRequest((BaseAPI + "/userID"), jsonstring));

            if(loggedID != 0)
            {
                string jsonstring2 = JsonUtility.ToJson(new UserInfoWithID(loggedID));
                StartCoroutine(CheckWeaponsFromIDPostRequest((BaseAPI + "/weapons"), jsonstring2));
            }

            string jsonstring3 = JsonUtility.ToJson(new UserInfo(loggedUser));
            StartCoroutine(UserGoldPostRequest(BaseAPI + "/userGold", jsonstring3));



        }

        if (webRequest.isNetworkError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
        }
    }

    IEnumerator UserGoldPostRequest(string url, string json)
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
            userGold = int.Parse(webRequest.downloadHandler.text);
            Debug.Log(webRequest.downloadHandler.text);
        }
    }

    IEnumerator UserIDPostRequest(string url, string json)
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
            loggedID = int.Parse(webRequest.downloadHandler.text);
            Debug.Log(loggedID);
        }
    }

    IEnumerator CheckWeaponsFromIDPostRequest(string url, string json)
    {
        UnityWebRequest webRequest = new UnityWebRequest(url, "POST");

        byte[] jsonbyte = new System.Text.UTF8Encoding().GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(jsonbyte);
        webRequest.downloadHandler = new DownloadHandlerBuffer();

        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        Debug.Log(webRequest.downloadHandler.text);
        if (webRequest.downloadHandler.text == "")
        {
            Debug.Log("empty");
            //Adding Pistol
            Pistol pistol = new Pistol(0);
            string jsonstring = JsonUtility.ToJson(new AddCharacterWeapons(loggedID, pistol.gunName, pistol.upgradeCost));
            StartCoroutine(AddWeaponPostRequest((BaseAPI + "/addweapons"), jsonstring));

            //Adding SMG
            SubmachineGun smg = new SubmachineGun(0);
            string jsonstring2 = JsonUtility.ToJson(new AddCharacterWeapons(loggedID, smg.gunName, smg.upgradeCost));
            StartCoroutine(AddWeaponPostRequest((BaseAPI + "/addweapons"), jsonstring2));

            //Adding Assault Rifle
            AssaultRifle ar = new AssaultRifle(0);
            string jsonstring3 = JsonUtility.ToJson(new AddCharacterWeapons(loggedID, ar.gunName, ar.upgradeCost));
            StartCoroutine(AddWeaponPostRequest((BaseAPI + "/addweapons"), jsonstring3));

            //Adding Sniper
            Sniper sniper = new Sniper(0);
            string jsonstring4 = JsonUtility.ToJson(new AddCharacterWeapons(loggedID, sniper.gunName, sniper.upgradeCost));
            StartCoroutine(AddWeaponPostRequest((BaseAPI + "/addweapons"), jsonstring4));

            weaponsAdded = true;

        }
        else
        {
            weaponsAdded = true;
        }
    }

    IEnumerator AddWeaponPostRequest(string url, string json)
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
            Debug.Log("Weapon Added");
        }
    }

    IEnumerator PistolLevelPostRequest(string url, string json)
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
            
        }

        if(webRequest.downloadHandler.text != null)
        {
            int.TryParse(webRequest.downloadHandler.text, out pistolLevel);
        }
    }

    IEnumerator SMGLevelPostRequest(string url, string json)
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
            
        }

        if (webRequest.downloadHandler.text != null)
        {
            int.TryParse(webRequest.downloadHandler.text, out smgLevel);
        }
    }

    IEnumerator ARLevelPostRequest(string url, string json)
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
            
        }

        if (webRequest.downloadHandler.text != null)
        {
            int.TryParse(webRequest.downloadHandler.text, out arLevel);
        }
    }

    IEnumerator SniperLevelPostRequest(string url, string json)
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
            
        }

        if (webRequest.downloadHandler.text != null)
        {
            int.TryParse(webRequest.downloadHandler.text, out sniperLevel);
        }
    }


    public void PlayScene()
    {
        PlayerPrefs.SetInt("money", userGold);
        PlayerPrefs.SetInt("pistolLevel", pistolLevel);
        PlayerPrefs.SetInt("sMLevel", smgLevel);
        PlayerPrefs.SetInt("aRLevel", arLevel);
        PlayerPrefs.SetInt("sniperLevel", sniperLevel);
        PlayerPrefs.SetInt("playerID", loggedID);
        if(soundToggle.isOn)
            PlayerPrefs.SetInt("sound", 1);
        else
            PlayerPrefs.SetInt("sound", 0);

        if(musicToggle.isOn)
            PlayerPrefs.SetInt("music", 1);
        else
            PlayerPrefs.SetInt("music", 0);

        PlayerPrefs.Save();
        SceneManager.LoadScene("Level");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitScene()
    {
        Application.Quit();
    }

    public void Settings()
    {
        settingsUI.SetActive(true);
        postLoginStuff.SetActive(false);
    }

    public void BackToMainMenu()
    {
        postLoginStuff.SetActive(true);
        settingsUI.SetActive(false);
    }

    public void RegisterNewUser()
    {
        loginStuff.SetActive(false);
        registerStuff.SetActive(true);
    }

    private void MuteSound()
    {
        if(!soundToggle.isOn)
        {
            loginButton.GetComponent<AudioSource>().mute = true;
            registerButton.GetComponent<AudioSource>().mute = true;
            goBack.GetComponent<AudioSource>().mute = true;
            registerNewUserButton.GetComponent<AudioSource>().mute = true;
            skipLogin.GetComponent<AudioSource>().mute = true;
            playButton.GetComponent<AudioSource>().mute = true;
            settingsButton.GetComponent<AudioSource>().mute = true;
            quitButton.GetComponent<AudioSource>().mute = true;
            logoutButton.GetComponent<AudioSource>().mute = true;
            settingsBackButton.GetComponent<AudioSource>().mute = true;
            musicToggle.GetComponent<AudioSource>().mute = true;
            soundToggle.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            loginButton.GetComponent<AudioSource>().mute = false;
            registerButton.GetComponent<AudioSource>().mute = false;
            goBack.GetComponent<AudioSource>().mute = false;
            registerNewUserButton.GetComponent<AudioSource>().mute = false;
            skipLogin.GetComponent<AudioSource>().mute = false;
            playButton.GetComponent<AudioSource>().mute = false;
            settingsButton.GetComponent<AudioSource>().mute = false;
            quitButton.GetComponent<AudioSource>().mute = false;
            logoutButton.GetComponent<AudioSource>().mute = false;
            settingsBackButton.GetComponent<AudioSource>().mute = false;
            musicToggle.GetComponent<AudioSource>().mute = false;
            soundToggle.GetComponent<AudioSource>().mute = false;
        }
    }


}
