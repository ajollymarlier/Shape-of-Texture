using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapTrigger : MonoBehaviour
{
    private bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider other)
    {   
        if(!triggered){
            GameObject[] doorLights = GameObject.FindGameObjectsWithTag("Door Light");
            for (int i=0; i < doorLights.Length; i++){
                doorLights[i].GetComponent<Light>().intensity = 0;
            }

            GameObject[] buttonLights = GameObject.FindGameObjectsWithTag("Button Light");

            for (int i=0; i < buttonLights.Length; i++){
                buttonLights[i].GetComponent<Light>().intensity = 100;
            }

            ButtonPuzzleInit buttonPuzzleInit = gameObject.GetComponent<ButtonPuzzleInit>();
            buttonPuzzleInit.init();

            triggered = true;
        }
    }
}
