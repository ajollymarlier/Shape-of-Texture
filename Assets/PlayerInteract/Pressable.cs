using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;// Required when using Event data.
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
    using UnityEditor;
    using System.Net;
#endif

public class Pressable : Interactable, IPointerEnterHandler
{
    private FMOD.Studio.EventInstance instance;
    // private bool isPressed;

    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Medical Bay Intro Scene/SFX_Button_press");
        // isPressed = false;
    }

    public void OnPointerEnter(PointerEventData eventData){
        Debug.Log("Enter!");
    }

    public void OnPointerExit(PointerEventData eventData){
        Debug.Log("Exit!");
    }

    public override void Interact()
    {
        Debug.Log("Object is now pressed");

        if (SceneManager.GetActiveScene().name == "Tutorial"){
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.start();

            TutorialInit tutorialInit = gameObject.transform.parent.GetComponent<TutorialInit>();
            tutorialInit.handleObjPress(gameObject);
        }
        else if (SceneManager.GetActiveScene().name == "Final Medical Bay"){
            // Sound
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.start();
                
            ButtonPuzzleInit buttonPuzzleInit = gameObject.transform.parent.GetComponent<ButtonPuzzleInit>();
            buttonPuzzleInit.handleButtonPress(gameObject);
        } 
    }
}
