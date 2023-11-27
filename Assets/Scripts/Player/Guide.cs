using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;

    public void Start()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
    {
        panel1.SetActive(true);
        Time.timeScale = 0;
    }
    }

    public void Next()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }

    public void Close()
    {
        panel2.SetActive(false);
        Time.timeScale = 1;
    }
}
