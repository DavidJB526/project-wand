using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    //Declare your item types

    enum ItemTypes { Potions, Spellbooks }

    //Make an array to hold your count for each item

    int[] items = new int[2];

    /*void AddItem(ItemType itemType, int Count);
    void RemoveItem(ItemType itemType, int Count);
    int CheckItemCount(ItemType itemType, int Count);
    bool InventorySystem.CheckIfWeHaveItem(ItemType itemType, int Count);
    ItemType[] TellMeItemsThatIHaveAtLeastOneOf();
    */

    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
