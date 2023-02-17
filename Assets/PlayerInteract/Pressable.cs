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
            Debug.Log("Object is now pressed");
        }
        else
        {
            Debug.Log("Object is now unpressed");
        }
    }
}
