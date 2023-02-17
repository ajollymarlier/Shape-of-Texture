using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzleInit : MonoBehaviour
{
    public GameObject[] orderedButtons;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        i=0;
    }

    // Update is called once per frame
    public void init()
    {
        orderedButtons[0].GetComponent<FMODUnity.StudioEventEmitter>().Play();
    }

    public void handleButtonPress(GameObject pressedButton){
        if (i < orderedButtons.Length - 1 && pressedButton == orderedButtons[i]){
            pressedButton.transform.GetChild(0).GetComponent<Light>().intensity = 0;
            pressedButton.GetComponent<FMODUnity.StudioEventEmitter>().Stop();
            i += 1;

            orderedButtons[i].GetComponent<FMODUnity.StudioEventEmitter>().Play();
        
        } 
        // Wrong button
        else if (pressedButton != orderedButtons[i])
        {
            pressedButton.GetComponent<FMODUnity.StudioEventEmitter>().Stop();
            orderedButtons[i].GetComponent<FMODUnity.StudioEventEmitter>().Stop();

            // Turn lights back on
            for (int j=0; j < i; j++){
                orderedButtons[j].transform.GetChild(0).GetComponent<Light>().intensity = 100;
            }

            i = 0;
            orderedButtons[0].GetComponent<FMODUnity.StudioEventEmitter>().Play();
        }
        else{
            pressedButton.transform.GetChild(0).GetComponent<Light>().intensity = 0;
            pressedButton.GetComponent<FMODUnity.StudioEventEmitter>().Stop();

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
        }
    }
}
