using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject controllerCanvas;
    
    public Animator _animator;
    private Volume globalVolume;
    private GameObject fpcObject;
    private FirstPersonController fpc;
    private Light flashlight;
    private float flashlightIntensity;

    void Start()
    {
        fpcObject = GameObject.Find("FirstPersonController");
        fpc = fpcObject.GetComponent<FirstPersonController>();
        controllerCanvas = fpcObject.transform.Find("Joint/PlayerCamera/CrosshairAndStamina").gameObject;
        flashlight = fpcObject.transform.Find("Joint/PlayerCamera/Flashlight").GetComponent<Light>();
        flashlightIntensity = flashlight.intensity;
        globalVolume = GameObject.Find("Global Volume").GetComponent<Volume>();
        GamePaused = false;
        _animator = pauseMenuUI.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (GamePaused){
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);

        Time.timeScale = 1f;
        GamePaused = false;
        if (controllerCanvas){
            controllerCanvas.SetActive(true);
        }
    }

    void Pause()
    {
        
        pauseMenuUI.SetActive(true);
        if (_animator == null)
        {
            _animator = pauseMenuUI.GetComponent<Animator>();
        }
        Time.timeScale = 0f;
        GamePaused = true;
        _animator.SetTrigger("Paused");
        if (controllerCanvas){
            controllerCanvas.SetActive(false);
        }
        
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Loading menu");
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void Options()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void Return()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
    }

    public void SetBrightness(float brightness)
    {
        Debug.Log(brightness);
        flashlight.intensity = flashlightIntensity * brightness;
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void ToggleGlobalVolume (bool isToggled)
    {
        globalVolume.enabled = isToggled;
    }

    public void ToggleHeadBob (bool isToggled)
    {
        fpc.enableHeadBob = isToggled;
    }
}
