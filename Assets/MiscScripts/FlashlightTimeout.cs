using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public GameObject warningText;

    public Animator animator;

    private bool lowBattery;

    private bool text1;
    private bool text2;
    // Start is called before the first frame update
    void Start()
    {
        timerValSecs = startingTimerValSecs;
        lowBattery = false;
        text1 = false;
        text2 = false;
        flashlight = GetComponent<Light>();
        defaultFlashlightIntensity = flashlight.intensity;
        flashingIntensity = defaultFlashlightIntensity - (startingTimerValSecs - lowBatteryTime) * fadeAmount;
        animator = warningText.GetComponent<Animator>();
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
                text1 = true;
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
            if (lowBattery && text1)
            {
                // change warning text
                warningText.GetComponent<TextMeshProUGUI>().text = "Warning: Find a battery";
                animator.SetTrigger("Warned");
                animator.ResetTrigger("Reset");
            }
            if (timerValSecs < lowBatteryTime / 2)
            {
                text1 = false;
                text2 = true;
            }
            if (lowBattery && text2)
            {
                if (timerValSecs % 2 < 0.5){
                    // change warning text
                    warningText.GetComponent<TextMeshProUGUI>().text = "Warning: Find a BATTERY";
                }
                else if (timerValSecs % 2 < 1){
                    // change warning text
                    warningText.GetComponent<TextMeshProUGUI>().text = "Warning: Find A battery";
                }
                else if (timerValSecs % 2 < 1.5){
                    // change warning text
                    warningText.GetComponent<TextMeshProUGUI>().text = "Warning: FIND a battery";
                }
                else
                {
                    // change warning text
                    warningText.GetComponent<TextMeshProUGUI>().text = "WARNING: Find a battery";
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
        lowBattery = false;
        text1 = false;
        text2 = false;
        warningText.GetComponent<TextMeshProUGUI>().text = "";
        animator.ResetTrigger("Warned");
        animator.SetTrigger("Reset");
    }
}
