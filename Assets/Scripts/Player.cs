using System.Collections;
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

public class Player : MonoBehaviour {

    #region Variable References

    //Fields
    private Rigidbody2D playerRigidBody;

    //Serialized Fields
    [SerializeField]
    private float 
        idleBaseDamage, //simple idle attack damage
        idleDamageModifier, //multiplicative modifier to idle attack
        projectileSpeed, //TODO: Remove projectile-based artifacts
        playerAttackSpeed, //time between idle attacks
        elementalBaseDamage, //base damage for active elemental attacks
        elementalMultiplier, //multiplicative bonus given for using the correct elemental attack
        playerMovementSpeed, //horizontal movement speed
        moveCreepWaitTime; //wait time for move creep (very short)

    //Serialized object references
    [SerializeField]
    private Text goldText;
    [SerializeField]
    private AudioSource wizardWalking;

    //Variables
    private bool enemyPresent;
    private bool canWalk;
    private Enemy enemy;
    private Animator playerAnimator;
    private bool isWalking = false;

    //TODO: Transform these into get and set fields rather than just a public variable. This is the hack functional way to do it
    //public variables
    public int goldCount;

    //enumerated variables
    public enum Weakness { Fire, Plant, Water, None };

    #endregion

    // Use this for initialization
    private void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        UpdateGoldText();
	}
    private void Update()
    {
        checkIfEnemyDestroyed();
    }

    //FixedUpdate is called once per physics calculation
    private void FixedUpdate()
    {
        Walk();
    }

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

    //Checks to see if there's a monster within range and renews this per each frame the monster is within range
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy"))
    //    {
    //        enemyPresent = true;
    //    }
    //}

    //checks to see if the enemy has been destroyed
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
            if(isWalking == true)
            {

                wizardWalking.Play();
                isWalking = false;
            }
            else
            {

            }
            
        }
        else
        {
            playerRigidBody.velocity = new Vector2(0,0);
           wizardWalking.Stop();
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
            Destroy(enemy.gameObject); //destroys the monster
            isWalking = true;
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

    //Called from elemental attack button clicks, performs an elemental attack
    //TODO: Add Cooldowns and visual cues for the cooldowns to these attacks
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
