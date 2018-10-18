using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    string  gameScreen;

    

	
    public void startButtonCLicked()
    {
        SceneManager.LoadScene(gameScreen);
    }
}
