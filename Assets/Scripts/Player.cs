﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Necessary functions of the player:
// - Acquire gold
// - Deal Damage to monsters
// - Modify said damage with an adjustable multiplier (for items)
// - Move
//  + Stop moving in the presence of a monster 
//      (forward trigger zone can be used for both this and to acquire the monster object for damage-dealing)
//  + Resume moving once the monster has been defeated
// - Deal elementally-modified damage upon input from a player

public class Player : MonoBehaviour
{

    #region Variable References

    //Fields
    private Rigidbody2D playerRigidBody;

    //Serialized Fields
    //[Header("Player Variables")]
    [SerializeField]
    private float
        idleBaseDamage, //simple idle attack damage
        idleDamageModifier, //multiplicative modifier to idle attack
        projectileSpeed, //TODO: Remove projectile-based artifacts
        playerAttackSpeed, //time between idle attacks
        elementalBaseDamage, //base damage for active elemental attacks
        elementalMultiplier, //multiplicative bonus given for using the correct elemental attack
        playerMovementSpeed; //horizontal movement speed

    //Store Variables
    [Header("Store Variables")]
    [SerializeField]
    private float startingPrice; //starting cost of upgrades
    [SerializeField]
    private float priceIncreaseRatio; //the increase of the per-purchase cost of upgrades
    [SerializeField]
    private string attackButtonBaseText; //text that appears in front of the price on the attack store button
    [SerializeField]
    private string speedButtonBaseText; //text that appears in front of the price on the attack speed store button
    [SerializeField]
    private float attackIncrease; //how much the idle attack power increases per purchase
    [SerializeField]
    private float speedIncrease; //how much the idle attack speed decreases per purchase

    private Text attackButtonText;
    private Text speedButtonText;
    private float attackCost;
    private float speedCost;


    //Serialized object references
    [Header("Object/Components")]
    [SerializeField]
    private Text goldText;
    [SerializeField]
    private Button AttackStoreButton;
    [SerializeField]
    private Button SpeedStoreButton;

    //Variables
    private bool enemyPresent;
    private bool canWalk;
    private Enemy enemy;
    private Animator playerAnimator;

    //TODO: Transform these into get and set fields rather than just a public variable. This is the hack functional way to do it
    //public variables
    [HideInInspector]
    public float goldCount;

    //enumerated variables
    public enum Weakness { Fire, Plant, Water, None };

    #endregion

