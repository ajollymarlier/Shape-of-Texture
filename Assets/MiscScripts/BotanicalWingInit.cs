using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotanicalWingInit : MonoBehaviour
{
    public GameObject movingDoor;
    public string audioLogPath;
    private FMODUnity.StudioEventEmitter emitter;

    private FMOD.Studio.EventInstance instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(audioLogPath);
    }

    public void handleObjPress(GameObject pressedObj){
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instance.start();

        if (!(movingDoor == null)){
            //Move door
            pressedObj.GetComponentInChildren<Light>().intensity = 0;
            Destroy(movingDoor);
        }
        else{
            pressedObj.GetComponentInChildren<Light>().color = Color.green;
        }
    }
}
