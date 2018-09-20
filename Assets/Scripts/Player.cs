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

    //Serialized Fields
    [SerializeField]
    private float baseDamageAmount, damageModifier, projectileSpeed;

    [SerializeField]
    private Rigidbody2D projectile;

    //Variables

    // Use this for initialization
    private void Start ()
    {
		
	}
	
	// Update is called once per frame
	private void Update ()
    {
		
	}

    //Attacks when an enemy is in range
    private void Attack()
    {
        Rigidbody2D projectileClone;
        projectileClone = Instantiate(projectile, transform.position + transform.right, transform.rotation) as Rigidbody2D;
        projectileClone.velocity = transform.TransformDirection(Vector2.right * projectileSpeed);
    }
}
