using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedBaySubtitle : MonoBehaviour
{
    public GameObject textBox;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SubtitleSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SubtitleSequence() {
        yield return new WaitForSeconds(3);
        textBox.GetComponent<Text>().text = "Hello, patient. This is a prerecorded message...";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<Text>().text = "if you are currently experiencing difficulty seeing, please do not panic,";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<Text>().text = "this is a very common side effect of the anesthetic used aboard the station.";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<Text>().text = "For your ease of navigation, please follow the beeping noises to the exit door.";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<Text>().text = "";
    }
}
