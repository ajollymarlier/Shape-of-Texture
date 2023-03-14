using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleInteract : Interactable
{
    public override void Interact()
    {
        GameObject hall = GameObject.Find("Structure/Halls/BotanicalHall");
        BotanDoorInit botanDoorInit = hall.transform.GetComponent<BotanDoorInit>();
        botanDoorInit.handleInteract(gameObject);

        Light botanDoorLight = hall.transform.GetComponentInChildren<Light>();
        botanDoorLight.intensity = 100;
    }
}
