using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MistakeManager : MonoBehaviour
{
    public int mistakes;
    public bool isGameOver;

    private IEnumerator cr;

    public GameObject warningText;
    public Animator animator;

    private FMOD.Studio.EventInstance instance;
    public string deathAudioLogPath;

    // Start is called before the first frame update
    void Start()
    {
        mistakes = 0;
        isGameOver = false;
        animator = warningText.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeMistake()
    {
        mistakes += 1;
        if (mistakes == 1)
        {
            cr = WarnPlayer();
            StartCoroutine(cr);
        }
        else if (mistakes == 2)
        {
            StopCoroutine(cr);
            cr = WarnPlayer2();
            StartCoroutine(cr);
        }
        else if (mistakes == 3)
        {
            StopCoroutine(cr);
            cr = WarnPlayer3();
            StartCoroutine(cr);
            GameOver();
        }
    }

    private void GameOver(){
        mistakes = 0;

        instance = FMODUnity.RuntimeManager.CreateInstance(deathAudioLogPath);
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instance.start();
        StartCoroutine(flashlightBlackoutSequence());
    }

    private IEnumerator flashlightBlackoutSequence(){
        yield return new WaitForSeconds(4);
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        restartLevel();
    }

    private void restartLevel(){
        Time.timeScale = 1f;
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

    IEnumerator WarnPlayer() {
        // change warning text
        warningText.GetComponent<TextMeshProUGUI>().text = "Warning: Follow the music...";
        animator.SetTrigger("Warned");
        animator.ResetTrigger("Reset");
        yield return new WaitForSeconds(3);
        animator.SetTrigger("Faded");
        animator.ResetTrigger("Warned");
        yield return new WaitForSeconds(1);
        warningText.GetComponent<TextMeshProUGUI>().text = "";
    }

    IEnumerator WarnPlayer2() {
        // change warning text
        warningText.GetComponent<TextMeshProUGUI>().text = "Warning: FOLLOW THE MUSIC...";
        animator.SetTrigger("Warned");
        animator.ResetTrigger("Reset");
        yield return new WaitForSeconds(3);
        animator.SetTrigger("Faded");
        animator.ResetTrigger("Warned");
        yield return new WaitForSeconds(1);
        warningText.GetComponent<TextMeshProUGUI>().text = "";
    }

    IEnumerator WarnPlayer3() {
        isGameOver = true;
        // change warning text
        warningText.GetComponent<TextMeshProUGUI>().text = "Warning: ... the ... music ...";
        animator.SetTrigger("Warned");
        animator.ResetTrigger("Reset");
        yield return new WaitForSeconds(3);
        animator.SetTrigger("Faded");
        animator.ResetTrigger("Warned");
        yield return new WaitForSeconds(1);
        warningText.GetComponent<TextMeshProUGUI>().text = "";
    }

}
