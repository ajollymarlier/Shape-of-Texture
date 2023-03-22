using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        textBox.GetComponent<TextMeshProUGUI>().text = "Hello, patient. This is a prerecorded message...";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TextMeshProUGUI>().text = "if you are currently experiencing difficulty seeing, please do not panic,";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<TextMeshProUGUI>().text = "this is a very common side effect of the anesthetic used aboard the station.";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TextMeshProUGUI>().text = "The buttons required to unlock the doors contain sound systems for the visually impaired.";
        yield return new WaitForSeconds(6);
        textBox.GetComponent<TextMeshProUGUI>().text = "Apologies for the inconvenience, provided you are a member of the intended crew...";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
    }
}
