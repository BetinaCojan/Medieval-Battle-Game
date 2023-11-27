using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemBlue : MonoBehaviour
{
    Transform target;
    public Transform borderCheck;
    public int golemHP = 100;
    public Animator animator;
    public Slider golemHealthBar;
    public GameObject infoGolem;

    public static bool golemAttacking = false;

    public GameObject damageGolem;
    public Transform damagePlace;

    AudioManager audioManager;


    public float interval = 0.5f; // The time interval in seconds between function calls
    private float timer = 0.5f; // Stopwatch to count the elapsed time

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        golemHealthBar.value = golemHP;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Physics2D.IgnoreCollision(target.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }


    void Update()
    {

        if (animator.GetBool("isAttacking"))
        {
            golemAttacking = true;
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                Damage();
                timer = 0f;
            }
        }
        else
        {
            golemAttacking = false;
        }
        

        if (target.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(0.3f, 0.3f);
            infoGolem.transform.localScale = new Vector2(0.03f, 0.03f);
        }
        else
        {
            transform.localScale = new Vector2(-0.3f, 0.3f);
            infoGolem.transform.transform.localScale = new Vector2(-0.03f, 0.03f);
        }
    }

    void Damage()
    {
        Instantiate(damageGolem, damagePlace.position, damageGolem.transform.rotation);
    }
    public void TakeDamage(int damageAmount)
    {
        golemHP -= damageAmount;
        golemHealthBar.value = golemHP;
        if(golemHP > 0)
        {
            audioManager.PlaySFX(audioManager.zombieHurt);
            animator.SetTrigger("damage");
            animator.SetBool("isAttacking", false);
        }
        else
        {
            audioManager.PlaySFX(audioManager.zombieDeath);
            animator.SetTrigger("death");
            animator.SetBool("isAttacking", false);
            GetComponent<CapsuleCollider2D>().enabled = false;
            this.enabled = false;
            infoGolem.SetActive(false);
        }
    }
}
