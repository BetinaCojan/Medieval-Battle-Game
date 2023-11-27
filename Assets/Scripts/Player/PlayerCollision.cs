using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCollision : MonoBehaviour
{
    AudioManager audioManager;
    int vibrations;

    public CheckPoint checkPoint;
    public List<CheckPoint> checkpoints = new List<CheckPoint>();
    private void Awake()
    {
        vibrations = PlayerPrefs.GetInt("Vibrations", 0);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gameObject.GetComponent<Renderer>().enabled = true;
        GetComponent<Animator>().SetLayerWeight(1, 0);
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }
    private void Start()
    {
        CheckPoint[] checkpointArray = GameObject.FindObjectsOfType<CheckPoint>();
        foreach (CheckPoint checkpoint in checkpointArray)
        {
            checkpoints.Add(checkpoint);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.transform.tag == "Enemies" || collision.transform.tag == "EnemiesStrong")
        {
            HealthManager.health--;
            if (HealthManager.health <= 0)
            {
                Vibrate();
                audioManager.PlaySFX(audioManager.GameOver);
                Time.timeScale = 0;
                PlayerManager.isGameOver = true;
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                StartCoroutine(GetHurt());
            }
        }

        IEnumerator GetHurt()
        {
            Vibrate();
            Physics2D.IgnoreLayerCollision(6,8);
            audioManager.PlaySFX(audioManager.loseHearth);
            GetComponent<Animator>().SetLayerWeight(1, 1);
            yield return new WaitForSeconds(1);
            GetComponent<Animator>().SetLayerWeight(1, 0);
            Physics2D.IgnoreLayerCollision(6, 8, false);
        }

        if (collision.transform.tag == "Death")
        {
            Vibrate();
            audioManager.PlaySFX(audioManager.GameOver);
            Time.timeScale = 0;
            PlayerManager.isGameOver = true;
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
    public void Pay()
    {
        Vector2 lastCheckpointPos2D = new Vector2(PlayerManager.lastCheckPointPos.x, PlayerManager.lastCheckPointPos.y);

        foreach (CheckPoint checkpoint in checkpoints)
        {
            checkpoint.resetAnimation();

            Vector2 checkpointPos2D = new Vector2(checkpoint.transform.position.x, checkpoint.transform.position.y);
            if (checkpointPos2D == lastCheckpointPos2D)
            {
                checkpoint.checkpointAnimation.Play("checkpoint-animations");
                audioManager.PlaySFX(audioManager.Checkpoint);
            }
        }

        HealthManager.health = 3;
        PlayerManager.isGameOver = false;
        gameObject.GetComponent<Renderer>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").transform.position = PlayerManager.lastCheckPointPos;
    }

    public void Vibrate()
    {
        if (vibrations == 1)
        {
            Handheld.Vibrate();
        }
    }
}
