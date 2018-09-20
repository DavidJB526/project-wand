using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Maintains camera focus on the player (with editor-accessible offset)
// ((Possible feature)) Can de-parent and then refocus when transitioning between levels

public class CameraController : MonoBehaviour {

    //Serialize Field
    [SerializeField]
    private Transform playerPosition;
    [SerializeField]
    private float xOffset;
    private float yOffset;
    //Fields
    private Transform cameraPosition;

	// Use this for initialization
	void Start ()
    {
        cameraPosition = this.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void SetCameraAndOffset(float XOffset, float YOffset)
    {

    }
}
