using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private FMOD.Studio.EventInstance pressInstance;
    private FMOD.Studio.EventInstance hoverInstance;
    public Button yourButton;

	private void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
        pressInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Main Menu Sounds/SFX_Button_click");
        hoverInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Main Menu Sounds/SFX_Button_hover");
    }

    void TaskOnClick(){
		Debug.Log ("You have clicked the button!");
        pressInstance.start();
        stopsound();
	}

    public void OnPointerEnter(PointerEventData eventData){
        Debug.Log("Enter!");
        startsound();
    }

    public void OnPointerExit(PointerEventData eventData){
        Debug.Log("Exit!");
        hoverInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void startsound(){
        hoverInstance.start();
    }

    public void stopsound(){
        hoverInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
