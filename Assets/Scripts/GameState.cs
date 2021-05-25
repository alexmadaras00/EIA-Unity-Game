using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameState : MonoBehaviour
{
    private Gun gunScript;

    private int coins;
    public Text coinsColectedText;

    private int codes;
    public Text codesColectedText;

    private Button menuButton;
    private GameObject menuCanvas;

    public GameObject player;
    private Text gameOverText;

    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gunScript = GameObject.FindWithTag("Player").GetComponent<Gun>();
        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();
        menuCanvas = GameObject.Find("MenuBackground");
        menuCanvas.SetActive(false);

        if (GameObject.Find("GameOverText"))
            gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();

        coinsColectedText = GameObject.Find("CoinsText").GetComponent<Text>();
        codesColectedText = GameObject.Find("CodesText").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(RestartLevel());
        OnMenuButtonClick();
        coinsColectedText.text = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().coinsCollected.ToString();
        codesColectedText.text = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().codesCollected.ToString() + "/4";
    }

    private void OnMenuButtonClick()
    {    
        if(menuCanvas.activeSelf)
        {
            Time.timeScale = 0;
            if(Input.GetKeyDown(KeyCode.Escape))
                menuCanvas.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            if (Input.GetKeyDown(KeyCode.Escape))
                menuCanvas.SetActive(true);
        }
    }

    public void OnResumeButtonClick()
    {
        if (menuCanvas.activeSelf)
        {
            Time.timeScale = 0;  
            menuCanvas.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            menuCanvas.SetActive(true);
        }
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Level");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator RestartLevel()
    {
        if (player.GetComponent<PlayerMovement>().gameOver )
        {
            gameOverText.enabled = true;
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Level");
        }

    }

}
