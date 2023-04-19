using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string targetSceneName;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other){
        GameObject.Find("FirstPersonController").GetComponent<Footsteps_Audio>().stopsound();
        FMODUnity.StudioEventEmitter[] emitters = GameObject.FindObjectsOfType<FMODUnity.StudioEventEmitter>();
        foreach (FMODUnity.StudioEventEmitter em in emitters)
            em.GetComponent<FMODUnity.StudioEventEmitter>().Stop();
        SceneManager.LoadScene(sceneName:targetSceneName);
    }
}
