using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectLock : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    private void Awake()
    {
        //Set screen size for Standalone
#if UNITY_STANDALONE
        Screen.SetResolution(500, 800, false);
        Screen.fullScreen = false;
#endif
    }
}
