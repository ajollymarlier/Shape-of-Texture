using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotanDoorInit : MonoBehaviour
{

    GameObject leftDoor;
    // Start is called before the first frame update
    void Start()
    {
        leftDoor = transform.Find("Door/Left Door").gameObject;
    }

    public void handleInteract(GameObject pressedButton)
    {
        Debug.Log("Door opened");
        Debug.Log(pressedButton);
        leftDoor.SetActive(false);
    }
}
