using UnityEngine;

public class Sound : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Click()
    {
        audioManager.PlaySFX(audioManager.Click);
    }
}
