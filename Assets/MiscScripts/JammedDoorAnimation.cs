using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JammedDoorAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public bool closing;
    public float distanceTravelled;

    void Start()
    {
        distanceTravelled = 0.0f;
        closing = true;    
    }

    // Update is called once per frame
    void Update()
    {
        if (closing){
            transform.Translate(Vector3.left * 10 * Time.deltaTime);
            distanceTravelled += Vector3.left.x * 10 * Time.deltaTime;

            if (distanceTravelled <= -1.4){
                closing = false;
            }
        }
        else{
            transform.Translate(Vector3.right * 0.75f * Time.deltaTime);
            distanceTravelled += Vector3.right.x * 0.75f * Time.deltaTime;

            if (distanceTravelled >= -0.5){
                closing = true;
            }
        }
    }
}
