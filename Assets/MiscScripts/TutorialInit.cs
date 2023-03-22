using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialInit : MonoBehaviour
{
    public GameObject movingDoor;
    public bool isAudioLog;
    public string audioLogPath;
    public GameObject textBox;
    private FMODUnity.StudioEventEmitter emitter;

    private FMOD.Studio.EventInstance instance;

    private bool logPlaying;

    private void Start(){
        instance = FMODUnity.RuntimeManager.CreateInstance(audioLogPath);
        logPlaying = false;
    }

    public void handleObjPress(GameObject pressedObj){
        pressedObj.GetComponentInChildren<Light>().intensity = 0;
        movingDoor.transform.Translate(Vector3.right * 4);
        if (!isAudioLog){
            pressedObj.GetComponent<FMODUnity.StudioEventEmitter>().Stop();
        }
        

        if (isAudioLog){
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.start();
            logPlaying = true;
            StartCoroutine(SubtitleSequence());
        }   
    }

    void Update()
    {
        if (logPlaying && PauseMenu.GamePaused){
            instance.setPaused(true);
        }
        else if (logPlaying && !PauseMenu.GamePaused){
            instance.setPaused(false);
        }
    }

    IEnumerator SubtitleSequence() {
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "Attention, crew. As of today you all will be the sole crew of the station ‘Good Neighbors’.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "The station is on the edge of explored territory,";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "which means that its security is one of our utmost concerns.";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TextMeshProUGUI>().text = "I want each and every one of you to keep an eye out, and at the sign of something strange,";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<TextMeshProUGUI>().text = "you’re to send a distress signal back to earth.";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "Good luck, and I’m very proud of all of you.";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
    }
}
