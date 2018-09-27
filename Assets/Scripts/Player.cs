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

    //Fields
    private Rigidbody2D playerRigidBody;

    //Serialized Fields
    //<<<<<<< HEAD
    [SerializeField]
    private float baseDamageAmount,
        damageModifier,
        projectileSpeed,
        playerAttackSpeed,
        playerMovementSpeed;

    [SerializeField]
    private Rigidbody2D projectile;

    [SerializeField]
    private Text goldText;
//>>>>>>> de251bbc82be02a177a8e530696988bc59ae3535

    //Variables
    private bool enemyPresent;
    private Enemy enemy;
    private float goldCount;

    // Use this for initialization
    private void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        goldText.text = "";
	}
	
	// Update is called once per frame
	private void FixedUpdate ()
    {
        Walk();
        GoldCheck();
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Found Enemy");

        if (collider.CompareTag("Enemy"))
        {
            enemy = collider.transform.GetComponent<Enemy>();
        }
    }

    //Checks to see if there's a monster within range
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("Enemy"))
        {
            enemyPresent = true;
        }
    }

    //If the monster disappears, resets whether an enemy is present

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

    //Attacks when an enemy is in range
    private void Attack()
    {
        if (enemy != null)
        {
            Rigidbody2D projectileClone;
            projectileClone = Instantiate(projectile, transform.position + transform.right, transform.rotation) as Rigidbody2D;
            projectileClone.velocity = transform.TransformDirection(Vector2.right * projectileSpeed);
            enemy.TakeDamage(baseDamageAmount * damageModifier);
            goldCount += enemy.goldAmount;
        }

    }

    private void GoldCheck()
    {
        goldText.text = "Gold: " + goldCount.ToString();
    }
}
