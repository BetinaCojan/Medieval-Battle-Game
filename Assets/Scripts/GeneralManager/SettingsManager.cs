using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Image switchOFF;
    public Image switchON;
    int switchIndex;

    public Image switchOFFVibrations;
    public Image switchONVibrations;
    int switchIndexVibrations;

    public Image switchOffFPS;
    public Image switchOnFPS;
    int switchIndexFPS;
    void Awake()
    {
        switchIndex = PlayerPrefs.GetInt("SwitchControls", 0);
        Switch();

        switchIndexVibrations = PlayerPrefs.GetInt("Vibrations", 0);
        SwitchVibrations();

        switchIndexFPS = PlayerPrefs.GetInt("FPS", 1);
        FPS();
    }


    public void Switch()
    {
        if (switchIndex == 0)
        {
            switchOFF.gameObject.SetActive(true);
            switchON.gameObject.SetActive(false);
        }
        else if (switchIndex == 1)
        {
            switchOFF.gameObject.SetActive(false);
            switchON.gameObject.SetActive(true);
        }
    }

    public void buttonOFF()
    {
        switchOFF.gameObject.SetActive(false);
        switchON.gameObject.SetActive(true);
        PlayerPrefs.SetInt("SwitchControls", 1);
    }

    public void buttonON()
    {
        switchOFF.gameObject.SetActive(true);
        switchON.gameObject.SetActive(false);
        PlayerPrefs.SetInt("SwitchControls", 0);
    }

    public void SwitchVibrations()
    {
        if (switchIndexVibrations == 0)
        {
            switchOFFVibrations.gameObject.SetActive(true);
            switchONVibrations.gameObject.SetActive(false);
        }
        else if (switchIndexVibrations == 1)
        {
            switchOFFVibrations.gameObject.SetActive(false);
            switchONVibrations.gameObject.SetActive(true);
        }
    }

    public void buttonOFFVibrations()
    {
        switchOFFVibrations.gameObject.SetActive(false);
        switchONVibrations.gameObject.SetActive(true);
        PlayerPrefs.SetInt("Vibrations", 1);
    }

    public void buttonONVibrations()
    {
        switchOFFVibrations.gameObject.SetActive(true);
        switchONVibrations.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Vibrations", 0);
    }

    public void FPS()
    {
        if (switchIndexFPS == 1)
        {
            switchOffFPS.gameObject.SetActive(false);
            switchOnFPS.gameObject.SetActive(true);
        }
        else if (switchIndexFPS == 0)
        {
            switchOffFPS.gameObject.SetActive(true);
            switchOnFPS.gameObject.SetActive(false);
        }
    }

    public void buttonOffFPS()
    {
        switchOffFPS.gameObject.SetActive(false);
        switchOnFPS.gameObject.SetActive(true);
        PlayerPrefs.SetInt("FPS", 1);
    }

    public void buttonOnFPS()
    {
        switchOffFPS.gameObject.SetActive(true);
        switchOnFPS.gameObject.SetActive(false);
        PlayerPrefs.SetInt("FPS", 0);
    }
}
