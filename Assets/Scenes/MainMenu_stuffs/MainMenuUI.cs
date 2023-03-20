using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour{

    public void PlayTheGame ()
	{
        PauseMenu.GamePaused = false;
		SceneManager.LoadScene(1);
		GameObject.Find("Play_Game_Button").GetComponent<MenuButtonSFX>().stopsound();
	}

	public void QuitGame ()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
