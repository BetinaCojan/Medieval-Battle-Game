using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;
using TMPro.Examples;

public class PlayerManager : MonoBehaviour
{
    PlayerCollision thePlayer;

    public static bool isGameOver;
    public GameObject gameOverScreen;
    public GameObject pauseMenuScreen;

    public GameObject mobileControlsLeft;
    public GameObject mobileControlsRight;
    private int switchControls;

    private int FpsGame;

    public static Vector2 lastCheckPointPos = new Vector2(-1,-1);

    public static int numberOfCoins;
    public TextMeshProUGUI coinsText;

    public Button PayMoney;
    public static int payMoneyTax;
    public TextMeshProUGUI payMoneyTaxText;

    public CinemachineVirtualCamera VCam;
    public GameObject[] playerPrefabs;
    int characterIndex;

    // Timer
    public Slider timerSlider;
    public float gameTime;
    public float initTime;

    private void Awake() 
    {
        payMoneyTax = 2;
        Time.timeScale = 1;
        lastCheckPointPos = new Vector2(-1, -1);
        characterIndex = PlayerPrefs.GetInt("SelectCharacter", 0);
        GameObject player = Instantiate(playerPrefabs[characterIndex], lastCheckPointPos, Quaternion.identity);
        VCam.m_Follow = player.transform;
        numberOfCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        isGameOver = false;
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPos;

        // Check Control
        switchControls = PlayerPrefs.GetInt("SwitchControls", 0);
        if (switchControls == 0)
        {
            mobileControlsLeft.SetActive(true);
            mobileControlsRight.SetActive(false);
        }
        else if (switchControls == 1)
        {
            mobileControlsLeft.SetActive(false);
            mobileControlsRight.SetActive(true);
        }

        // Set FPS
        FpsGame = PlayerPrefs.GetInt("FPS", 1);
        if(FpsGame == 0)
        {
            Application.targetFrameRate = 30;
        }
        else if(FpsGame == 1)
        {
            Application.targetFrameRate = 60;
        }
    }

    private void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollision>();
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
    }

    void Update()
    {
        coinsText.text = numberOfCoins.ToString();
        if(isGameOver) 
        {
            gameOverScreen.SetActive(true);
        }

        // Check if there is enough money and display the buy button
        if (payMoneyTax <= numberOfCoins)
        {
            PayMoney.interactable = true;
        }
        else
        {
            PayMoney.interactable = false;
        }

        // Timer
        gameTime -= Time.deltaTime;
        timerSlider.value = gameTime;

        if (gameTime <= 0)
        {
            isGameOver = true;
        } 
    }

    public void ThePay()
    {
        Physics2D.IgnoreLayerCollision(6, 8, false);
        numberOfCoins = numberOfCoins - payMoneyTax;
        PlayerPrefs.SetInt("NumberOfCoins", numberOfCoins);
        thePlayer.Pay();
        payMoneyTax = payMoneyTax + 2;
        payMoneyTaxText.text = "Revive: " + payMoneyTax.ToString();
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;
        gameTime = initTime;
        timerSlider.value = initTime;
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
    }

    public void MenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

}

