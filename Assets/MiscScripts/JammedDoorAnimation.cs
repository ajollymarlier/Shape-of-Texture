using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JammedDoorAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public bool closing;
    public float distanceTravelled;
    public float maxDistance = 2;

    public GameObject emitter;

    void Start()
    {
        distanceTravelled = 0.0f;
        closing = true;    
        //event:/Medical Bay Intro Scene/SFX_jammed doors
    }

    // Update is called once per frame
    void Update()
    {
        if (closing){
            float move = Mathf.Min(10 * Time.deltaTime, maxDistance);
            transform.Translate(Vector3.left * move);
            emitter.transform.Translate(Vector3.left * move);
            distanceTravelled += Vector3.left.x * move;

            if (distanceTravelled <= -1.4){
                closing = false;
                emitter.GetComponent<FMODUnity.StudioEventEmitter>().Play();
            }
        }
        else{
            float move = Mathf.Min(0.4f * Time.deltaTime, maxDistance);
            transform.Translate(Vector3.right * move);
            emitter.transform.Translate(Vector3.right * move);
            distanceTravelled += Vector3.right.x * move;

            if (distanceTravelled >= -0.5){
                closing = true;
            }
        }
    }
}
