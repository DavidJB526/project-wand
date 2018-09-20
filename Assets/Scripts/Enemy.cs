using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    float health, goldAmount;

    enum Weakness { Fire, Plant, Water };

    private void Update()
    {
        if (health <= 0)
        {            
            Destroy(gameObject);
        }
    }

    private void TakeDamage(float amount)
    {
        Debug.Log(health);
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
