using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform posA, posB;
    public float speed;
    Vector3 targetPos;

    PlayerMovement playerMovement;
    Rigidbody2D rb;
    Vector3 moveDirection;

    public float waitDuration;


    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        targetPos = posB.position;
        DirectionCalculate();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, posA.position) < 0.05f)
        {
            targetPos = posB.position;
            moveDirection = Vector3.zero;
            StartCoroutine(WaitNextPoint());

        }

        if (Vector2.Distance(transform.position, posB.position) < 0.05f)
        {
            targetPos = posA.position;
            moveDirection = Vector3.zero;
            StartCoroutine(WaitNextPoint());
        }
    }

    IEnumerator WaitNextPoint()
    {
        yield return new WaitForSeconds(waitDuration);
        DirectionCalculate();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    void DirectionCalculate()
    {
        moveDirection = (targetPos - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement.isOnPlatform = true;
            playerMovement.platformRb = rb;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement.isOnPlatform = false;
        }
    }
}