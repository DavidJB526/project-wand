using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //editor-accessible variables
    [SerializeField]
    private Transform playerTransform; //transform of the wizard

    [SerializeField]
    private float spawnDistanceInUnits;

    [SerializeField]
    private float enemyOffset;

    [SerializeField]
    private Enemy[] enemyPrefabs;

    [SerializeField]
    private SpriteRenderer[] backgroundSprites;

	// Use this for initialization
	void Start ()
    {
        setupInitialLevel();
	}
	
	// Update is called once per frame
	void Update ()
    {
        checkTileDistance();
	}

    void setupInitialLevel() //set down the first few tiles without monsters on them
    {
        Instantiate(backgroundSprites[Random.Range(0, backgroundSprites.Length)], this.transform.position, Quaternion.identity); //spawn the first tile on the player

        setNextSpawnPosition();

        Instantiate(backgroundSprites[Random.Range(0, backgroundSprites.Length)], this.transform.position, Quaternion.identity);

        setNextSpawnPosition();

        Instantiate(backgroundSprites[Random.Range(0, backgroundSprites.Length)], this.transform.position, Quaternion.identity);

        setNextSpawnPosition();

    }

    void checkTileDistance() //check to see if a new tile needs to be spawned
    {
        if ((transform.position.x - playerTransform.position.x) <= spawnDistanceInUnits * 2) //if the player is within range
        {
            Instantiate(backgroundSprites[Random.Range(0, backgroundSprites.Length)], this.transform.position, Quaternion.identity);

            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], new Vector3(transform.position.x, transform.position.y + enemyOffset, transform.position.z), Quaternion.identity);

            setNextSpawnPosition();
        }
    }

    void setNextSpawnPosition()
    {
        //move the transform to the next thing
        transform.position = new Vector3(transform.position.x + spawnDistanceInUnits, transform.position.y, transform.position.z);
    }
}
