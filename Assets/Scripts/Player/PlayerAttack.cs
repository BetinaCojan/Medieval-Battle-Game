using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerControls controls;
    public Animator animator;

    public GameObject damagePlayer;
    public Transform damagePlace;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        controls = new PlayerControls();
        controls.Enable();
        controls.Land.Attack.performed += ctx => Damage();
    }

    void Start()
    {
        controls.Enable();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Damage()
    {
        audioManager.PlaySFX(audioManager.Attack);
        animator.SetTrigger("attack");
        Instantiate(damagePlayer, damagePlace.position, damagePlayer.transform.rotation);
    }
}
