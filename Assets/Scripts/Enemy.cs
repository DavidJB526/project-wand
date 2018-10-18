using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    //TODO: make all of these fields with get; and set; rather than just a public variable
    private float currentHealth;

    [HideInInspector]
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            UpdateHealthBar();
        }
    }

    [SerializeField]
    private float MaxHealth;

    public int goldAmount;

    public enum Weakness { Fire, Plant, Water, None };
    
    public Weakness enemyWeakness;

    //components
    [SerializeField]
    private Slider healthSlider;

    //methods
    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    //updates health bar whenever the enemy's health changes to reflect the change
    private void UpdateHealthBar()
    {
        float healthRatio = currentHealth/ MaxHealth;
        healthSlider.value = healthRatio;
    }
}
