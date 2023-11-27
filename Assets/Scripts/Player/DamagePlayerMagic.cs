using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerMagic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemies")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if(collision.tag == "Golem")
        {
            collision.GetComponent<GolemBlue>().TakeDamage(20);
            Destroy(gameObject);
        }
    }
}
