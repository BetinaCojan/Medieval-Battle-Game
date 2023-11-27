using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class finishFlag : MonoBehaviour
{
    public int nextSceneLoad;
    public GameObject GameWinPanel;
    public GameObject CongratulationsPanel;

    public GameObject loadingScreenPanel;
    public Slider loadingBar;

    AudioManager audioManager;

    public void Awake()
    {
        GameWinPanel.SetActive(false);
        CongratulationsPanel.SetActive(false);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            audioManager.PlaySFX(audioManager.gameWin);

            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                // If the whole game is completed
                StartCoroutine(finishGame());
            }
            else
            {
                // If only the current level is completed
                StartCoroutine(finishLevel());
                if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                }
            }
        }
    }

    private IEnumerator finishLevel()
    {
        Time.timeScale = .001f;
        yield return new WaitForSeconds(.0005f);
        Time.timeScale = 0f;
        GameWinPanel.SetActive(true);
    }

    private IEnumerator finishGame()
    {
        Time.timeScale = .001f;
        yield return new WaitForSeconds(.0005f);
        Time.timeScale = 0f;
        CongratulationsPanel.SetActive(true);
    }

        public void NextLevel(int levelIndex)
        {
            StartCoroutine(LoadSceneAsynchronously(levelIndex));
        }

        IEnumerator LoadSceneAsynchronously(int levelIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
            loadingScreenPanel.SetActive(true);
            while (!operation.isDone)
            {
                loadingBar.value = operation.progress;
                yield return null;
            }
        }
}
