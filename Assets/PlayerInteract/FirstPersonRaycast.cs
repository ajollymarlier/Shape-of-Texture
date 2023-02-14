using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonRaycast : MonoBehaviour
{
    private bool canCast = true;

    public GameObject interactManager;

    // Start is called before the first frame update
    void Start()
    {
        interactManager = GameObject.FindWithTag("InteractManager");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, transform.forward);
        // Vector3 look = t.TransformDirection(Vector3.forward);

        if (canCast){
            if (Physics.Raycast(landingRay, out hit, 5)){
                Debug.Log("Looking at " + hit.collider.tag);
            }
        }
    }

    void UpdateCastable(bool newVal){
        canCast = newVal;
    }
}
