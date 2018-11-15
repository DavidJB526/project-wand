﻿using System.Collections;
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
            float damageTaken = currentHealth - value;
            launchDamageNumber(damageTaken);
            currentHealth = value;
            UpdateHealthBar();
        }
    }

    [Header("Gameplay Variables")]
    [SerializeField]
    private float MaxHealth;

    public int goldAmount;

    public enum Weakness { Fire, Plant, Water, None };
    
    public Weakness enemyWeakness;

    [Header("UI Variables")]

    [SerializeField]
    private float minimumTextSize;

    [SerializeField]
    private float maximumTextSize;

    [SerializeField]
    private float lowerDamageThreshold;

    [SerializeField]
    private float upperDamageThreshold;

    [SerializeField]
    private float textVelocity;

    [SerializeField]
    private float destructionTime;

    [SerializeField]
    private float textXNegativeDeviation;

    [SerializeField]
    private float textXPositiveDeviation;

    [Header("Component Fields")]

    //components
    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private Transform textSpawn;

    [SerializeField]
    private Canvas textCanvas;

    [SerializeField]
    private Text textPrefab;

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

    //fires a text element
    private void launchDamageNumber(float damageAmount)
    {
        var flyingText = Instantiate(textPrefab, textSpawn);
        flyingText.text = damageAmount.ToString();
        flyingText.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-textXNegativeDeviation, textXPositiveDeviation), textVelocity);
        Destroy(flyingText, destructionTime);
    }

    //function called to destroy the monster
    public void killEnemy()
    {
        textCanvas.transform.SetParent(null, true);
        Destroy(textCanvas.gameObject, 2);
        Destroy(this.gameObject);
    }
}
