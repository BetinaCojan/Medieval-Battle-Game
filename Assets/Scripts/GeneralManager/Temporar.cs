using UnityEngine.SceneManagement;
using UnityEngine;

public class Temporar : MonoBehaviour
{
    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Menu");
    }

    public void Adauga100bani()
    {
        int coins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        PlayerPrefs.SetInt("NumberOfCoins", coins + 100);
        SceneManager.LoadScene("Menu");
    }

}
