using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;// Required when using Event data.

public class Pressable : Interactable, IPointerEnterHandler
{
    private bool isPressed;

    private void Start()
    {
        isPressed = false;
    }

    public void OnPointerEnter(PointerEventData eventData){
        Debug.Log("Enter!");
    }

    public void OnPointerExit(PointerEventData eventData){
        Debug.Log("Exit!");
    }

    public override void Interact()
    {
        Debug.Log("Object is now pressed");
            
        ButtonPuzzleInit buttonPuzzleInit = gameObject.transform.parent.GetComponent<ButtonPuzzleInit>();
        buttonPuzzleInit.handleButtonPress(gameObject);
    }
}
