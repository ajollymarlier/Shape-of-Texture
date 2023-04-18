using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using FMODUnity;

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

    public GameObject volumeSlider;
    public GameObject brightnessSlider;
    public GameObject globalVolumeToggle;
    public GameObject viewBobToggle;
    public GameObject graphicsDropdown;

    private FMOD.Studio.Bus Master;

    void Awake()
    {
        Master = FMODUnity.RuntimeManager.GetBus("bus:/");
    }

    void Start()
    {
        GamePaused = false;

        fpcObject = GameObject.Find("FirstPersonController");
        fpc = fpcObject.GetComponent<FirstPersonController>();
        controllerCanvas = fpcObject.transform.Find("Joint/PlayerCamera/CrosshairAndStamina").gameObject;
        flashlight = fpcObject.transform.Find("Joint/PlayerCamera/Flashlight").GetComponent<Light>();
        flashlightIntensity = flashlight.intensity;
        globalVolume = GameObject.Find("Global Volume").GetComponent<Volume>();
        _animator = pauseMenuUI.GetComponent<Animator>();

        // Load PlayerPrefs
        // Master Volume
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.6f);
            Debug.Log("Set volume");
        }
        float volume = PlayerPrefs.GetFloat("volume");
        SetVolume(volume);
        volumeSlider.GetComponent<Slider>().value = volume;
        
        // Brightness
        if (!PlayerPrefs.HasKey("brightness"))
        {
            PlayerPrefs.SetFloat("brightness", 1.5f);
            Debug.Log("Set brightness");
        }
        float brightness = PlayerPrefs.GetFloat("brightness");
        SetBrightness(brightness);
        brightnessSlider.GetComponent<Slider>().value = brightness;

        // Toggle Filters
        if (!PlayerPrefs.HasKey("toggleGVolume"))
        {
            PlayerPrefs.SetInt("toggleGVolume", 1);
            Debug.Log("Set toggleGVolume");
        }
        bool isToggled = PlayerPrefs.GetInt("toggleGVolume") == 1;
        ToggleGlobalVolume(isToggled);
        globalVolumeToggle.GetComponent<Toggle>().isOn = isToggled;

        // Toggle View Bob
        if (!PlayerPrefs.HasKey("toggleHeadBob"))
        {
            PlayerPrefs.SetInt("toggleHeadBob", 1);
            Debug.Log("Set toggleHeadBob");
        }
        isToggled = PlayerPrefs.GetInt("toggleHeadBob") == 1;
        ToggleHeadBob(isToggled);
        viewBobToggle.GetComponent<Toggle>().isOn = isToggled;

        // Quality
        if (!PlayerPrefs.HasKey("qualityIndex"))
        {
            PlayerPrefs.SetInt("qualityIndex", 3);
            Debug.Log("Set tqualityIndex");
        }
        int qualityIndex = PlayerPrefs.GetInt("qualityIndex");
        SetQuality(qualityIndex);
        graphicsDropdown.GetComponent<TMP_Dropdown>().value = qualityIndex;

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
        FMODUnity.StudioEventEmitter[] emitters = GameObject.FindObjectsOfType<FMODUnity.StudioEventEmitter>();
        foreach (FMODUnity.StudioEventEmitter em in emitters)
            em.GetComponent<FMODUnity.StudioEventEmitter>().Stop();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        GamePaused = false;
        GameObject.Find("FirstPersonController").GetComponent<Footsteps_Audio>().stopsound();
        FMODUnity.StudioEventEmitter[] emitters = GameObject.FindObjectsOfType<FMODUnity.StudioEventEmitter>();
        foreach (FMODUnity.StudioEventEmitter em in emitters)
            em.GetComponent<FMODUnity.StudioEventEmitter>().Stop();
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == 1)
        {
            TutorialInit[] inits = GameObject.FindObjectsOfType<TutorialInit>();
            foreach (TutorialInit init in inits)
                init.Stop();
        }
        else if (index == 2)
        {
            GameObject.Find("SubtitleManager").GetComponent<MedBaySubtitle>().Stop();
        }
        else if (index == 3)
        {
            GameObject.Find("SubtitleManager").GetComponent<ControlCentreSubtitle>().Stop();
        }
        else if (index == 4)
        {
            BotanicalWingInit[] inits = GameObject.FindObjectsOfType<BotanicalWingInit>();
            foreach (BotanicalWingInit init in inits)
                init.Stop();
        }
        SceneManager.LoadScene(index);
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
        Debug.Log("volume: " + volume);
        PlayerPrefs.SetFloat("volume", volume);
        Master.setVolume(volume);
    }

    public void SetBrightness(float brightness)
    {
        Debug.Log("brightness: " + brightness);
        PlayerPrefs.SetFloat("brightness", brightness);
        globalVolume.profile.TryGet(out ColorAdjustments ca);
        ca.postExposure.value = brightness;
        // flashlight.intensity = flashlightIntensity * brightness;
    }

    public void ToggleGlobalVolume (bool isToggled)
    {
        Debug.Log("global volume: " + isToggled);
        // globalVolume.enabled = isToggled;
        globalVolume.profile.TryGet(out FilmGrain fg);
        fg.active = isToggled;
        globalVolume.profile.TryGet(out ChromaticAberration ca);
        ca.active = isToggled;
        globalVolume.profile.TryGet(out Bloom b);
        b.active = isToggled;
        PlayerPrefs.SetInt("toggleGVolume", (isToggled ? 1 : 0));
    }

    public void ToggleHeadBob (bool isToggled)
    {
        Debug.Log("view bob: " + isToggled);
        fpc.enableHeadBob = isToggled;
        PlayerPrefs.SetInt("toggleHeadBob", (isToggled ? 1 : 0));
    }

    public void SetQuality (int qualityIndex)
    {
        Debug.Log("quality level: " + qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("qualityIndex", qualityIndex);
    }

}