    // Use this for initialization
    private void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        UpdateGoldText();
        setupStore();
	}
    private void Update()
    {
        checkIfEnemyDestroyed();
        updateStoreButtons();
    }

    //FixedUpdate is called once per physics calculation
    private void FixedUpdate()
    {
        Walk();
    }

    #region Character Movement + Attacks

    //assign new enemy when it enters player range
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Found Enemy");
            enemy = collider.transform.GetComponent<Enemy>();
            enemyPresent = true;
            StartCoroutine(IdleActiveCoroutine());
            //playerAnimator.SetBool("inCombat", true);
        }
    }

    //checks to see if the enemy has been destroyed each frame
    // Essentially acts as an "On trigger exit" event function that includes when the enemy object is destroyed
    private void checkIfEnemyDestroyed()
    {
        if (enemyPresent && !enemy)
        {
            enemyPresent = false;
        }
    }

    //Walks if unobstructed, stops if obstructed
    private void Walk()
    {
        if (!enemyPresent)
        {
            playerRigidBody.velocity = new Vector2(playerMovementSpeed, 0);
        }
        else
        {
            playerRigidBody.velocity = new Vector2(0,0);
        }
    }

    //function to check whether the enemy is dead and drops gold, used within the attack functions
    private void CheckForDeathAndReset()
    {
        //checks to see if the enemy is there AND has no health left
        if (enemy!=null && enemy.CurrentHealth <= 0)
        {
            goldCount += enemy.goldAmount; //adds to the player's gold count from the enemy's drop amount
            UpdateGoldText(); //updates the gold text
            enemy.killEnemy(); //destroys the monster
        }
    }

    //Attacks when an enemy is in range
    private void IdleAttack()
    {
        if (enemy != null)
        {
            //has enemy take damage on attack
            enemy.CurrentHealth -= (idleBaseDamage * idleDamageModifier);
            //check to make sure the enemy is/is not dead
            CheckForDeathAndReset();
        }
        //playerAnimator.SetBool("inCombat", false);
    }

    #endregion
    
    #region Store Functions
    
    //initializes store values
    private void setupStore()
    {
        //get text components from buttons
        attackButtonText =  AttackStoreButton.GetComponentInChildren<Text>();
        speedButtonText = SpeedStoreButton.GetComponentInChildren<Text>();

        //set starting prices
        attackCost = startingPrice;
        speedCost = startingPrice;

        //set initial text
        attackButtonText.text = attackButtonBaseText + attackCost + "g";
        speedButtonText.text = speedButtonBaseText + speedCost + "g";
    }

    //if the player doesn't have enough gold to buy an upgrade, disables interaction on that button
    private void updateStoreButtons()
    {
        //check for attack button
        if (goldCount >= attackCost)
        {
            AttackStoreButton.interactable = true;
        }
        else
        {
            AttackStoreButton.interactable = false;
        }

        //check for speed button
        if (goldCount >= speedCost && playerAttackSpeed != speedIncrease)
        {
            SpeedStoreButton.interactable = true;
        }
        else
        {
            SpeedStoreButton.interactable = false;
        }
    }

    //removes the requisite gold from the player's gold count, adjusts the stat, then increases the price of the upgrade and updates the button text
    public void AtkStoreButtonClicked()
    {
        goldCount -= attackCost; //subtract the gold
        attackCost = Mathf.Round(attackCost * priceIncreaseRatio); //increase the cost and round it to the nearest integer
        attackButtonText.text = attackButtonBaseText + attackCost + "g"; //update the button text
        idleBaseDamage += attackIncrease; //increase the player's idle attack

        UpdateGoldText(); //update the gold count text
    }

    public void SpeedStoreButtonClicked()
    {
        goldCount -= speedCost; //subtract the gold
        speedCost = Mathf.Round(speedCost * priceIncreaseRatio); //increase the cost
        speedButtonText.text = speedButtonBaseText + speedCost + "g"; //update the button text
        if (playerAttackSpeed >= speedIncrease)
        {
            playerAttackSpeed -= speedIncrease;
        }

        if (playerAttackSpeed == speedIncrease)
        {
            speedButtonText.text = "Maxed Out!";
        }

        UpdateGoldText(); //update the gold count text
    }

    #endregion


    //Called from elemental attack button clicks, performs an elemental attack
    //TODO: Add Cooldowns and visual cues for such cooldowns to these attacks
    #region Active Button Attacks

    public void FireAttack()
    {
        if (enemy != null)
        {
            if (enemy.enemyWeakness == Enemy.Weakness.Fire)
            {
                enemy.CurrentHealth -= (elementalBaseDamage * elementalMultiplier);
            }
            else
            {
                enemy.CurrentHealth -= elementalBaseDamage;
            }

            CheckForDeathAndReset();
        }
    }

    public void WaterAttack()
    {
        if (enemy != null)
        {
            if (enemy.enemyWeakness == Enemy.Weakness.Water)
            {
                enemy.CurrentHealth -= (elementalBaseDamage * elementalMultiplier);
            }
            else
            {
                enemy.CurrentHealth -= elementalBaseDamage;
            }

            CheckForDeathAndReset();
        }
    }

    public void PlantAttack()
    {
        if (enemy != null)
        {
            if (enemy.enemyWeakness == Enemy.Weakness.Plant)
            {
                enemy.CurrentHealth -= (elementalBaseDamage * elementalMultiplier);
            }
            else
            {
                enemy.CurrentHealth -= elementalBaseDamage;
            }

            CheckForDeathAndReset();
        }
    }

    #endregion

    //updates gold text UI
    private void UpdateGoldText()
    {
        goldText.text = "Gold: " + goldCount.ToString();
    }

    #region Coroutines

    //Idle Attack speed/damage Coroutine
    private IEnumerator IdleActiveCoroutine()
    {
        while (enemyPresent)
        {
            Debug.Log("Started Attack Coroutine");
            IdleAttack();
            yield return new WaitForSeconds(playerAttackSpeed);
        }
    }
    #endregion
}
