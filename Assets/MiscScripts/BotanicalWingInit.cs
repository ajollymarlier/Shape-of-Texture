using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotanicalWingInit : MonoBehaviour
{
    public GameObject movingDoor;
    public string audioLogPath;
    public GameObject textBox;
    public Light flashlight;
    private FMODUnity.StudioEventEmitter emitter;

    private FMOD.Studio.EventInstance instance;
    private bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(audioLogPath);
        isPlaying = false;
    }

    public void handleObjPress(GameObject pressedObj){
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instance.start();
        isPlaying = true;

        if (audioLogPath == "event:/Botanical Wing Scene/SFX_BotanicalIntroLog") {
            StartCoroutine(SubtitleSequence001());
        }
        else if (audioLogPath == "event:/Botanical Wing Scene/SFX_BotanicalIntroLog 2") {
            StartCoroutine(SubtitleSequence002());
        }
        

        if (!(movingDoor == null)){
            //Move door
            pressedObj.GetComponentInChildren<Light>().intensity = 0;
            Destroy(movingDoor);
            flashlight.GetComponent<FlashlightTimeout>().losingBattery = true;
        }
        else{
            pressedObj.GetComponentInChildren<Light>().color = Color.green;
        }
    }

    void Update()
    {
        if (isPlaying && PauseMenu.GamePaused){
            instance.setPaused(true);
        }
        else if (isPlaying && !PauseMenu.GamePaused){
            instance.setPaused(false);
        }
    }

    IEnumerator SubtitleSequence001() {
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "Okay, so before we send this station to its orbit, I'm making a simple guide for the crew.";
        yield return new WaitForSeconds(6);
        textBox.GetComponent<TextMeshProUGUI>().text = "This is the biolab, now, I'm sure you're all familiar with the concept of plants,";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TextMeshProUGUI>().text = "but for those of you who come from colonies where they weren't on the curriculum,";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TextMeshProUGUI>().text = "plants produce food and oxygen";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "Without them, the station does not produce food or oxygen.";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "Generally the plants in the biolab should never be left unattended for more than four hours.";
        yield return new WaitForSeconds(6);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
    }

    IEnumerator SubtitleSequence002() {
        // yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "Alright, with the simple stuff out of the way, let's move on to the technical elements;";
        yield return new WaitForSeconds(6);
        textBox.GetComponent<TextMeshProUGUI>().text = "The plants are being exposed to radiation-emitting lights at all times.";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<TextMeshProUGUI>().text = "Do not stick your hands or any other part of your body";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "directly under the lights unless you're wearing the appropriate protective gear,";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<TextMeshProUGUI>().text = "which should be in the closets in the back.";
        yield return new WaitForSeconds(2);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
    }
}
