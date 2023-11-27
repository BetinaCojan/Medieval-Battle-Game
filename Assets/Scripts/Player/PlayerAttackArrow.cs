using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackArrow : MonoBehaviour
{
    PlayerControls controls;
    public Animator animator;

    public GameObject damagePlayer;
    public Transform damagePlace;
    public float force = 200;
    public static bool rightAttack;

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

        GameObject go = Instantiate(damagePlayer, damagePlace.position, damagePlayer.transform.rotation);

         if (!GetComponent<PlayerMovement>().isFacingRight)
        {
            go.GetComponent<Rigidbody2D>().AddForce(Vector2.left * force);
            go.GetComponent<SpriteRenderer>().flipX = true;
            rightAttack = false;
        }
        else
        { 
            go.GetComponent<Rigidbody2D>().AddForce(Vector2.right * force);
            go.GetComponent<SpriteRenderer>().flipX = false;
            rightAttack = true;
        }
        Destroy(go, 1.5f);
    }
}
