using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
    using UnityEditor;
    using System.Net;
#endif

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
        FMODUnity.RuntimeManager.PlayOneShot("event:/Medical Bay Intro Scene/SFX_Doors_open", transform.position);
    }
}
