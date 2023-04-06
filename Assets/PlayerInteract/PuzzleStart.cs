using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleStart : Interactable
{
    private FMOD.Studio.EventInstance pressInstance;
    private FMOD.Studio.EventInstance initInstance;
    // private bool isPressed;

    private bool triggered;
    private bool isPlaying;

    public GameObject textBox;

    private void Start()
    {
        pressInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Medical Bay Intro Scene/SFX_Button_press");
        initInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Medical Bay Intro Scene/Sequence Inizialized Log");
        // isPressed = false;
        triggered = false;
    }

    void Update()
    {
        if (isPlaying && PauseMenu.GamePaused){
            initInstance.setPaused(true);
        }
        else if (isPlaying && !PauseMenu.GamePaused){
            initInstance.setPaused(false);
        }
    }

    public override void Interact()
    {
        Debug.Log("Object is now pressed");
        // Sound
        // pressInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        // pressInstance.start();

        if(!triggered){
            // Play "Sequence initialized" and press sfx
            pressInstance.start();
            initInstance.start();

            GameObject[] doorLights = GameObject.FindGameObjectsWithTag("Door Light");
            for (int i=0; i < doorLights.Length; i++){
                doorLights[i].GetComponent<Light>().intensity = 0;
            }

            GameObject[] buttonLights = GameObject.FindGameObjectsWithTag("Button Light");

            for (int i=0; i < buttonLights.Length; i++){
                buttonLights[i].GetComponent<Light>().intensity = 100;
            }

            ButtonPuzzleInit buttonPuzzleInit = gameObject.transform.parent.gameObject.GetComponent<ButtonPuzzleInit>();
            buttonPuzzleInit.init();

            isPlaying = true;
            StartCoroutine(SubtitleSequenceInitialized());

            triggered = true;
        }
    }

    IEnumerator SubtitleSequenceInitialized() {
        // yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "Sequence initialized.";
        yield return new WaitForSeconds(2.5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        isPlaying = false;
    }
}
