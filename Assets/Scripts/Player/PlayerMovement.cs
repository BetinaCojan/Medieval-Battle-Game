using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerControls controls;

    float direction = 0;
    public float speed = 400;
    public bool isFacingRight = true;

    public float jumpForce = 5;
    bool isGrounded;
    int numberOfJumps = 0;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public Rigidbody2D playerRB;
    public Animator animator;

    public bool isOnPlatform;
    public Rigidbody2D platformRb;

    AudioManager audioManager;

    private void Awake() 
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        controls = new PlayerControls();
        controls.Enable();
        
        controls.Land.Move.performed += ctx => 
        {
            direction = ctx.ReadValue<float>();
        };

        controls.Land.Jump.performed += ctx => Jump();
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

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        if (isOnPlatform)
        {
            playerRB.velocity = new Vector3(direction * speed + platformRb.velocity.x, playerRB.velocity.y, 0f);
        }
        else
        {
            playerRB.velocity = new Vector3(direction * speed, playerRB.velocity.y, 0f);
        }
        animator.SetFloat("speed", Mathf.Abs(direction));
        if((isFacingRight && direction < 0) || (!isFacingRight && direction > 0))
        {
            Flip();
        }
    }

    void Flip() 
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void Jump()
    {
        if(isGrounded)
        {
            audioManager.PlaySFX(audioManager.Jump);
            numberOfJumps = 0;
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            numberOfJumps ++;
        }
        else 
        {
            if(numberOfJumps == 1)
            {
                audioManager.PlaySFX(audioManager.Jump);
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                numberOfJumps ++;
            }
        }
        
    }
}
