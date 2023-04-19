using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TutorialFilter : MonoBehaviour
{
    public Animator animator;
    
    public GameObject gv;
    public Volume volume;
    public bool setToCur;

    // public ColorAdjustments ca;

    public bool triggered;
    public bool part2;
    // Start is called before the first frame update
    void Start()
    {
        volume = gameObject.GetComponent<Volume>();
        setToCur = false;
        triggered = false;
        part2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!setToCur)
        {
            setToCur = true;
            volume.profile.TryGet(out ColorAdjustments ca);
            ca.postExposure.value = PlayerPrefs.GetFloat("brightness");
        }

        if (triggered && !PauseMenu.GamePaused)
        {
            if (volume.profile.TryGet(out ColorAdjustments ca))
                if (ca.postExposure.value < 7.5f && !part2)
                {
                    ca.postExposure.value += 1.8f * Time.deltaTime;
                }
                else
                {
                    part2 = true;
                    ca.postExposure.value -= 3f * Time.deltaTime;
                }
            
            if (volume.profile.TryGet(out Bloom bloom))
                bloom.intensity.value += 1f * Time.deltaTime;
        }
    }

    void OnTriggerEnter()
    {
        triggered = true;
    }
}
