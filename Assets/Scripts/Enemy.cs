using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    public float health;

    public int goldAmount;

    enum Weakness { Fire, Plant, Water, None };

    [SerializeField]
    Weakness enemyWeakness;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
