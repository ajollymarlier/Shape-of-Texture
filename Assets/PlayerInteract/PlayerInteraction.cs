using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    private bool canCast = true;
    private RectTransform reticleRect;
    // private Image reticleImage;

    // Start is called before the first frame update
    void Start()
    {
        reticleRect = gameObject.transform.Find("CrosshairAndStamina/Reticle").GetComponent<RectTransform>();
        // reticleImage = gameObject.transform.Find("CrosshairAndStamina/Reticle").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        var lookingatInteractable = CheckLookingAtInteractable();

        if(lookingatInteractable.Item1)
        {
            reticleRect.sizeDelta = new Vector2(200, 200);
            if(Input.GetKeyDown(KeyCode.E))
            {
                // reticleImage.color = new Color32(153, 153, 153, 100);
                CheckInteraction(lookingatInteractable.Item2);
            }
            // else
            // {
            //     reticleImage.color = new Color32(255, 255, 255, 100);
            // }
        }
        else
        {
            reticleRect.sizeDelta = new Vector2(50, 50);
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
                if (hit.transform.GetComponent<Interactable>())
                {
                    Debug.Log("Looking at interactable " + hit.collider.name);
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
