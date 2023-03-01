using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool canCast = true;
    private RectTransform reticle;

    // Start is called before the first frame update
    void Start()
    {
        reticle = gameObject.transform.Find("CrosshairAndStamina/Reticle").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        var lookingatInteractable = CheckLookingAtInteractable();

        if(lookingatInteractable.Item1)
        {
            reticle.sizeDelta = new Vector2(200, 200);
            if(Input.GetKeyDown(KeyCode.E))
            {
                CheckInteraction(lookingatInteractable.Item2);
            }
        }
        else
        {
            reticle.sizeDelta = new Vector2(50, 50);
        }
        
    }

    void UpdateCastable(bool newVal){
        canCast = newVal;
    }

    private (bool, Interactable) CheckLookingAtInteractable()
    {
        // RaycastHit hit;
        Ray landingRay = new Ray(transform.position, transform.forward);

        if (canCast){
            // RaycastHit[] hits = Physics.RaycastAll(landingRay, 4.5f);
            // if (hits.Length > 0)
            // {
            //     foreach(RaycastHit hit in hits){
            //         Debug.Log("Trying to interact with " + hit.collider.name);
            //         if (hit.transform.GetComponent<Interactable>())
            //         {
            //             return (true, hit.transform.GetComponent<Interactable>());

            //         }
            //     }
            // }
            RaycastHit hit;
            if(Physics.Raycast(landingRay, out hit, 4.5f)){
                Debug.Log("Trying to interact with " + hit.collider.name);
                if (hit.transform.GetComponent<Interactable>())
                {
                    return (true, hit.transform.GetComponent<Interactable>());

                }
            }
            
        }
        return (false, null);
    }

    private void CheckInteraction(Interactable hit)
    {
        hit.Interact();
    }
}
