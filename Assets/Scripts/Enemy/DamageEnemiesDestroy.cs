using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemiesDestroy : MonoBehaviour
{
    void Start()
    {
        DestroyDamage();
    }

    void DestroyDamage()
    {
        Destroy(gameObject, 0.3f);
    }


}
