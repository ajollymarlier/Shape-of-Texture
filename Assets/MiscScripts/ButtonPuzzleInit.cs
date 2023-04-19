using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

#if UNITY_EDITOR
    using UnityEditor;
    using System.Net;
#endif

public class ButtonPuzzleInit : MonoBehaviour
{
    public GameObject[] orderedButtons;
    public GameObject[] otherButtons;
    private int i;
    private bool inProgress;
    private FMODUnity.StudioEventEmitter emitter;

    public GameObject[] doorlightShaders;
    public Shader greenShader;

    public MistakeManager mistakeManager;

    // Start is called before the first frame update
    void Start()
    {
        i=0;
        inProgress = false;
        emitter = gameObject.GetComponent<FMODUnity.StudioEventEmitter>();
        greenShader = Shader.Find("Shader Graphs/GreenLightDoor");
        if (mistakeManager == null)
        {
            mistakeManager = GameObject.Find("MistakeManager").GetComponent<MistakeManager>();
        }
    }

    // Update is called once per frame
    public void init()
    {
        orderedButtons[0].GetComponent<FMODUnity.StudioEventEmitter>().Play();
        inProgress = true;
        foreach (GameObject button in orderedButtons)
            button.GetComponent<Pressable>().Enable();
        foreach (GameObject button in otherButtons)
            button.GetComponent<Pressable>().Enable();
    }

    public void handleButtonPress(GameObject pressedButton){
        if (inProgress){
            if (i < orderedButtons.Length - 1 && pressedButton == orderedButtons[i]){
                pressedButton.transform.GetChild(2).GetComponent<Light>().intensity = 0;
                pressedButton.transform.GetComponent<FMODUnity.StudioEventEmitter>().Stop();
                i += 1;

                orderedButtons[i].GetComponent<FMODUnity.StudioEventEmitter>().Play();
            
            } 
            // Wrong button
            else if (pressedButton != orderedButtons[i])
            {
                pressedButton.GetComponent<FMODUnity.StudioEventEmitter>().Stop();
                orderedButtons[i].GetComponent<FMODUnity.StudioEventEmitter>().Stop();

                // Wrong sound
                FMODUnity.RuntimeManager.PlayOneShot("event:/GLOBAL events/SFX_Failure", transform.position);

                // Turn lights back on
                for (int j=0; j < i; j++){
                    orderedButtons[j].transform.GetChild(2).GetComponent<Light>().intensity = 100;
                }

                i = 0;
                orderedButtons[0].GetComponent<FMODUnity.StudioEventEmitter>().Play();

                foreach (GameObject button in orderedButtons)
                    button.GetComponent<Pressable>().Unpress();
                
                pressedButton.GetComponent<Pressable>().Unpress();

                mistakeManager.MakeMistake();
            }
            else{
                inProgress = false;
                pressedButton.transform.GetChild(2).GetComponent<Light>().intensity = 0;
                pressedButton.transform.GetComponent<FMODUnity.StudioEventEmitter>().Stop();

                //Turn door lights back on
                GameObject[] doorLights = GameObject.FindGameObjectsWithTag("Door Light");
                for (int j=0; j < doorLights.Length; j++){
                    doorLights[j].GetComponent<Light>().intensity = 40;
                }

                //Turn Button Lights Off
                GameObject[] buttonLights = GameObject.FindGameObjectsWithTag("Button Light");
                for (int j=0; j < buttonLights.Length; j++){
                    buttonLights[j].GetComponent<Light>().intensity = 0;
                }

                GameObject leftDoor = transform.Find("Left Door").gameObject;
                leftDoor.SetActive(false);
                // Play sound
                FMODUnity.RuntimeManager.PlayOneShot("event:/Medical Bay Intro Scene/SFX_Doors_open", transform.position);

                // Stop emitter
                if (emitter != null)
                    emitter.EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

                foreach (GameObject button in orderedButtons)
                    button.GetComponent<Pressable>().Disable();

                foreach (GameObject doorlight in doorlightShaders)
                    doorlight.GetComponent<Renderer>().material.shader = greenShader;
            }
        }
    }
}
