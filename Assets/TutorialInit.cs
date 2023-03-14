using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInit : MonoBehaviour
{
    public GameObject movingDoor;
    public bool isAudioLog;
    public string audioLogPath;

    public void handleObjPress(GameObject pressedObj){
        pressedObj.GetComponentInChildren<Light>().intensity = 0;
        movingDoor.transform.Translate(Vector3.right * 4);

        if (isAudioLog){
            Debug.Log("AUDIO LOG!");
        }   
    }
}
