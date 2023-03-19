using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInit : MonoBehaviour
{
    public GameObject movingDoor;
    public bool isAudioLog;
    public string audioLogPath;
    private FMODUnity.StudioEventEmitter emitter;

    private FMOD.Studio.EventInstance instance;

    private void Start(){
        instance = FMODUnity.RuntimeManager.CreateInstance(audioLogPath);
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
        }   
    }

    void Update()
    {
        
    }
}
