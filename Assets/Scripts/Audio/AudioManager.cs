using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip Checkpoint;
    public AudioClip Click;
    public AudioClip Coins;
    public AudioClip GameOver;
    public AudioClip Jump;
    public AudioClip Attack;
    public AudioClip LevelCompleted;
    public AudioClip GolemBlue;
    public AudioClip gameWin;
    public AudioClip zombieHurt;
    public AudioClip zombieDeath;
    public AudioClip loseHearth;

    [Header("---------- Background Music ----------")]
    public AudioClip BackgroundMenu;
    public AudioClip BackgroundLevels;

    public static AudioManager instance;
    private int tranzitionMusic = 5;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayBackgroundMusic();
        musicSource.clip = BackgroundMenu;
        musicSource.Play();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip newClip = null;

        if (scene.buildIndex >= 1 && scene.buildIndex <= 10)
        {
            newClip = BackgroundLevels;
        }
        else
        {
            newClip = BackgroundMenu;
        }

        StartCoroutine(TransitionMusic(newClip));
    }

    private IEnumerator TransitionMusic(AudioClip newClip)
    {
        if (musicSource.isPlaying)
        {
            while (musicSource.volume > 0)
            {
                musicSource.volume -= Time.deltaTime * tranzitionMusic;
                yield return null;
            }

            musicSource.Stop();
        }

        musicSource.clip = newClip;

        if (!musicSource.isPlaying)
        {
            musicSource.volume = 0;
            musicSource.Play();

            while (musicSource.volume < 1)
            {
                musicSource.volume += Time.deltaTime * tranzitionMusic;
                yield return null;
            }
        }
    }


    private void PlayBackgroundMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}