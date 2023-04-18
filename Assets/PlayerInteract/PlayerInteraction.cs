using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    private bool canCast = true;
    private RectTransform reticleRect;
    private TextMeshProUGUI interactText;
    // private Image reticleImage;
    public bool inMedBay;
    public bool initCanInteract;

    // Start is called before the first frame update
    void Start()
    {
        reticleRect = gameObject.transform.Find("CrosshairAndStamina/Reticle").GetComponent<RectTransform>();
        interactText = gameObject.transform.Find("CrosshairAndStamina/UI_Elements/InteractControl").GetComponent<TextMeshProUGUI>();
        // reticleImage = gameObject.transform.Find("CrosshairAndStamina/Reticle").GetComponent<Image>();
        if (SceneManager.GetActiveScene().name == "Final Medical Bay")
        {
            inMedBay = true;
            initCanInteract = false;
            
            StartCoroutine(WaitToInteract());
        }
        else
        {
            inMedBay = false;
        }
    }

    public IEnumerator WaitToInteract() {
        yield return new WaitForSeconds(27.01f);
        initCanInteract = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GamePaused){
            var lookingatInteractable = CheckLookingAtInteractable();

            if(lookingatInteractable.Item1)
            {
                reticleRect.sizeDelta = new Vector2(200, 200);
                Interactable item = lookingatInteractable.Item2;
                if (item.GetType() == typeof(Pressable) && ((Pressable)item).pressed && !((Pressable)item).sequenceEnded)
                {
                    interactText.text = "";
                }
                else if (item.GetType() == typeof(ConsoleInteract))
                {
                    if (!((ConsoleInteract)item).interacted)
                    {
                        interactText.text = "[E]/[Left Click] - Interact";
                        if(Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                        {
                            CheckInteraction(item);
                        }
                    }
                    else
                    {
                        interactText.text = "Botanical Wing door has opened.";
                    }
                }
                else if (!inMedBay)
                {
                    interactText.text = "[E]/[Left Click] - Interact";
                    if(Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        CheckInteraction(item);
                    }
                }
                else
                {
                    if (item.GetType() == typeof(PuzzleStart) && !initCanInteract)
                    {
                        interactText.text = "Please wait until log is over...";
                    }

                    else if (item.GetType() == typeof(PuzzleStart) && ((PuzzleStart)item).triggered)
                    {
                        interactText.text = "";
                    }

                    else if (item.GetType() == typeof(Pressable) && !((Pressable)item).medBayCanPress)
                    {
                        interactText.text = "Initialize door sequence first";
                    }

                    else if (item.GetType() == typeof(Pressable) && ((Pressable)item).sequenceEnded)
                    {
                        interactText.text = "Sequence completed";
                    }

                    else
                    {
                        interactText.text = "[E]/[Left Click] - Interact";
                        if(Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                        {
                            CheckInteraction(item);
                        }
                    }
                }
                
            }
            else
            {
                reticleRect.sizeDelta = new Vector2(50, 50);
                interactText.text = "";
            }
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
