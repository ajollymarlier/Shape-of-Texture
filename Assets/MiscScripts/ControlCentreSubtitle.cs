using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlCentreSubtitle : MonoBehaviour
{
    public GameObject textBox;

    private bool logPlaying;
    private FMOD.Studio.EventInstance instance;

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(SubtitleSequence());
        logPlaying = false;
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Control Center Scene/ControlLOG_Franks");
    }

    // Update is called once per frame
    void Update()
    {
        if (!logPlaying && !PauseMenu.GamePaused)
        {
            logPlaying = true;
            instance.start();
            StartCoroutine(SubtitleSequence());
        }

        if (logPlaying && PauseMenu.GamePaused){
            instance.setPaused(true);
        }
        else if (logPlaying && !PauseMenu.GamePaused){
            instance.setPaused(false);
        }
    }

    IEnumerator SubtitleSequence() {
        // yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "Alright, folks,";
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "management didn’t allow a proper recreational room when Mark first commissioned Good Neighbors,";
        yield return new WaitForSeconds(6);
        textBox.GetComponent<TextMeshProUGUI>().text = "which means the control room is what I’d recommend as a lounge.";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<TextMeshProUGUI>().text = "It’s fairly spacious, already made for discussions,";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "and it’s roughly in the middle of the station,";
        yield return new WaitForSeconds(2);
        textBox.GetComponent<TextMeshProUGUI>().text = "so you can get anywhere from there.";
        yield return new WaitForSeconds(2);
        textBox.GetComponent<TextMeshProUGUI>().text = "Just ignore the big super-console and you should be fine.";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "Again, apologies and I hope it works. Franks out.";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
    }
}
