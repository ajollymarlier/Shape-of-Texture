using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
    using System.Net;
#endif

public class BatteryPickup : Interactable
{
    public FlashlightTimeout flashlight; 
    
    public override void Interact()
    {
        Debug.Log("Button is picked up");

        // Play pickup sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/Botanical Wing Scene/SFX_batterypickup", transform.position);
        flashlight.timerValSecs = 120;

        Destroy(gameObject);
    }
}
