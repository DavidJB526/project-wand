using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    [SerializeField]
<<<<<<< HEAD
    private float baseDamageAmount, damageModifier, projectileSpeed;

    [SerializeField]
    private Rigidbody2D projectile;
=======
    private float playerAttackSpeed;
    [SerializeField]
    private float baseDamageAmount;
    [SerializeField]
    private float damageModifier;
    [SerializeField]
    private float playerMovementSpeed;
>>>>>>> de251bbc82be02a177a8e530696988bc59ae3535

    //Variables
    private bool enemyPresent;

    // Use this for initialization
    private void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	private void Update ()
    {
        Walk();
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
        Rigidbody2D projectileClone;
        projectileClone = Instantiate(projectile, transform.position + transform.right, transform.rotation) as Rigidbody2D;
        projectileClone.velocity = transform.TransformDirection(Vector2.right * projectileSpeed);
    }
}
