using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            audioManager.PlaySFX(audioManager.Coins);
            PlayerManager.numberOfCoins++;
            PlayerPrefs.SetInt("NumberOfCoins", PlayerManager.numberOfCoins);
            Destroy(gameObject);
        }
    }
}
