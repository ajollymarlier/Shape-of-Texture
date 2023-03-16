using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JammedDoorAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public bool closing;
    public float distanceTravelled;
    public float maxDistance = 2;

    void Start()
    {
        distanceTravelled = 0.0f;
        closing = true;    
    }

    // Update is called once per frame
    void Update()
    {
        if (closing){
            float move = Mathf.Min(10 * Time.deltaTime, maxDistance);
            transform.Translate(Vector3.left * move);
            distanceTravelled += Vector3.left.x * move;

            if (distanceTravelled <= -1.4){
                closing = false;
            }
        }
        else{
            float move = Mathf.Min(0.75f * Time.deltaTime, maxDistance);
            transform.Translate(Vector3.right * move);
            distanceTravelled += Vector3.right.x * move;

            if (distanceTravelled >= -0.5){
                closing = true;
            }
        }
    }
}
