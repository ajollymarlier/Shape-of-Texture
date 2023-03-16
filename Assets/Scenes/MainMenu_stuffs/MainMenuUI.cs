using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour{

    	public void PlayTheGame ()
	{
		SceneManager.LoadScene(1);
	}

	    public void QuitGame ()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
