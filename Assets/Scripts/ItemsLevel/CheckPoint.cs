using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Animator checkpointAnimation;
    private static Transform lastCheckPoint;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        checkpointAnimation = GetComponent<Animator>();
        SetAnimation("checkpoint-animations_idle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (transform != lastCheckPoint)
            {
                PlayerManager.lastCheckPointPos = transform.position;
                SetAnimation("checkpoint-animations");
                lastCheckPoint = transform;
                audioManager.PlaySFX(audioManager.Checkpoint);
                lastCheckPoint = transform;

                // Set the other checkpoints to IDLE state
                CheckPoint[] allCheckPoints = FindObjectsOfType<CheckPoint>();
                foreach (CheckPoint checkpoint in allCheckPoints)
                {
                    if (checkpoint != this)
                    {
                        checkpoint.SetAnimation("checkpoint-animations_idle");
                    }
                }
            }
        }
    }

    public void SetAnimation(string animationName)
    {
        checkpointAnimation.Play(animationName);
    }

private void OnEnable()
    {
        // Check if this is the last activated checkpoint and activate the activation animation if necessary
        if (transform == lastCheckPoint)
        {
            checkpointAnimation.Play("checkpoint-animations");
            audioManager.PlaySFX(audioManager.Checkpoint);
        }
    }

    public void resetAnimation()
    {
        checkpointAnimation.Play("checkpoint-animations_idle");
    }
}