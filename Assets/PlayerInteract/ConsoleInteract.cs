using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleInteract : Interactable
{
    public bool interacted = false;

    private bool logPlaying;
    public GameObject textBox;
    private FMOD.Studio.EventInstance instance;
    private Coroutine cr;

    public GameObject[] doorlightShaders;
    public Shader greenShader;

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(SubtitleSequence());
        logPlaying = false;
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Control Center Scene/Botanical Wing Opened LOG");

        greenShader = Shader.Find("Shader Graphs/GreenLightDoor");
    }

    void Update() {
        if (logPlaying && PauseMenu.GamePaused){
            instance.setPaused(true);
        }
        else if (logPlaying && !PauseMenu.GamePaused){
            instance.setPaused(false);
        }
    }

    public override void Interact()
    {
        if (!interacted)
        {
            GameObject hall = GameObject.Find("Structure/Halls/BotanicalHall");
            BotanDoorInit botanDoorInit = hall.transform.GetComponent<BotanDoorInit>();
            botanDoorInit.handleInteract(gameObject);

            Light botanDoorLight = hall.transform.GetComponentInChildren<Light>();
            botanDoorLight.intensity = 100;

            interacted = true;

            logPlaying = true;
            instance.start();
            cr = StartCoroutine(SubtitleSequence());

            foreach (GameObject doorlight in doorlightShaders)
                    doorlight.GetComponent<Renderer>().material.shader = greenShader;

            // Set all others interacted
            ConsoleInteract[] consoles = GameObject.FindObjectsOfType<ConsoleInteract>();
            foreach (var console in consoles)
            {
                console.interacted = true;
            }
        }
        
    }

    IEnumerator SubtitleSequence() {
        textBox.GetComponent<TextMeshProUGUI>().text = "Botanical wing opened";
        yield return new WaitForSeconds(2.9f);
        if (textBox.GetComponent<TextMeshProUGUI>().text == "Botanical wing opened")
        {
            textBox.GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
