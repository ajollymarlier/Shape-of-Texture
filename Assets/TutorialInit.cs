using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void handleButtonPress(GameObject pressedButton){
        pressedButton.GetComponentInChildren<Light>().intensity = 0;
        gameObject.transform.Translate(Vector3.right * 4);   
    }
}
