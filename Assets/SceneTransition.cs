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
        SceneManager.LoadScene(sceneName:targetSceneName);
    }
}
