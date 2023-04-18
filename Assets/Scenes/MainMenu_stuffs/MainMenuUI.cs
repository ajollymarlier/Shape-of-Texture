using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;

public class MainMenuUI : MonoBehaviour{

    public GameObject title;
    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;
    public GameObject playMenuUI;
    public GameObject levelMenuUI;

    private Volume globalVolume;

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
        globalVolume = GameObject.Find("Global Volume").GetComponent<Volume>();

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

    public void ToPlayMenu()
    {
        title.SetActive(false);
        mainMenuUI.SetActive(false);
        playMenuUI.SetActive(true);
    }

    public void PlayTheGame (GameObject button)
	{
        PauseMenu.GamePaused = false;
        button.GetComponent<MenuButtonSFX>().stopsound();
		SceneManager.LoadScene(1);
	}

    public void PlayLevel1(GameObject button)
    {
        PauseMenu.GamePaused = false;
        button.GetComponent<MenuButtonSFX>().stopsound();
		SceneManager.LoadScene(1);
    }

    public void PlayLevel2(GameObject button)
    {
        PauseMenu.GamePaused = false;
        button.GetComponent<MenuButtonSFX>().stopsound();
		SceneManager.LoadScene(2);
    }

    public void PlayLevel3(GameObject button)
    {
        PauseMenu.GamePaused = false;
        button.GetComponent<MenuButtonSFX>().stopsound();
		SceneManager.LoadScene(3);
    }

    public void PlayLevel4(GameObject button)
    {
        PauseMenu.GamePaused = false;
        button.GetComponent<MenuButtonSFX>().stopsound();
		SceneManager.LoadScene(4);
    }

    public void Options()
    {
        title.SetActive(false);
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

	public void QuitGame ()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

    public void Return()
    {
        title.SetActive(true);
        mainMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        playMenuUI.SetActive(false);
        levelMenuUI.SetActive(false);
    }

    public void ToLevelMenu()
    {
        playMenuUI.SetActive(false);
        levelMenuUI.SetActive(true);
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
    }

    public void ToggleGlobalVolume (bool isToggled)
    {
        Debug.Log("global volume: " + isToggled);
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
        PlayerPrefs.SetInt("toggleHeadBob", (isToggled ? 1 : 0));
    }

    public void SetQuality (int qualityIndex)
    {
        Debug.Log("quality level: " + qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("qualityIndex", qualityIndex);
    }
}
