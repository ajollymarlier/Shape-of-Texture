using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool canCast = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            CheckInteraction();
        }
    }

    void UpdateCastable(bool newVal){
        canCast = newVal;
    }

    private void CheckInteraction()
    {
        // RaycastHit hit;
        Ray landingRay = new Ray(transform.position, transform.forward);

        if (canCast){
            RaycastHit[] hits = Physics.RaycastAll(landingRay, 5);
            if (hits.Length > 0)
            {
                foreach(RaycastHit hit in hits){
                    Debug.Log("Trying to interact with " + hit.collider.name);
                    if (hit.transform.GetComponent<Interactable>())
                    {
                        hit.transform.GetComponent<Interactable>().Interact();
                    }
                }
                
            }
        }
    }
}
