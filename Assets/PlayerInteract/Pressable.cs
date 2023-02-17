using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressable : Interactable
{

    private bool isPressed;

    private void Start()
    {
        isPressed = false;
    }

    public override void Interact()
    {
        isPressed = !isPressed;
        if (isPressed)
        {
            GameObject[] doorLights = GameObject.FindGameObjectsWithTag("Door Light");
            for (int i=0; i < doorLights.Length; i++){
                doorLights[i].GetComponent<Light>().intensity = 40;
            }

            GameObject[] buttonLights = GameObject.FindGameObjectsWithTag("Button Light");
            for (int i=0; i < buttonLights.Length; i++){
                buttonLights[i].GetComponent<Light>().intensity = 0;
            }
        }
        else
        {
            Debug.Log("Object is now unpressed");
        }
    }
}
