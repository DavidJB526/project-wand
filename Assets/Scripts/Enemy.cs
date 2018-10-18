using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    public float health;

    public int goldAmount;

    public enum Weakness { Fire, Plant, Water, None };

    //TODO: make this a field with get; and set;
    public Weakness enemyWeakness;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
