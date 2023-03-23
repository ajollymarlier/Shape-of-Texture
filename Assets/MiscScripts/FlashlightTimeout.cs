using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlashlightTimeout : MonoBehaviour
{
    public float startingTimerValSecs;
    public float timerValSecs;
    public float lowBatteryTime = 10;
    public float fadeAmount = 0.02f;
    private float defaultFlashlightIntensity;
    public float flashingIntensity;
    public bool losingBattery;
    private Light flashlight;

    private bool lowBattery;
    // Start is called before the first frame update
    void Start()
    {
        timerValSecs = startingTimerValSecs;
        lowBattery = false;
        flashlight = GetComponent<Light>();
        defaultFlashlightIntensity = flashlight.intensity;
        flashingIntensity = defaultFlashlightIntensity - (startingTimerValSecs - lowBatteryTime) * fadeAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (losingBattery){
            timerValSecs -= Time.deltaTime;
            flashlight.intensity -= fadeAmount * Time.deltaTime;

            // No battery
            if (timerValSecs < 0)
            {
                flashlight.intensity = 0;
                GameOver();
            }
            // Low battery
            else if (timerValSecs < lowBatteryTime)
            {   
                lowBattery = true;
            }
            // else if (timerValSecs < lowBatteryTime){
            //     lowBattery = true;
            // }
            // else if (!lowBattery){
            //     lowBattery = true;
            // }

            // Low battery sequence
            if (lowBattery)
            {
                if (timerValSecs % 1 < 0.5){
                    flashlight.intensity = 0.4f;
                }
                else{
                    flashlight.intensity = flashingIntensity;
                }
            }
        }
        
    }

    private void GameOver(){
        GameObject.Find("FirstPersonController").GetComponent<Footsteps_Audio>().stopsound();
        SceneManager.LoadScene("Botanical Wing");
    }

    public void ResetFlashlight(){
        timerValSecs = startingTimerValSecs;
        flashlight.intensity = defaultFlashlightIntensity;
    }
}
