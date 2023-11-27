using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuEvents : MonoBehaviour
{
    private int FpsGame;
    public void Awake()
    {
        Time.timeScale = 1;
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Update()
    {
        // Set FPS
        FpsGame = PlayerPrefs.GetInt("FPS", 1);
        if (FpsGame == 0)
        {
            Application.targetFrameRate = 30;
        }
        else if (FpsGame == 1)
        {
            Application.targetFrameRate = 60;
        }
    }
}
