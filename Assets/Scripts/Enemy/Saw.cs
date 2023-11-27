using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float speed = 0.8f;
    public float range = 3;
    float startingX;
    int dir = 1;

    void Start()
    {
        startingX = transform.position.x;
    }

    void FixedUpdate()
    {
        moveEnemies();
        if (transform.position.x < startingX || transform.position.x > startingX + range)
        {
            dir *= -1;
        }
    }

    public void moveEnemies()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime * dir, Space.World);
    }
}