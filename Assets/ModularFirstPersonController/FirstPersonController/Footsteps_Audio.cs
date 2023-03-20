using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
    using UnityEditor;
    using System.Net;
#endif

public class Footsteps_Audio : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;
    private bool playSound;

    private bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Medical Bay Intro Scene/SFX_steps_metal");
        playSound = false;
        isPlaying = false;
    }
    void Update()
    
    {
        if (GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>().isWalking && !playSound)
        {
            startsound();
            playSound = true;
        }
        else if (!GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>().isWalking)
        {
            stopsound();
            playSound = false;
        }

        if (isPlaying && PauseMenu.GamePaused){
            instance.setPaused(true);
        }
        else if (isPlaying && !PauseMenu.GamePaused){
            instance.setPaused(false);
        }
    }
    public void startsound()
    {
        // Debug.Log("Sound start");
        instance.start();
        isPlaying = true;  
    }

    public void stopsound()
    {
        // Debug.Log("Sound stop");
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        isPlaying = false;
    }
}
