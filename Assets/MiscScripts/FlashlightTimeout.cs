using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlashlightTimeout : MonoBehaviour
{
    public float timerValSecs;
    public float lowBatteryTime;
    private float defaultFlashlightIntensity;
    private Light flashlight;

    private bool lowBattery;
    // Start is called before the first frame update
    void Start()
    {
        lowBattery = false;
        flashlight = GetComponent<Light>();
        defaultFlashlightIntensity = flashlight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        timerValSecs -= Time.deltaTime;
        flashlight.intensity -= 0.0002f;

        if (timerValSecs < 0)
        {
            flashlight.intensity = 0;
            GameOver();
        }
        else if (timerValSecs < 10)
        {   
            if (timerValSecs % 2 < 1){
                flashlight.intensity = 0;
            }
            else{
                flashlight.intensity = defaultFlashlightIntensity;
            }
            
        }
        else if (timerValSecs < lowBatteryTime){
            lowBattery = true;
        }
        else if (!lowBattery){
            lowBattery = true;
        }
    }

    private void GameOver(){
        SceneManager.LoadScene("Main Menu");
    }

    public void ResetFlashlightIntensity(){
        flashlight.intensity = defaultFlashlightIntensity;
    }
}
